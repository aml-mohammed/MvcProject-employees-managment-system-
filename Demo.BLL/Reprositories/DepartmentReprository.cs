using Demo.BLL.Interfaces;
using Demo.DAL.Contextes;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reprositories
{
    public class DepartmentReprository : GenericRepository<Department>, IDepartmentReprository
    {
      //  public MvcProjectDbContext _Conext { get; }
        public DepartmentReprository(MvcProjectDbContext conext):base(conext)
        {
         //   _Conext = conext;
        }


        //public int Add(Department department)
        //{
        //    _Conext.Add(department);
        // return _Conext.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _Conext.Remove(department);
        //    return _Conext.SaveChanges();
        //}

        //public IEnumerable<Department> GetAll()
        //{
        //    return _Conext.Departments.ToList();
        //}

        //public Department GetDepartmentById(int Id)
        //{
        //    return _Conext.Departments.Find(Id);
        //}

        //public int Update(Department department)
        //{
        //    _Conext.Update(department);
        //    return _Conext.SaveChanges();
        //}
    }
}
