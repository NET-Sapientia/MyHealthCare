using Microsoft.EntityFrameworkCore;
using myhealthcareapi.DataAccesLayers;
using myhealthcareapi.DataAccesLayers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly Context _context;
        public HospitalService(Context context)
        {
            _context = context;
        }
        public async Task<List<HospitalEntity>> GetAllHospitals()
        {
            return await _context.Hospitals.ToListAsync();
        }

        public async Task<List<HospitalEntity>> GetHospitalsFromDepartments(List<DepartmentEntity> departments)
        {
            List<HospitalEntity> result = new List<HospitalEntity>();
            foreach (var dep in departments)
            {
                var hospital = await _context.Hospitals.FirstOrDefaultAsync(h => h.Id == dep.HospitalId);
                result.Add(hospital);
            }

            return Task.Run(() => result).Result;
        }
    }
}
