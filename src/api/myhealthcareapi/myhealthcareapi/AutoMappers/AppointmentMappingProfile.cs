using AutoMapper;
using myhealthcareapi.DataAccesLayers.Models;
using myhealthcareapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.AutoMappers
{
    public class AppointmentMappingProfile : Profile
    {
        public AppointmentMappingProfile()
        {
            CreateMap<AddAppointmentModel, AppointmentEntity>();
            CreateMap<ClientAppointmentWithNamesEntity, ClientAppointmentWithNames>();
            CreateMap<MedicAppointmentWithNamesEntity, MedicAppointmentWithNames>();
        }
    }
}
