using System;
using System.Collections.Generic;
using EmployeePaySlip.Config;
using EmployeePaySlip.Models;
using EmployeePaySlip.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace EmployeePaySlip.Test.Services
{
    public class PaySlipGeneratorTest
    {
        private PaySlipGenerator _paySlipGenerator;

        [Fact]
        public void Should_Throw_Exception_When_PaySlipOutputFile_Is_Empty()
        {
            var config = new ConfigurationBuilder().Build();
            var fileConfig = new FileConfig(config) {EmployeeOutputFile = ""};

            _paySlipGenerator = new PaySlipGenerator(fileConfig);

            var exception = Assert.Throws<Exception>(() => _paySlipGenerator.PrintPaySlip(new List<PaySlip>()));
            Assert.Equal("File path is not setup for payslip generation", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_PaySlipOutputFile_Is_Valid()
        {
            var config = new ConfigurationBuilder().Build();
            var fileConfig = new FileConfig(config) {EmployeeOutputFile = "Output.csv"};

            _paySlipGenerator = new PaySlipGenerator(fileConfig);

            var paySlips = new List<PaySlip>()
            {
                new PaySlip()
                {
                    Name = "Abc",
                    GrossIncome = 900,
                    IncomeTax = 90,
                    NetIncome = 89,
                    PayPeriod = "Jan-Dec"
                }
            };

            Assert.True(_paySlipGenerator.PrintPaySlip(paySlips));
        }

        [Fact]
        public void Should_Throw_Exception_When_PaySlipOutputFile_Is_Invalid()
        {
            var config = new ConfigurationBuilder().Build();
            var fileConfig = new FileConfig(config) {EmployeeOutputFile = "d:/Output.csv"};

            _paySlipGenerator = new PaySlipGenerator(fileConfig);

            var paySlips = new List<PaySlip>()
            {
                new PaySlip()
                {
                    Name = "Abc",
                    GrossIncome = 900,
                    IncomeTax = 90,
                    NetIncome = 89,
                    PayPeriod = "Jan-Dec"
                }
            };

            Assert.Throws<Exception>(() => _paySlipGenerator.PrintPaySlip(paySlips));
        }
    }
}
