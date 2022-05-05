using myhealthcareapi.DataAccesLayers.Models;
using myhealthcareapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public interface IDepartmentService
    {
        public Task<DepartmentEntity> GetDepartmentById(int id);
        public Task<List<DepartmentEntity>> GetDepartmentsByName(string departmentName);
        public Task<List<MedicNameWithId>> GetDepartmentsMedics(int departmentId);
    }
}
