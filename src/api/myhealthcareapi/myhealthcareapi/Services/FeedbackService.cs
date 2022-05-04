using Microsoft.EntityFrameworkCore;
using myhealthcareapi.DataAccesLayers;
using myhealthcareapi.DataAccesLayers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly Context _context;
        public FeedbackService(Context context)
        {
            _context = context;
        }
        public async Task<List<FeedBackEntity>> GetAllClientFeedback(int clientId)
        {
            return await _context.FeedBacks.Where(fd => fd.Appointment.ClientId == clientId).ToListAsync();
        }

        public async Task<List<FeedBackEntity>> GetAllMedicFeedback(int medicId)
        {
            return await _context.FeedBacks.Where(fd => fd.Appointment.MedicId == medicId).ToListAsync();
        }
    }
}
