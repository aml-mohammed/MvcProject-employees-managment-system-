using Demo.BLL.Interfaces;
using Demo.DAL.Contextes;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reprositories
{
    public class EmployeeRepository:GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MvcProjectDbContext _conext;

        public EmployeeRepository(MvcProjectDbContext conext) : base(conext)
        {
            _conext = conext;
        }

        public IQueryable<Employee> GetEmployeeByName(string name)
        {
            return _conext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(D=>D.Department);
          
        }

     
    }
}
