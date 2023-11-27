﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitOfWok
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentReprository DepartmentReprository { get; set; }
       Task<int> CompleteAsync();
        void Dispose();
    }
}
