using CheckMate.Application.DTOs.Authentification;
using CheckMate.Application.Interfaces;
using CheckMate.Application.Services;
using CheckMate.Infrastructure.Configurations;
using CheckMate.Infrastructure.Identity;
using CheckMate.Infrastructure.Identity.Tokens;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;

namespace CheckMate.Tests.Application.Services
{
    public class TokenServiceTests
    {
        private readonly Mock<IRefreshTokenRepository> _mockTokenRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserStore<User>> _mockUserStore;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly JwtSettings _settings = new()
        {
            Audience = "LocalHost",
            Issuer = "LocalHost",
            RefreshTokenExpirationDays = 7,
            AccessTokenExpirationMinutes = 30,
            SigningKey = "3SGxTPYByjfU/KUHIo6y2maFugCQ5XIKg442URGFQco="
        };

        private readonly User _user = new User
        {
            Id = "0e55319f-8c9e-4111-9c44-3f5a4e14c32d",
            FirstName = "Admin",
            LastName = "Admin",
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            UserName = "Administrator",
            Email = "admin@checkmate.local"
        };

        private readonly RefreshToken _refreshToken = new()
        {
           Token = "PkTZbahPLaqBKYn1CJuwF1K+DcJ7DJgCssu0G+YIqhNUKswOTNpqtN4WErRyzD77LefoWFbl1mYOLTd6PBlmpw==",
           IsRevoked = false,
           RevokedAt = null,
           UserId = "0e55319f-8c9e-4111-9c44-3f5a4e14c32d"
        };

        private readonly Mock<IOptions<JwtSettings>> _mockIOptions;
        private readonly ITokenService _tokenService;

        public TokenServiceTests()
        {
            _mockTokenRepository = new Mock<IRefreshTokenRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUserStore = new Mock<IUserStore<User>>();

            _mockUserManager = new Mock<UserManager<User>>(
                _mockUserStore.Object,
                Options.Create(new IdentityOptions()),
                new PasswordHasher<User>(),
                Array.Empty<IUserValidator<User>>(),
                Array.Empty<IPasswordValidator<User>>(),
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                Mock.Of<IServiceProvider>(),
                NullLogger<UserManager<User>>.Instance
            );

            _mockIOptions = new Mock<IOptions<JwtSettings>>();
            _mockIOptions.Setup(op => op.Value).Returns(_settings);

            _tokenService = new TokenService(
                userManager: _mockUserManager.Object,
                options: _mockIOptions.Object,
                refreshTokenRepository: _mockTokenRepository.Object,
                unitOfWork: _mockUnitOfWork.Object
                );
        }

        #region GenerateAccessToken

        [Fact]
        public async Task GenerateAccessTokenAsync_WhenSigningKeyNull_ShouldRaiseAnException()
        {
            // Arrange
            _settings.SigningKey = null;
            
            // Act
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _tokenService.GenerateAccessTokenAsync(_user, ["Admin"]));

            // Assert
            Assert.Equal("Could not find the Jwt signing key.", exception.Message);
        }

        [Fact]
        public async Task GenerateAccessTokenAsync_WhenSigningKeyEmpty_ShouldRaiseAnException_()
        {
            // Arrange
            _settings.SigningKey = String.Empty;

            // Act + Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _tokenService.GenerateAccessTokenAsync(_user, ["Admin"]));
        }

        [Fact]
        public async Task GenerateAccessTokenAsync_ShouldReturnValidObject()
        {
            // Act
            AccessTokenGenerationDto dto = await _tokenService.GenerateAccessTokenAsync(_user, ["Admin"]);

            // Assert
            Assert.NotNull(dto);
            Assert.NotEmpty(dto.AccessToken);
            Assert.NotEmpty(dto.JwtId);
            Assert.Equal(_settings.AccessTokenExpirationMinutes, dto.AccessTokenExpiryMinutes);
        }

        #endregion

        #region GenerateRefreshToken

