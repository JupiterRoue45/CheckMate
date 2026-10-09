using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Address;
using CheckMate.Application.Errors.Address;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IAddressRepository addressRepository, ICountryRepository countryRepository, IUnitOfWork unitOfWork)
        {
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Address>> CreateAddressAsync(AddressCreationDto dto)
        {
            if (!await _countryRepository.VerifyCountryExistance(dto.CountryId.Value))
                return Result<Address>.Failure(AddressErrors.InexistantCountry());

            Address address = new()
            {
                StreetName = dto.Street,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CountryId = dto.CountryId.Value
            };

            await _addressRepository.CreateAsync(address);

            await _unitOfWork.SaveChangesAsync();

            return Result<Address>.Success(address);
        }

        public async Task<Result> DeleteAddressAsync(int id)
        {
            bool deleted = await _addressRepository.DeleteAsync(id);

            if (!deleted)
                return Result.Failure(AddressErrors.AddressNotFound(id));

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result<Address>> GetAddressByIdAsync(int id)
        {
            Address? address = await _addressRepository.GetByIdAsync(id);

            if (address is null)
                return Result<Address>.Failure(AddressErrors.AddressNotFound(id));

            return Result<Address>.Success(address);
        }

        public async Task<IEnumerable<Address>> GetAddressesByLabel(string label)
        {
            return await _addressRepository.GetByLabelAsync(label);
        }

        public async Task<IEnumerable<Address>> GetAllAddresses()
        {
            return await _addressRepository.GetAllAsync();
        }

        public async Task<Result<Address>> UpdateAddressAsync(int id, AddressUpdateDto dto)
        {
            bool isCountryValid = await _countryRepository.VerifyCountryExistance(dto.CountryId.Value);

            if(!isCountryValid)
                return Result<Address>.Failure(AddressErrors.InexistantCountry( ));

            Address? address = await _addressRepository.GetByIdAsync(id);

            if (address is null)
                return Result<Address>.Failure(AddressErrors.AddressNotFound(id));

            address.AddressLabel = dto.Label;
            address.StreetName = dto.Street;
            address.AddressNumber = dto.Number;
            address.City = dto.City;
            address.State = dto.State;
            address.ZipCode = dto.ZipCode;
            address.CountryId = dto.CountryId.Value;

            await _unitOfWork.SaveChangesAsync();

            return Result<Address>.Success(address);
        }
    }
}
