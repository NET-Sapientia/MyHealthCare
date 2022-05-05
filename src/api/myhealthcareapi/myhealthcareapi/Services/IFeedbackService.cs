using myhealthcareapi.DataAccesLayers.Models;
using myhealthcareapi.Models;
using myhealthcareapi.Services.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public interface IFeedbackService
    {
        public Task<List<UserFeedback>> GetAllClientFeedback(int clientId);

        public Task<List<MedicFeedback>> GetAllMedicFeedback(int medicId);

        public Task<List<ClientAppointmentWithNamesEntity>> GetClientAppointments(int clientId);
        public Task<List<MedicAppointmentWithNamesEntity>> GetMedicAppointments(int medicId);

        public Task<FeedbackServiceResponses> AddFeedBack(FeedBackEntity feedBack);
    }
}
