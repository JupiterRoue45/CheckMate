using CheckMate.Application.DTOs.Room;
using CheckMate.Application.Errors.Room;
using CheckMate.Application.Services;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Tests.Application.Services
{
    public class RoomServiceTests
    {
        private readonly RoomService _roomService;
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly Mock<IRoomTypeRepository> _roomTypeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RoomServiceTests()
        {
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _roomTypeRepositoryMock = new Mock<IRoomTypeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _roomService = new RoomService(
                _roomRepositoryMock.Object,
                _roomTypeRepositoryMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task CreateRoom_ShouldReturnError_WhenRoomTypeDoesNotExist()
        {
            // Arrange
            var roomCreationDto = new RoomCreationDto
            {
                Number = "101",
                RoomTypeId = 999 // Non-existent room type ID
            };
            _roomTypeRepositoryMock.Setup(repo => repo.VerifyRoomTypeExistence(roomCreationDto.RoomTypeId))
                .ReturnsAsync(false);

            // Act
            var result = await _roomService.CreateRoom(roomCreationDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(RoomErrors.InexistantRoomType(roomCreationDto.RoomTypeId), result.Error);
        }

        [Fact]
        public async Task CreateRoom_ShouldReturnError_WhenRoomNumberAlreadyExists()
        {
            // Arrange
            var roomCreationDto = new RoomCreationDto
            {
                Number = "101",
                RoomTypeId = 1 // Assume this room type exists
            };
            _roomTypeRepositoryMock.Setup(repo => repo.VerifyRoomTypeExistence(roomCreationDto.RoomTypeId))
                .ReturnsAsync(true);
            _roomRepositoryMock.Setup(repo => repo.VerifyRoomExistenceFromRoomNumberAsync(roomCreationDto.Number))
                .ReturnsAsync(true);

            // Act
            var result = await _roomService.CreateRoom(roomCreationDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(RoomErrors.AlreadyExists(roomCreationDto.Number), result.Error);
        }

        [Fact]
        public async Task CreateRoom_ShouldCreateRoom_WhenValidInput()
        {
            // Arrange
            var roomCreationDto = new RoomCreationDto
            {
                Number = "101",
                Floor = 1,
                Area = 20.5m,
                RoomTypeId = 1 
            };
            _roomTypeRepositoryMock.Setup(repo => repo.VerifyRoomTypeExistence(roomCreationDto.RoomTypeId))
                .ReturnsAsync(true);
            _roomRepositoryMock.Setup(repo => repo.VerifyRoomExistenceFromRoomNumberAsync(roomCreationDto.Number))
                .ReturnsAsync(false);

            // Act
            var result = await _roomService.CreateRoom(roomCreationDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(roomCreationDto.Number, result.Value.RoomNumber);
            Assert.Equal(roomCreationDto.Floor, result.Value.Floor);
            Assert.Equal(roomCreationDto.Area, result.Value.Area);
            Assert.Equal(roomCreationDto.RoomTypeId, result.Value.RoomTypeId);
        }
    }
}
