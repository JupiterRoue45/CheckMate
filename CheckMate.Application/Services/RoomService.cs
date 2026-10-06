using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Room;
using CheckMate.Application.Errors.Room;
using CheckMate.Application.Errors.RoomType;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using CheckMate.Domain.Enums;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(
            IRoomRepository roomRepository,
            IRoomTypeRepository roomTypeRepository,
            IUnitOfWork unitOfWork
            )
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _roomTypeRepository = roomTypeRepository;
        }
        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Result<Room>> GetRoom(int Id)
        {
            var room = await _roomRepository.GetByIdAsync(Id);

            if (room == null)
                return Result<Room>.Failure(RoomErrors.NotFound(Id));

            return Result<Room>.Success(room);
        }

        public async Task<Result<Room>> CreateRoom(RoomCreationDto dto)
        {
            // We check if the room type exists
            bool roomTypeExists = await _roomTypeRepository.VerifyRoomTypeExistence(dto.RoomTypeId);

            if (!roomTypeExists)
                return Result<Room>.Failure(RoomErrors.InexistantRoomType(dto.RoomTypeId));

            // We check if there is no room with the same number
            bool roomNumberExists = await _roomRepository.VerifyRoomExistenceFromRoomNumberAsync(dto.Number);

            if (roomNumberExists)
                return Result<Room>.Failure(RoomErrors.AlreadyExists(dto.Number));

            Room room = new Room
            {
                RoomNumber = dto.Number,
                Floor = dto.Floor,
                Area = dto.Area,
                RoomStatus = RoomStatusEnum.Clean,
                RoomTypeId = dto.RoomTypeId
            };

            await _roomRepository.CreateAsync(room);

            await _unitOfWork.SaveChangesAsync();

            return Result<Room>.Success(room);
        }
    }
}
