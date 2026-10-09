using CheckMate.API.Helpers;
using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Address;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckMate.API.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AddressCreationDto requestDto)
        {
            Result<Address> result = await _addressService.CreateAddressAsync(requestDto);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Get), new { id = result.Value.AddressId }, result.Value);
            }
            else
            {
                return result.Error.Value.ToActionResult(this);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            Result<Address> result = await _addressService.GetAddressByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return result.Error.Value.ToActionResult(this);
            }
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] string label)
        {
            IEnumerable<Address> addresses = await _addressService.GetAddressesByLabel(label);

            return Ok(addresses);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _addressService.DeleteAddressAsync(id);

            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return result.Error.Value.ToActionResult(this);
            }
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, AddressUpdateDto dto)
        {
            Result<Address> result = await _addressService.UpdateAddressAsync(id, dto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return result.Error.Value.ToActionResult(this);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Address> addresses = await _addressService.GetAllAddresses();

            return Ok(addresses);
        }
    }
}
