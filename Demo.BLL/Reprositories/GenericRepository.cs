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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MvcProjectDbContext _dbconext;

        public GenericRepository(MvcProjectDbContext dbconext)
        {
           _dbconext = dbconext;
        }
        public async Task Add(T item)
        {
          await  _dbconext.AddAsync(item);
         //   return _dbconext.SaveChanges();
        }

        public void Delete(T item)
        {
            _dbconext.Remove(item);
           // return _dbconext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T)==typeof(Employee))
            {
              return (IEnumerable<T>) await _dbconext.Employees.Include(E => E.Department).ToListAsync();
            }
        return  await   _dbconext.Set<T>().ToListAsync();
           
        }
        
          
        

        public async Task<T> GetById(int Id)
        
         => await  _dbconext.Set<T>().FindAsync(Id);
        

        public void Update(T item)
        {
            _dbconext.Update(item);
           // return _dbconext.SaveChanges();
        }
    }
}
