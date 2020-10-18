using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;
using EmployeePaySlip.Config;
using EmployeePaySlip.Models;

namespace EmployeePaySlip.Services
{
    public class PaySlipGenerator : IPaySlipGenerator
    {
        private IFileConfig _fileConfig;
        public PaySlipGenerator(IFileConfig fileConfig)
        {
            _fileConfig = fileConfig;
        }

        public string PrintPaySlip(List<PaySlip> paySlips)
        {
            try
            {
                var fileName = _fileConfig.EmployeeOutputFile;
                string fileGeneratedPath;
                if (string.IsNullOrEmpty(fileName))
                {
                    throw new Exception("File path is not setup for payslip generation");
                }
                using (var sw = new StreamWriter(fileName, false, new UTF8Encoding(true)))
                using (var cw = new CsvWriter(sw, System.Globalization.CultureInfo.CurrentCulture))
                {
                    fileGeneratedPath = ((FileStream) (sw.BaseStream)).Name;
                    cw.WriteRecords(paySlips);
                }

                return fileGeneratedPath;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
