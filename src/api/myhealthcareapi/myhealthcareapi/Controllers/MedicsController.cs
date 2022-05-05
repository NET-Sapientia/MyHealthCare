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
                var medic = await _medicService.GetMedicByEmail(loginModel.Email);

                if (medic == null)
                    return NotFound(new BackEndResponse<object>(404, "Medic email not found"));

                if (!string.Equals(loginModel.Password, medic.Password))
                    return NotFound(new BackEndResponse<object>(404, "Wrong password"));

                return StatusCode(200, new BackEndResponse<MedicWithToken>(200, "Success", new MedicWithToken { Email = medic.Email, Id = medic.Id, Name = medic.Name, Token = _medicService.GenerateJwtToken(medic) }));

            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }
    }
}
