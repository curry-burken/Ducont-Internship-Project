using Employee_Details.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Details.Data
{
    public class EmployeeDBtype : DbContext
    {
        public EmployeeDBtype(DbContextOptions options) : base(options)
        {
        }
        public DbSet<RegisterTemplate> Register { get; set; }
    }
}
