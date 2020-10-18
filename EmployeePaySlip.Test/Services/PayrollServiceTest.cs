using EmployeePaySlip.Models;
using EmployeePaySlip.Services;
using NSubstitute;
using Xunit;

namespace EmployeePaySlip.Test.Services
{
    public class PayrollServiceTest
    {
        [Fact]
        public void Should_Return_Payslip()
        {
            // Arrange
            var taxCalculatorService = Substitute.For<ITaxCalculatorService>();

            taxCalculatorService.CalculateIncomeTax(Arg.Any<int>()).Returns(90);
            var employee = new Employee()
            {
                FirstName = "Abc",
                LastName = "Test",
                AnnualSalary = 8000,
                PayPeriod = "Jan-Dec",
                SuperRate = 0.09m
            };
            // Act
            var payrollService = new PayrollService(taxCalculatorService);
            var paySlip = payrollService.GeneratePaySlip(employee);

            // Assert
            Assert.NotNull(paySlip);
            Assert.Equal(employee.FirstName+ ' ' + employee.LastName, paySlip.Name);
            Assert.Equal(90, paySlip.IncomeTax);
            Assert.Equal(667, paySlip.GrossIncome);
            Assert.Equal(60, paySlip.Super);
            Assert.Equal(577, paySlip.NetIncome);
        }
    }
}
