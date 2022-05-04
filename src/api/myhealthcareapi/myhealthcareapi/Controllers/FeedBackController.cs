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
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private IMapper _mapper;

        public FeedBackController(IFeedbackService feedbackService, IMapper mapper)
        {
            _feedbackService = feedbackService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Client/{id}")]
        public async Task<IActionResult> GetAllClientFeedback(int clientId)
        {
            try
            {
                var hospitals = await _feedbackService.GetAllClientFeedback(clientId);

                return StatusCode(200, new BackEndResponse<List<Hospital>>(200, "Success", _mapper.Map<List<Hospital>>(hospitals)));
            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }

        [HttpGet]
        [Route("Medic/{id}")]
        public async Task<IActionResult> GetAllMedicFeedback(int medicId)
        {
            try
            {
                var hospitals = await _feedbackService.GetAllMedicFeedback(medicId);

                return StatusCode(200, new BackEndResponse<List<Hospital>>(200, "Success", _mapper.Map<List<Hospital>>(hospitals)));
            }

            catch (Exception ex)
            {
                return StatusCode(500, new BackEndResponse<object>(500, ex.Message));
            }
        }
    }
}
