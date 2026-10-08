using CheckMate.API.Helpers;
using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Person;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckMate.API.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IPersonService _personService;

        public ClientController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PersonCreationDto requestDto)
        {
            Result<Person> result = await _personService.CreatePersonAsync(requestDto);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Get), new { id = result.Value.ClientId }, result.Value);
            }
            else
            {
                return result.Error.Value.ToActionResult(this);
            }
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            Result<Person> result = await _personService.GetPersonByIdAsync(id);

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
            IEnumerable<Person> persons = await _personService.ListAllPersonsAsync();
            return Ok(persons);
        }
    }
}
