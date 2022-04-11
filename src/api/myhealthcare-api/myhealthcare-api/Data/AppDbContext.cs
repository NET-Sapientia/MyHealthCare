using Microsoft.EntityFrameworkCore;
using myhealthcare_api.Data.Models;

namespace myhealthcare_api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Hospital> Hospitals { get; set; }
    }
}
