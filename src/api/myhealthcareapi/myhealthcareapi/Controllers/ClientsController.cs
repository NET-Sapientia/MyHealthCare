using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myhealthcareapi.DataAccesLayers.Models;
using myhealthcareapi.Models;
using myhealthcareapi.Services;
using myhealthcareapi.Services.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private readonly IClientService _clientService;
        private IMapper _mapper;

        public ClientsController(IClientService clientService, IMapper mapper)
        {
            _clientService= clientService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterClient(AddClientModel clientModel)
        {
            try
            {
                var client = new ClientEntity();
                if (clientModel == null)
                {
                    client = null;
                }
                else
                {
                    client.Id = 1;
                    client.Name = clientModel.Name;
                    client.Email = clientModel.Email;
                    client.Address = clientModel.Address;
                    client.Password = clientModel.Password;
                }
                //var client = _mapper.Map<ClientEntity>(clientModel);
                var result = await _clientService.AddClient(client);

                if(result == ClientServiceResponses.NULLPARAM)
                    return BadRequest(new BackEndResponse<object>(400, "Client to register can not be null"));

                if (result == ClientServiceResponses.EXCEPTION)
                    return StatusCode(500, new BackEndResponse<object>(500, "Client could not be added"));

                if(result == ClientServiceResponses.EMAILALREADYEXISTS)
                    return StatusCode(409, new BackEndResponse<object>(409, "Email already exists"));
                
                return StatusCode(200, new BackEndResponse<int>(200, "User added", client.Id));

            }

            catch(Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                var client = await _clientService.GetClientByEmail(loginModel.Email);

                if (client == null)
                    return NotFound(new BackEndResponse<object>(404, "User email not found"));

                if(!string.Equals(loginModel.Password, client.Password))
                    return NotFound(new BackEndResponse<object>(404, "Wrong password"));

                return StatusCode(200, new BackEndResponse<string>(200, "Success", _clientService.GenerateJwtToken(client)));

            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }

        [HttpGet]
        [Route("Client/{email}")]
        public async Task<IActionResult> GetClientByEmail(string email)
        {
            try
            {
                var client = await _clientService.GetClientByEmail(email);

                if (client == null)
                    return NotFound(new BackEndResponse<object>(404, "User email not found"));

                return StatusCode(200, new BackEndResponse<Client>(200, "Success", _mapper.Map<Client>(client)));

            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }




    }
}
