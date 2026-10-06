using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.RoomType;
using CheckMate.Application.Errors.RoomType;
using CheckMate.Application.Interfaces;
using CheckMate.Application.Services;
using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Tests.Application.Services
{
    public class RoomTypeTests
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly Mock<IRoomTypeRepository> _roomTypeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RoomTypeTests()
        {
            _roomTypeRepositoryMock = new Mock<IRoomTypeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _roomTypeService = new RoomTypeService(_unitOfWorkMock.Object, _roomTypeRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateRoomType_ShouldReturnError_WhenRoomTypeAlreadyExists()
        {
            // Arrange
            var roomTypeCreationDto = new RoomTypeCreationDto
            {
                Name = "Deluxe",
                Description = "A deluxe room"
            };
            _roomTypeRepositoryMock.Setup(repo => repo.VerifyRoomTypeExistenceFromName(roomTypeCreationDto.Name))
                .ReturnsAsync(true);

            // Act
            var result = await _roomTypeService.CreateRoomType(roomTypeCreationDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(RoomTypeErrors.AlreadyExists(roomTypeCreationDto.Name), result.Error);
        }

        [Fact]
        public async Task CreateRoomType_ShouldReturnSuccess_WhenInputsAreValid()
        {
            // Arrange
            var roomTypeCreationDto = new RoomTypeCreationDto
            {
                Name = "Deluxe",
                Description = "A deluxe room",
                Rank = 1,
                MaxOccupancy = 2
            };
            _roomTypeRepositoryMock.Setup(repo => repo.VerifyRoomTypeExistenceFromName(roomTypeCreationDto.Name))
                .ReturnsAsync(false);

            // Act
            var result = await _roomTypeService.CreateRoomType(roomTypeCreationDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            Assert.Equal(roomTypeCreationDto.Name, result.Value.RoomTypeName);
            Assert.Equal(roomTypeCreationDto.Description, result.Value.Description);
            Assert.Equal(roomTypeCreationDto.Rank, result.Value.Rank);
            Assert.Equal(roomTypeCreationDto.MaxOccupancy, result.Value.MaxOccupancy);
        }

        [Fact]
        public async Task GetRoomType_ShouldReturnError_WhenRoomTypeNotExists()
        {
            // Arrange
            int roomTypeId = 999; // Non-existent room type ID
            _roomTypeRepositoryMock.Setup(repo => repo.Get(roomTypeId))
                .ReturnsAsync((RoomType)null);

            // Act
            var result = await _roomTypeService.GetRoomType(roomTypeId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(RoomTypeErrors.NotFound(roomTypeId), result.Error);
        }

        [Fact]
        public async Task GetRoomType_ShouldReturnRoomType_WhenInputsAreValid()
        {
            // Arrange
            int RoomTypeId = 1;
            RoomType roomType = new RoomType
            {
                RoomTypeId = RoomTypeId,
                RoomTypeName = "Deluxe",
                Description = "A deluxe room",
                Rank = 1,
                MaxOccupancy = 2
            };
            _roomTypeRepositoryMock.Setup(rt => rt.Get(RoomTypeId))
                .ReturnsAsync(roomType);

            // Act
            Result<RoomType> result = await _roomTypeService.GetRoomType(RoomTypeId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            Assert.Equal(roomType.RoomTypeId, result.Value.RoomTypeId);
            Assert.Equal(roomType.RoomTypeName, result.Value.RoomTypeName);
            Assert.Equal(roomType.Description, result.Value.Description);
            Assert.Equal(roomType.Rank, result.Value.Rank);
            Assert.Equal(roomType.MaxOccupancy, result.Value.MaxOccupancy);
        }
    }
}
