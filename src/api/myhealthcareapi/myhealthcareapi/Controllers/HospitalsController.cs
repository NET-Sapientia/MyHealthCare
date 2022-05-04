using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myhealthcareapi.DataAccesLayers.Models;
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
    public class HospitalsController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;
        private readonly IDepartmentService _departmentService;
        private IMapper _mapper;

        public HospitalsController(IHospitalService hospitalService, IMapper mapper, IDepartmentService departmentService)
        {
            _hospitalService = hospitalService;
            _mapper = mapper;
            _departmentService = departmentService;
        }

        [HttpGet]
        [Route("Hospitals")]
        public async Task<IActionResult> GetAllHospitals()
        {
            try
            {
                var hospitals = await _hospitalService.GetAllHospitals();

                return StatusCode(200, new BackEndResponse<List<Hospital>>(200, "Success", _mapper.Map<List<Hospital>>(hospitals)));
            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }

        [HttpGet]
        [Route("Hospitals/{departmentName}")]
        public async Task<IActionResult> GetHospitalsByDepartmentName(string departmentName)
        {
            try
            {
                var departments = await _departmentService.GetDepartmentsByName(departmentName);

                var result = await _hospitalService.GetHospitalsFromDepartments(departments);

                return StatusCode(200, new BackEndResponse<List<Hospital>>(200, "Success", _mapper.Map<List<Hospital>>(result)));

            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }
    }
}
