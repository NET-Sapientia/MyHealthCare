using Microsoft.EntityFrameworkCore;
using myhealthcareapi.DataAccesLayers;
using myhealthcareapi.DataAccesLayers.Models;
using myhealthcareapi.Services.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly Context _context;
        public AppointmentService(Context context)
        {
            _context = context;
        }
        public async Task<AppointmentServiceResponses> AddAppointment(AppointmentEntity Appointment)
        {
            _context.Appointments.Add(Appointment);

            try
            {
                await _context.SaveChangesAsync();
                return AppointmentServiceResponses.SUCCES;
            }
            catch
            {
                return AppointmentServiceResponses.EXCEPTION;
            }
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
