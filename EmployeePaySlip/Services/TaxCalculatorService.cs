using System;
using System.Collections.Generic;
using System.Linq;
using EmployeePaySlip.Models;
using EmployeePaySlip.Repository;

namespace EmployeePaySlip.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        private readonly ITaxDataRepository _taxDataRepository;

        private List<TaxData> _taxData;
        public TaxCalculatorService(ITaxDataRepository taxDataRepository)
        {
            _taxDataRepository = taxDataRepository;
        }

        public decimal CalculateIncomeTax(int annualSalary)
        {
            if (_taxData == null)
            {
                _taxData = _taxDataRepository.LoadTaxData();
            }

            if (annualSalary < 0)
            {
                throw new Exception("Invalid annual salary");
            }

            var taxRate = _taxData.SingleOrDefault(c => (c.MinLimit == 0 || annualSalary > c.MinLimit)
                                                                            && (c.MaxLimit == -1 || annualSalary <= c.MaxLimit));

            if (taxRate == null)
            {
                throw new Exception("Tax detail is not defined");
            }

            var incomeTax = (taxRate.BaseTax + (annualSalary - taxRate.MinLimit) * taxRate.TaxRate) / 12;

            return Math.Round(incomeTax, MidpointRounding.AwayFromZero);
        }
    }
}
