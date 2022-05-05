using Microsoft.EntityFrameworkCore;
using myhealthcareapi.DataAccesLayers;
using myhealthcareapi.DataAccesLayers.Models;
using myhealthcareapi.Models;
using myhealthcareapi.Services.ServiceResponses;
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

        public async Task<FeedbackServiceResponses> AddFeedBack(FeedBackEntity feedBack)
        {
            if (await _context.Appointments.FirstOrDefaultAsync(a => a.Id == feedBack.AppointmentId) == null)
                return FeedbackServiceResponses.NOTFOUND;

            _context.FeedBacks.Add(feedBack);
            try
            {
                await _context.SaveChangesAsync();
                return FeedbackServiceResponses.SUCCESS;
            }
            catch
            {
                return FeedbackServiceResponses.EXCEPTION;
            }
        }

        public async Task<List<UserFeedback>> GetAllClientFeedback(int clientId)
        {
            var list = await _context.FeedBacks.Where(fd => fd.Appointment.ClientId == clientId).ToListAsync();
            var result = new List<UserFeedback>();
            var appointments = await GetClientAppointments(clientId);
            foreach (var l in list)
            {
                var appointment = appointments.Where(a => a.Id == l.AppointmentId).FirstOrDefault();
                result.Add(new UserFeedback { Appointment = new ClientAppointmentWithNames { MedicName = appointment.MedicName, DepartmentName = appointment.DepartmentName, EndDate = appointment.EndDate, StartDate = appointment.StartDate, Id = appointment.Id, Notes = appointment.Notes }, Billing = l.Billing, Id = l.Id, Message = l.Message });
            }

            return Task.Run(() => result).Result;
        }

        public async Task<List<MedicFeedback>> GetAllMedicFeedback(int medicId)
        {
            var list = await _context.FeedBacks.Where(fd => fd.Appointment.MedicId == medicId).ToListAsync();
            var result = new List<MedicFeedback>();
            var appointments = await GetMedicAppointments(medicId);
            foreach (var l in list)
            {
                var appointment = appointments.Where(a => a.Id == l.AppointmentId).FirstOrDefault();
                result.Add(new MedicFeedback { Appointment = new MedicAppointmentWithNames { ClientName = appointment.ClientName, DepartmentName = appointment.DepartmentName, EndDate = appointment.EndDate, StartDate = appointment.StartDate, Id = appointment.Id, Notes = appointment.Notes }, Billing = l.Billing, Id = l.Id, Message = l.Message });
            }

            return Task.Run(() => result).Result;
        }

        public async Task<List<ClientAppointmentWithNamesEntity>> GetClientAppointments(int clientId)
        {
            var list = _context.Appointments as IQueryable<AppointmentEntity>;
            return await list.Where(a => a.ClientId == clientId).Join(_context.Medics, a => a.MedicId, m => m.Id, (a, m) => new { A = a, M = m })
                .Join(_context.Departments, a3 => a3.A.DepartmentId, d => d.Id, (a3, d) => new ClientAppointmentWithNamesEntity { DepartmentName = d.Name, EndDate = a3.A.EndDate, StartDate = a3.A.StartDate, Id = a3.A.Id, MedicName = a3.M.Name, Notes = a3.A.Notes }).ToListAsync();
        }


        public async Task<List<MedicAppointmentWithNamesEntity>> GetMedicAppointments(int medicId)
        {
            var list = _context.Appointments as IQueryable<AppointmentEntity>;
            return await list.Where(a => a.MedicId == medicId).Join(_context.Clients, a => a.ClientId, c => c.Id, (a, c) => new { A = a, C = c })
                .Join(_context.Departments, a3 => a3.A.DepartmentId, d => d.Id, (a3, d) => new MedicAppointmentWithNamesEntity { DepartmentName = d.Name, EndDate = a3.A.EndDate, StartDate = a3.A.StartDate, Id = a3.A.Id, ClientName = a3.C.Name, Notes = a3.A.Notes }).ToListAsync();
        }

    }
}