        [Fact]
        public void GenerateRefreshToken_MustReturnValidToken()
        {
            // Arrange
            string jwtId = "ABC123";

            // Act
            RefreshToken token = _tokenService.GenerateRefreshToken(jwtId, _user.Id);

            // Assert
            Assert.Equal(token.JwtId, jwtId);
            Assert.Equal(token.UserId, _user.Id);
            Assert.False(token.IsRevoked);
            Assert.Null(token.RevokedAt);
            Assert.Equal(DateOnly.FromDateTime(token.Expires), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationDays)));
        }

        #endregion

        #region RefreshToken

        [Fact]
        public async Task RefreshToken_NonExistantRefreshToken_ReturnNull()
        {
            // Arrange
            _mockTokenRepository.Setup(rtr => rtr.GetRefreshToken(_refreshToken.Token)).ReturnsAsync((RefreshToken?)null);

            // Act
            AuthResponseDto? token = await _tokenService.RefreshToken(_refreshToken.Token);

            // Assert
            Assert.Null(token);
        }

        [Fact]
        public async Task RefreshToken_RevokedRefreshToken_ReturnNull()
        {
            // Arrange
            _refreshToken.IsRevoked = true;
            _mockTokenRepository.Setup(rtr => rtr.GetRefreshToken(_refreshToken.Token)).ReturnsAsync(_refreshToken);

            // Act
            AuthResponseDto? token = await _tokenService.RefreshToken(_refreshToken.Token);

            // Assert
            Assert.Null(token);
        }

        [Fact]
        public async Task RefreshToken_ExpiredRefreshToken_ReturnNull()
        {
            // Arrange
            _refreshToken.Expires = DateTime.UtcNow.AddMinutes(-1);
            _mockTokenRepository.Setup(rtr => rtr.GetRefreshToken(_refreshToken.Token)).ReturnsAsync(_refreshToken);

            // Act
            AuthResponseDto? token = await _tokenService.RefreshToken(_refreshToken.Token);

            // Assert
            Assert.Null(token);
        }

        [Fact]
        public async Task RefreshToken_DisabledUser_ReturnNull()
        {
            // Arrange
            _user.IsActive = false;
            _refreshToken.User = _user;
            _refreshToken.Expires = DateTime.UtcNow.AddDays(7);

            _mockTokenRepository.Setup(rtr => rtr.GetRefreshToken(_refreshToken.Token)).ReturnsAsync(_refreshToken);

            // Act
            AuthResponseDto? token = await _tokenService.RefreshToken(_refreshToken.Token);

            // Assert
            Assert.Null(token);

        }

        [Fact]
        public async Task RefreshToken_ValidRefreshToken_ReturnValidDTO()
        {
            // Arrange
            _refreshToken.User = _user;
            _refreshToken.Expires = DateTime.UtcNow.AddDays(2);

            _mockTokenRepository.Setup(rtr => rtr.GetRefreshToken(_refreshToken.Token)).ReturnsAsync(_refreshToken);
            _mockTokenRepository
                .Setup(rtr => rtr.Revoke(It.IsAny<RefreshToken>()))
                .Callback<RefreshToken>(token =>
                {
                    token.IsRevoked = true;
                    token.RevokedAt = DateTime.UtcNow;
                });

            _mockUserManager.Setup(m => m.GetRolesAsync(_user)).ReturnsAsync(["Admin"]);

            // Act
            AuthResponseDto? token = await _tokenService.RefreshToken(_refreshToken.Token);

            // Assert
            Assert.True(_refreshToken.IsRevoked);
            Assert.NotNull(token);

            Assert.NotEmpty(token.RefreshToken);
            Assert.NotEmpty(token.AccessToken);
        }

        #endregion

        #region RevokeRefreshToken

        [Fact]
        public async Task RevokeRefreshToken_NullRefreshToken_ReturnsFalse()
        {
            // Arrange
            _mockTokenRepository.Setup(r => r.GetRefreshToken(It.IsAny<string>())).ReturnsAsync((RefreshToken)null);

            // Act
            bool revoked = await _tokenService.RevokeRefreshToken(_refreshToken.Token);

            // Assert
            Assert.False(revoked);
        }

        [Fact]
        public async Task RevokeRefreshToken_RevokedRefreshToken_ReturnsFalse()
        {
            // Arrange
            _refreshToken.IsRevoked = true;
            _mockTokenRepository.Setup(r => r.GetRefreshToken(_refreshToken.Token)).ReturnsAsync(_refreshToken);

            // Act
            bool revoked = await _tokenService.RevokeRefreshToken(_refreshToken.Token);

            // Assert
            Assert.False(revoked);
        }

        [Fact]
        public async Task RevokeRefreshToken_ValidRefreshToken_ReturnsTrue()
        {
            // Arrange
            _mockTokenRepository.Setup(r => r.GetRefreshToken(_refreshToken.Token)).ReturnsAsync(_refreshToken);
            _mockTokenRepository
                .Setup(r => r.Revoke(It.IsAny<RefreshToken>()))
                .Callback<RefreshToken>(token =>
                {
                    token.IsRevoked = true;
                    token.RevokedAt = DateTime.UtcNow;
                });
                

            // Act
            bool revoked = await _tokenService.RevokeRefreshToken(_refreshToken.Token);

            // Assert
            Assert.True(revoked);
        }

        #endregion
    }
}
