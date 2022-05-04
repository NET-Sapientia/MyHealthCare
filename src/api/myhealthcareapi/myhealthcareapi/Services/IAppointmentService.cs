using myhealthcareapi.DataAccesLayers.Models;
using myhealthcareapi.Services.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public interface IAppointmentService
    {
        public Task<AppointmentServiceResponses> AddAppointment(AppointmentEntity Appointment);
        public Task<List<ClientAppointmentWithNamesEntity>> GetClientAppointments(int clientId);
        public Task<List<MedicAppointmentWithNamesEntity>> GetMedicAppointments(int medicId);

    }
}
