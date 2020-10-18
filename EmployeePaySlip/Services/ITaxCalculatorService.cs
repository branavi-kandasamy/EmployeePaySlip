using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePaySlip.Services
{
    public interface ITaxCalculatorService
    {
        decimal CalculateIncomeTax(int annualSalary);
    }
}
