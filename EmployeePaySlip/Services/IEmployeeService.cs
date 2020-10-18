using System.Collections.Generic;
using EmployeePaySlip.Models;

namespace EmployeePaySlip.Services
{
    interface IEmployeeService
    {
        List<Employee> LoadEmployees();
    }
}
