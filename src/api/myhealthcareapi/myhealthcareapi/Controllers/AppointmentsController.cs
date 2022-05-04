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
    public class AppointmentsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IDepartmentService _departmentService;
        private readonly IMedicService _medicService;
        private readonly IAppointmentService _appointmentService;
        private IMapper _mapper;

        public AppointmentsController(IClientService clientService, IDepartmentService departmentService, IMedicService medicService, IAppointmentService appointmentService, IMapper mapper)
        {
            _clientService = clientService;
            _appointmentService = appointmentService;
            _medicService = medicService;
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> MakeAppointment(AddAppointmentModel addAppointmentModel)
        {
            try
            {
                var client = await _clientService.GetClientById(addAppointmentModel.ClientId);
                if (client == null)
                {
                    return NotFound(new BackEndResponse<object>(404, "Client not found"));
                }

                var department = await _departmentService.GetDepartmentById(addAppointmentModel.DepartmentId);
                if (department == null)
                {
                    return NotFound(new BackEndResponse<object>(404, "Department not found"));
                }

                var medic = await _medicService.GetMedicById(addAppointmentModel.MedicId);
                if (medic == null)
                {
                    return NotFound(new BackEndResponse<object>(404, "Medic not found"));
                }

                var appointmentEntity = _mapper.Map<AppointmentEntity>(addAppointmentModel);
                if (await _appointmentService.AddAppointment(appointmentEntity) == Services.ServiceResponses.AppointmentServiceResponses.EXCEPTION)
                    return StatusCode(500, new BackEndResponse<object>(500, "Appointment could not be added"));

                Appointment appointment = new Appointment { ClientName = client.Name, DepartmentName = department.Name, MedicName = medic.Name, Id = appointmentEntity.Id, EndDate = appointmentEntity.EndDate, StartDate = appointmentEntity.StartDate, Notes = appointmentEntity.Notes };
                return StatusCode(201, new BackEndResponse<Appointment>(200, "Appointment added", appointment));
            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }

        [HttpGet]
        [Route("ClientAppointments/{clientId}")]

        public async Task<IActionResult> GetClientAppointments(int clientId)
        {
            var client = await _clientService.GetClientById(clientId);
            if (client == null)
                return NotFound(new BackEndResponse<object>(404, "Client not found"));

            var result = await _appointmentService.GetClientAppointments(clientId);
            
            return StatusCode(200, new BackEndResponse<List<ClientAppointmentWithNames>>(200, "Success", _mapper.Map<List<ClientAppointmentWithNames>>(result)));

        }

        [HttpGet]
        [Route("MedicAppointments/{medicId}")]

        public async Task<IActionResult> GetMedicAppointments(int medicId)
        {
            var medic = await _medicService.GetMedicById(medicId);
            if (medic == null)
                return NotFound(new BackEndResponse<object>(404, "Medic not found"));

            var result = await _appointmentService.GetMedicAppointments(medicId);

            return StatusCode(200, new BackEndResponse<List<MedicAppointmentWithNames>>(200, "Success", _mapper.Map<List<MedicAppointmentWithNames>>(result)));

        }

    }
}
