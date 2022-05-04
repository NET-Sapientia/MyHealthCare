using myhealthcareapi.DataAccesLayers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public interface IFeedbackService
    {
        public Task<List<FeedBackEntity>> GetAllClientFeedback(int clientId);

        public Task<List<FeedBackEntity>> GetAllMedicFeedback(int medicId);
    }
}
