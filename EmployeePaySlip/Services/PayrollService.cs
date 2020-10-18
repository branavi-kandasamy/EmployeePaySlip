using System;
using EmployeePaySlip.Models;

namespace EmployeePaySlip.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly ITaxCalculatorService _taxCalculatorService;

        public PayrollService(ITaxCalculatorService taxCalculatorService)
        {
            _taxCalculatorService = taxCalculatorService;
        }

        public PaySlip GeneratePaySlip(Employee employee)
        {
            var paySlip = new PaySlip
            {
                Name = string.Concat(employee.FirstName, " ", employee.LastName),
                GrossIncome = Math.Round((decimal)employee.AnnualSalary / 12, MidpointRounding.AwayFromZero),
                IncomeTax = _taxCalculatorService.CalculateIncomeTax(employee.AnnualSalary),
                PayPeriod = employee.PayPeriod
            };
            paySlip.Super = Math.Round(paySlip.GrossIncome * employee.SuperRate);
            paySlip.NetIncome = paySlip.GrossIncome - paySlip.IncomeTax;
            return paySlip;
        }
    }
}
