using System;
using System.Collections.Generic;
using EmployeePaySlip.Models;
using EmployeePaySlip.Repository;
using EmployeePaySlip.Services;
using Xunit;
using NSubstitute;

namespace EmployeePaySlip.Test.Services
{
    public class TaxCalculatorServiceTest
    {
        [Fact]
        public void Should_Return_IncomeTax_For_AnnualSalary()
        {
            // Arrange
            var taxDataRepository = Substitute.For<ITaxDataRepository>();

            var taxDetails = new List<TaxData>()
            {
                new TaxData
                {
                    MinLimit = 0,
                    MaxLimit = 1000,
                    TaxRate = 0,
                    BaseTax = 0
                },
                new TaxData()
                {
                    MinLimit = 1000,
                    MaxLimit = 5000,
                    TaxRate = 0.10m,
                    BaseTax = 200,
                }
            };
            taxDataRepository.LoadTaxData().Returns(taxDetails);

            // Act
            var taxCalculatorService = new TaxCalculatorService(taxDataRepository);
            var incomeTax = taxCalculatorService.CalculateIncomeTax(4000);

            // Assert
            Assert.Equal(42, incomeTax);
        }

        [Fact]
        public void Should_Throw_Exception_When_TaxDetail_Is_Not_Defined()
        {
            var taxDataRepository = Substitute.For<ITaxDataRepository>();
            taxDataRepository.LoadTaxData().Returns(new List<TaxData>());

            var taxCalculatorService = new TaxCalculatorService(taxDataRepository);

            var exception = Assert.Throws<Exception>(() => taxCalculatorService.CalculateIncomeTax(4000));
            Assert.Equal("Tax detail is not defined", exception.Message);
        }

        [Fact]
        public void Should_Return_IncomeTax_For_AnnualSalary_Is_Negative()
        {
            // Arrange
            var taxDataRepository = Substitute.For<ITaxDataRepository>();

            var taxDetails = new List<TaxData>()
            {
                new TaxData
                {
                    MinLimit = 0,
                    MaxLimit = 1000,
                    TaxRate = 0,
                    BaseTax = 0
                },
                new TaxData()
                {
                    MinLimit = 1000,
                    MaxLimit = 5000,
                    TaxRate = 0.10m,
                    BaseTax = 200,
                }
            };
            taxDataRepository.LoadTaxData().Returns(taxDetails);

            // Act
            var taxCalculatorService = new TaxCalculatorService(taxDataRepository);
            var exception = Assert.Throws<Exception>(() => taxCalculatorService.CalculateIncomeTax(-4000));
            Assert.Equal("Invalid annual salary", exception.Message);
        }

        [Fact]
        public void Should_Return_IncomeTax_For_AnnualSalary_Is_More_Than_Limit()
        {
            // Arrange
            var taxDataRepository = Substitute.For<ITaxDataRepository>();

            var taxDetails = new List<TaxData>()
            {
                new TaxData
                {
                    MinLimit = 0,
                    MaxLimit = 1000,
                    TaxRate = 0,
                    BaseTax = 0
                },
                new TaxData()
                {
                    MinLimit = 1000,
                    MaxLimit = -1,
                    TaxRate = 0.10m,
                    BaseTax = 200,
                }
            };
            taxDataRepository.LoadTaxData().Returns(taxDetails);

            // Act
            var taxCalculatorService = new TaxCalculatorService(taxDataRepository);
            var incomeTax = taxCalculatorService.CalculateIncomeTax(6000);

            // Assert
            Assert.Equal(58, incomeTax);
        }
    }
}
