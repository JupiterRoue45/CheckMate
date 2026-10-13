using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.RoomUnavailability;
using CheckMate.Application.Errors.Room;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Identity;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CheckMate.Application.Services
{
    public class RoomUnavailabilityService : IRoomUnavailabilityService
    {
        private readonly IRoomUnavailabilityRepository _roomUnavailabilityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomRepository _roomRepository;

        public RoomUnavailabilityService(
            IRoomUnavailabilityRepository roomUnavailabilityRepository,
            IUnitOfWork unitOfWork,
            IRoomRepository roomRepository)
        {
            _roomUnavailabilityRepository = roomUnavailabilityRepository;
            _unitOfWork = unitOfWork;
            _roomRepository = roomRepository;
        }
        public async Task<Result<RoomUnavailabilityDto>> CreateRoomUnavailabilityAsync(RoomUnavailabilityCreationDto dto, User user)
        {
            Room? room = await _roomRepository.GetRoomFromRoomNumberAsync(dto.RoomNumber);

            if (room is null)
                return Result<RoomUnavailabilityDto>.Failure(RoomErrors.InexistantRoomNumber(dto.RoomNumber));

            RoomUnavailability roomUnavailability = new()
            {
                RoomId = room.RoomId,
                Reason = dto.Reason,
                Comment = dto.Comment,
                StartingDate = dto.StartingDate,
                EndingDate = dto.EndingDate,
                UserId = user.Id
            };

            await _roomUnavailabilityRepository.CreateAsync(roomUnavailability);

            await _unitOfWork.SaveChangesAsync();

            RoomUnavailabilityDto roomUnavailabilityDto = new()
            {
                RoomNumber = dto.RoomNumber,
                StartingDate = dto.StartingDate,
                EndingDate = dto.EndingDate,
                Reason = dto.Reason,
                Comment = dto.Comment,
                CreatedAt = roomUnavailability.CreatedAt
            };

            return Result<RoomUnavailabilityDto>.Success(roomUnavailabilityDto);
        }

        public async Task<IEnumerable<RoomUnavailabilityDto>> GetUnavailableRoomsBetweenTwoDates(DateOnly start, DateOnly end)
        {
            IEnumerable<RoomUnavailability> unavailabilities = await _roomUnavailabilityRepository.GetBetweenTwoDateAsync(start, end);

            return unavailabilities.Select(ru => GetDtoFromModel(ru));
        }

        private RoomUnavailabilityDto GetDtoFromModel(RoomUnavailability roomUnavailability)
        {
            return new RoomUnavailabilityDto()
            {
                RoomNumber = roomUnavailability.Room.RoomNumber,
                Comment = roomUnavailability.Comment,
                Reason = roomUnavailability.Reason,
                StartingDate = roomUnavailability.StartingDate,
                EndingDate = roomUnavailability.EndingDate,
                CreatedAt = roomUnavailability.CreatedAt
            };
        }

    }
}
