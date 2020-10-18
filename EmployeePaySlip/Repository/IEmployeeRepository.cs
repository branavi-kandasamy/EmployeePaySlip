using System;
using System.Collections.Generic;
using System.Text;
using EmployeePaySlip.Models;

namespace EmployeePaySlip.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> LoadEmployees();
    }
}
