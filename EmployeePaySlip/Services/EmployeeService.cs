using System.Collections.Generic;
using EmployeePaySlip.Models;
using EmployeePaySlip.Repository;

namespace EmployeePaySlip.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> LoadEmployees()
        {
            return _employeeRepository.LoadEmployees();
        }
    }
}
