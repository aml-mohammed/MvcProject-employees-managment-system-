using Demo.BLL.Interfaces;
using Demo.DAL.Contextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reprositories
{
    public class UnitOfWork : IUnitOfWok,IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentReprository DepartmentReprository { get; set; }
        public MvcProjectDbContext _Dbcontxt { get; }

        public UnitOfWork(MvcProjectDbContext dbcontxt)
        {
            EmployeeRepository = new EmployeeRepository(dbcontxt);
            DepartmentReprository = new DepartmentReprository(dbcontxt);
            _Dbcontxt = dbcontxt;
        }

        public async Task<int> CompleteAsync()
        {
           return await _Dbcontxt.SaveChangesAsync();
        }

        public void Dispose()
        {
            _Dbcontxt.Dispose();
        }
    }
}