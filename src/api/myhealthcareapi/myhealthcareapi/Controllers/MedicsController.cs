using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myhealthcareapi.Models;
using myhealthcareapi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicsController : ControllerBase
    {
        private readonly IMedicService _medicService;
        private IMapper _mapper;

        public MedicsController(IMedicService medicService, IMapper mapper)
        {
            _medicService = medicService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                var client = await _medicService.GetMedicByEmail(loginModel.Email);

                if (client == null)
                    return NotFound(new BackEndResponse<object>(404, "Medic email not found"));

                if (!string.Equals(loginModel.Password, client.Password))
                    return NotFound(new BackEndResponse<object>(404, "Wrong password"));

                return StatusCode(200, new BackEndResponse<string>(200, "Success", _medicService.GenerateJwtToken(client)));

            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }
    }
}
