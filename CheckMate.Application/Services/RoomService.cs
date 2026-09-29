using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
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
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(
            IRoomRepository roomRepository,
            IUnitOfWork unitOfWork
            )
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _roomRepository.GetAll();
        }

        public async Task<Room?> GetRoom(int Id)
        {
            return await _roomRepository.Get(Id);
        }
    }
}
