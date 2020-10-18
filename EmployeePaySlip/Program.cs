using System;
using System.Collections.Generic;
using System.Linq;
using EmployeePaySlip.Config;
using EmployeePaySlip.Models;
using EmployeePaySlip.Repository;
using EmployeePaySlip.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeePaySlip
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        private static void Main()
        {
            SetupServices();

            var employeeService = _serviceProvider.GetService<IEmployeeService>();
            var paySlipGenerator = _serviceProvider.GetService<IPaySlipGenerator>();

            var employeeDetails = employeeService.LoadEmployees();
          
            var paySlips = GetPaySlips(employeeDetails);

            var isSuccess = paySlipGenerator.PrintPaySlip(paySlips);
            if (isSuccess)
            {
                Console.WriteLine("Payslips are generated successfully");
            }
        }

        private static List<PaySlip> GetPaySlips(IEnumerable<Employee> employeeDetails)
        {
            var payrollService = _serviceProvider.GetService<IPayrollService>();

            return employeeDetails.Select(employee => payrollService.GeneratePaySlip(employee)).ToList();
        }

        private static void SetupServices()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile($"appSettings.json", optional: true, reloadOnChange: true)
                .Build();

            _serviceProvider = new ServiceCollection()
                .AddSingleton(context => configuration)
                .AddSingleton<IEmployeeService, EmployeeService>()
                .AddSingleton<IPayrollService, PayrollService>()
                .AddSingleton<ITaxCalculatorService, TaxCalculatorService>()
                .AddSingleton<IPaySlipGenerator, PaySlipGenerator>()
                .AddSingleton<IEmployeeRepository, EmployeeRepository>()
                .AddSingleton<IFileConfig, FileConfig>()
                .AddSingleton<ITaxDataRepository, TaxDataRepository>().BuildServiceProvider();
        }
    }
}
