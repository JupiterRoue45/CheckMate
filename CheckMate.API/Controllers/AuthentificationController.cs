using CheckMate.Application.DTOs.Authentification;
using CheckMate.Application.Interfaces;
using CheckMate.Infrastructure.Identity;
using CheckMate.Infrastructure.Identity.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CheckMate.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public AuthentificationController(
            UserManager<User> userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Login entry point
        /// </summary>
        /// <param name="request">class that contains email and password fields</param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> LogInAsync(LoginRequestDto request)
        {
            // try to retrieve the user from the email address
            User? user = await _userManager.FindByEmailAsync(request.Email);

            // if the user does not exist or entered the wrong password
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Unauthorized();

            // Disabled account
            if (!user.IsActive)
                return Problem(
                    statusCode: StatusCodes.Status403Forbidden,
                    title: "Disabled account",
                    detail: "Your account has been disabled. Contact the admin"
                    );

            // Reached maximum number of connection attempts
            if (await _userManager.IsLockedOutAsync(user))
                return Problem(
                    statusCode: StatusCodes.Status403Forbidden,
                    title: "Account locked temporarily"
                    );

            var roles = await _userManager.GetRolesAsync(user);

            // we create an access token
            AccessTokenGenerationDto accessTokenGenerationDto = await _tokenService.GenerateAccessTokenAsync(user, roles);

            // we create a refresh token
            RefreshToken refreshToken = _tokenService.GenerateRefreshToken(accessTokenGenerationDto.JwtId, user.Id);

            await _tokenService.SaveRefreshToken(refreshToken);

            return Ok(new AuthResponseDto
            {
                AccessToken = accessTokenGenerationDto.AccessToken,
                RefreshToken = refreshToken.Token,
                AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(accessTokenGenerationDto.AccessTokenExpiryMinutes)
            });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            // unique email address
            if (await _userManager.FindByEmailAsync(request.Email) is not null)
                return BadRequest($"Email {request.Email} already exists !");

            if (await _userManager.FindByNameAsync(request.UserName) is not null)
                return BadRequest($"Username {request.UserName} already exists !");

            User user = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            return Ok($"User {request.Email} registered successfully.");
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
        {
            AuthResponseDto? authResponse = await _tokenService.RefreshToken(request.RefreshToken);

            if (authResponse is null)
                return BadRequest(new { message = "Invalid refresh token Or disabled User." });

            return Ok(authResponse);

        }
    }
}
