using CheckMate.API.Helpers;
using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Client;
using CheckMate.Application.DTOs.Company;
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
        private readonly ICompanyService _companyService;
        private readonly IClientService _clientService;

        public ClientController(
            IPersonService personService,
            ICompanyService companyService,
            IClientService clientService
            )
        {
            _personService = personService;
            _companyService = companyService;
            _clientService = clientService;
        }

        [HttpPost("Persons")]
        [Authorize]
        public async Task<IActionResult> CreatePerson(PersonCreationDto requestDto)
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

        [HttpPost("Companies")]
        [Authorize]
        public async Task<IActionResult> CreateCompany(CompanyCreationDto requestDto)
        {
            Result<Company> result = await _companyService.CreateCompanyAsync(requestDto);

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
            Result<ClientDto> result = await _clientService.GetClientByIdAsync(id);

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
            IEnumerable<ClientDto> clients = await _clientService.GeAllClientsAsync();

            return Ok(clients);
        }
    }
}
