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
    public class DepartmentsController : ControllerBase
    {

        private readonly IDepartmentService _departmentService;
        private IMapper _mapper;

        public DepartmentsController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{departmentId}")]

        public async Task<IActionResult> GetDepartmentsMedics(int departmentId)
        {
            try
            {
                if (await _departmentService.GetDepartmentById(departmentId) == null)
                    return NotFound(new BackEndResponse<object>(404, "Department not found"));

                return StatusCode(200, new BackEndResponse<List<string>>(200, "Success", await _departmentService.GetDepartmentsMedics(departmentId)));
            }
            catch(Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }
    }
}
