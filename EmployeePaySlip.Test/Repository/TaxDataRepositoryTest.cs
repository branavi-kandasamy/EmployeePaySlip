using System;
using EmployeePaySlip.Config;
using EmployeePaySlip.Repository;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace EmployeePaySlip.Test.Repository
{
    public class TaxDataRepositoryTest
    {
        private TaxDataRepository _taxDataRepository;

        [Fact]
        public void Should_Throw_Exception_When_TaxDataInputFile_Is_Empty()
        {
            var config = new ConfigurationBuilder().Build();
            var fileConfig = new FileConfig(config) { TaxDataInputFile = "" };

            _taxDataRepository = new TaxDataRepository(fileConfig);

            var exception =  Assert.Throws<Exception>(() => _taxDataRepository.LoadTaxData());
            Assert.Equal("Tax details file path is not setup in config", exception.Message);
        }

        [Fact]
        public void Should_Return_TaxDetails_When_TaxDataInputFile_Is_Defined()
        {
            var config = new ConfigurationBuilder().Build();
            var fileConfig = new FileConfig(config) { TaxDataInputFile = "TaxData.csv" };

            _taxDataRepository = new TaxDataRepository(fileConfig);

            var taxDetails = _taxDataRepository.LoadTaxData();
            
            Assert.NotNull(taxDetails);
            Assert.Equal(5, taxDetails.Count);
        }

        [Fact]
        public void Should_Return_TaxDetails_When_TaxDataInputFile_Is_Invalid()
        {
            var config = new ConfigurationBuilder().Build();
            var fileConfig = new FileConfig(config) { TaxDataInputFile = "TaxData1.csv" };

            _taxDataRepository = new TaxDataRepository(fileConfig);

            var exception = Assert.Throws<Exception>(() => _taxDataRepository.LoadTaxData());
            Assert.Equal("Specified input file is not found", exception.Message);
        }
    }
}
