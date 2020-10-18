using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EmployeePaySlip.Config;
using EmployeePaySlip.Models;
using Microsoft.Extensions.Configuration;

namespace EmployeePaySlip.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public IFileConfig _fileConfig;
        public EmployeeRepository(IFileConfig fileConfig)
        {
            _fileConfig = fileConfig;
        }

        public List<Employee> LoadEmployees()
        {
            try
            {
                var fileName = _fileConfig.EmployeeInputFile;
                if (string.IsNullOrEmpty(fileName))
                {
                    throw new Exception("Employee input file path is not setup in config");
                }

                List<Employee> employeeDetails;
                using (var fs = new StreamReader(fileName))
                {
                    employeeDetails = new CsvHelper.CsvReader(fs, System.Globalization.CultureInfo.CurrentCulture)
                        .GetRecords<Employee>().ToList();
                }

                return employeeDetails;
            }
            catch (FileNotFoundException e)
            {
                throw new Exception("Specified input file is not found", e);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
