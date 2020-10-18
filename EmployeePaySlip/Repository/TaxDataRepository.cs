using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EmployeePaySlip.Config;
using EmployeePaySlip.Models;

namespace EmployeePaySlip.Repository
{
    public class TaxDataRepository : ITaxDataRepository
    {
        public IFileConfig _fileConfig;
        public TaxDataRepository(IFileConfig fileConfig)
        {
            _fileConfig = fileConfig;
        }

        public List<TaxData> LoadTaxData()
        {
            try
            {
                List<TaxData> taxDetails;
                var fileName = _fileConfig.TaxDataInputFile;
                if (string.IsNullOrEmpty(fileName))
                {
                    throw new Exception("Tax details file path is not setup in config");
                }
                using (var fs = new StreamReader(fileName))
                {
                    taxDetails = new CsvHelper.CsvReader(fs, System.Globalization.CultureInfo.CurrentCulture).GetRecords<TaxData>().ToList();
                }

                return taxDetails;
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
