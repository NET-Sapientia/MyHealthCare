using myhealthcareapi.DataAccesLayers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public interface IMedicService
    {
        public Task<MedicEntity> GetMedicByEmail(string email);
        public Task<MedicEntity> GetMedicById(int dd);
        public string GenerateJwtToken(MedicEntity user);
    }
}
