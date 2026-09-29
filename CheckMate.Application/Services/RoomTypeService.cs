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

        public Task<RoomType?> GetRoomType(int Id)
        {
            return _roomTypeRepository.Get(Id);
        }

        public Task CreateRoomType()
        {
            throw new NotImplementedException();
        }
    }
}
