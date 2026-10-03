using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.RoomType;
using CheckMate.Application.Errors.RoomType;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeService(
            IUnitOfWork unitOfWork,
            IRoomTypeRepository roomTypeRepository
            )
        {
            _unitOfWork = unitOfWork;
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task<Result<RoomType>> GetRoomType(int Id)
        {
            RoomType? roomType = await _roomTypeRepository.Get(Id);

            if (roomType == null)
            {
                return Result<RoomType>.Failure(RoomTypeErrors.NotFound(Id));
            }

            return Result<RoomType>.Success(roomType);
        }

        public async Task<RoomType> CreateRoomType(RoomTypeCreationDto dto)
        {
            RoomType roomType = new RoomType
            {
                RoomTypeName = dto.Name,
                Rank = dto.Rank,
                MaxOccupancy = dto.MaxOccupancy,
                Description = dto.Description
            };

            await _roomTypeRepository.Create(roomType);

            await _unitOfWork.SaveChangesAsync();

            return roomType;
        }

        public async Task<IEnumerable<RoomType>> GetAllRoomTypes()
        {
            return await _roomTypeRepository.GetAll();
        }
    }
}
