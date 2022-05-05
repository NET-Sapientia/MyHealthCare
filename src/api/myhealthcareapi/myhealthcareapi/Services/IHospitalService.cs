using myhealthcareapi.DataAccesLayers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public interface IHospitalService
    {
        public Task<List<HospitalEntity>> GetAllHospitals();

        public Task<List<HospitalEntity>> GetHospitalsFromDepartments(List<DepartmentEntity> departments);

        public Task<List<DepartmentEntity>> GetHospitalsDepartment(int hospitalId);
        public Task<HospitalEntity> GetHospitalById(int id);
    }
}
