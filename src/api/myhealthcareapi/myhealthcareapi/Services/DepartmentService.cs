using Microsoft.EntityFrameworkCore;
using myhealthcareapi.DataAccesLayers;
using myhealthcareapi.DataAccesLayers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly Context _context;
        public DepartmentService(Context context)
        {
            _context = context;
        }
        public async Task<DepartmentEntity> GetDepartmentById(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<DepartmentEntity>> GetDepartmentsByName(string departmentName)
        {
            return await _context.Departments.Where(d => String.Equals(d.Name, departmentName)).ToListAsync();
        }
    }
}
