using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contextes
{
    public class MvcProjectDbContext:IdentityDbContext<ApplicationUser> //DbContext
    {
        public MvcProjectDbContext(DbContextOptions<MvcProjectDbContext> options):base(options)
        {

        }
       // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       //=>
       //     optionsBuilder.UseSqlServer("Server = .; Database = MVCProjectDb; Trusted_Conecction=true; MultipleActiveResultSets=true");
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
