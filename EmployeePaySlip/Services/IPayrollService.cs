using EmployeePaySlip.Models;

namespace EmployeePaySlip.Services
{
    public interface IPayrollService
    {
        PaySlip GeneratePaySlip(Employee employee);
    }
}
