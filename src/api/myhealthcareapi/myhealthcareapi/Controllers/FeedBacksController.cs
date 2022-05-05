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
    public class FeedBacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IMedicService _medicService;
        private readonly IClientService _clientService;

        private IMapper _mapper;

        public FeedBacksController(IFeedbackService feedbackService, IMedicService medicService, IClientService clientService, IMapper mapper)
        {
            _feedbackService = feedbackService;
            _medicService = medicService;
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Client/{clientId}")]
        public async Task<IActionResult> GetAllClientFeedback(int clientId)
        {
            try
            {
                var client = await _clientService.GetClientById(clientId);

                if (client == null)
                    return NotFound(new BackEndResponse<object>(404, "Client not found"));

                var feedbacks = await _feedbackService.GetAllClientFeedback(clientId);

                return StatusCode(200, new BackEndResponse<List<UserFeedback>>(200, "Success", feedbacks));
            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }

        [HttpGet]
        [Route("Medic/{medicId}")]
        public async Task<IActionResult> GetAllMedicFeedback(int medicId)
        {
            try
            {
                var medic = await _medicService.GetMedicById(medicId);

                if (medic == null)
                    return NotFound(new BackEndResponse<object>(404, "Medic not found"));

                var feedbacks = await _feedbackService.GetAllMedicFeedback(medicId);

                return StatusCode(200, new BackEndResponse<List<MedicFeedback>>(200, "Success", feedbacks));
            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }

        [HttpPost]

        public async Task<IActionResult> AddFeedback(AddFeedbackModel addFeedbackModel)
        {
            try
            {
                var feedback = _mapper.Map<FeedBackEntity>(addFeedbackModel);

                var result = await _feedbackService.AddFeedBack(feedback);

                if (result == FeedbackServiceResponses.NOTFOUND)
                    return NotFound(new BackEndResponse<object>(404, "Appointment not found"));

                if (result == FeedbackServiceResponses.EXCEPTION)
                    return StatusCode(500, new BackEndResponse<object>(500, "Feedback could not be added"));

                return StatusCode(201, new BackEndResponse<int>(201, "Feedback added", feedback.Id));
            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }
    }
}
