using myhealthcareapi.DataAccesLayers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public interface IDepartmentService
    {
        public Task<DepartmentEntity> GetDepartmentById(int id);
    }
}
