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
            return await _context.Departments.Where(d => string.Equals(d.Name, departmentName)).ToListAsync();
        }

        public async Task<List<string>> GetDepartmentsMedics(int departmentId)
        {
            return await _context.MedicDepartments.Where(md => md.DepartmentId == departmentId)
                .Join(_context.Medics, md => md.MedicId, m => m.Id, (md, m) => new { M = m })
                .Select(m => m.M.Name).ToListAsync();
        }
    }
}
