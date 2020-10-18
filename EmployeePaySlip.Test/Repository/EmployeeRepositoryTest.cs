using System;
using EmployeePaySlip.Config;
using EmployeePaySlip.Repository;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace EmployeePaySlip.Test.Repository
{
    public class EmployeeRepositoryTest
    {
        private EmployeeRepository _employeeRepository;

        [Fact]
        public void Should_Throw_Exception_When_EmployeeInputFile_Is_Empty()
        {
            var config = new ConfigurationBuilder().Build();
            var fileConfig = new FileConfig(config) { EmployeeInputFile = "" };

            _employeeRepository = new EmployeeRepository(fileConfig);

            var exception = Assert.Throws<Exception>(() => _employeeRepository.LoadEmployees());
            Assert.Equal("Employee input file path is not setup in config", exception.Message);
        }

        [Fact]
        public void Should_Return_TaxDetails_When_EmployeeInputFile_Is_Defined()
        {
            var config = new ConfigurationBuilder().Build();
            var fileConfig = new FileConfig(config) { EmployeeInputFile = "EmployeeInput.csv" };

            _employeeRepository = new EmployeeRepository(fileConfig);

            var employeeDetails = _employeeRepository.LoadEmployees();

            Assert.NotNull(employeeDetails);
            Assert.Equal(2, employeeDetails.Count);
        }

        [Fact]
        public void Should_Return_TaxDetails_When_EmployeeInputFile_Is_Invalid()
        {
            var config = new ConfigurationBuilder().Build();
            var fileConfig = new FileConfig(config) { EmployeeInputFile = "TaxData1.csv" };

            _employeeRepository = new EmployeeRepository(fileConfig);

            var exception = Assert.Throws<Exception>(() => _employeeRepository.LoadEmployees());
            Assert.Equal("Specified input file is not found", exception.Message);
        }
    }
}
