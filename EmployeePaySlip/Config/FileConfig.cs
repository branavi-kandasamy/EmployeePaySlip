using Microsoft.Extensions.Configuration;

namespace EmployeePaySlip.Config
{
    public class FileConfig : IFileConfig
    {
        public string TaxDataInputFile { get; set; }

        public string EmployeeInputFile { get; set; }

        public string EmployeeOutputFile { get; set; }
        public FileConfig(IConfiguration configuration)
        {
            var pathSection = configuration.GetSection("DataFilePath");
            TaxDataInputFile = pathSection.GetSection("TaxDataFileName").Value;
            EmployeeInputFile = pathSection.GetSection("EmployeeDataFileName").Value;
            EmployeeOutputFile = pathSection.GetSection("PaySlipGeneratedPath").Value;
        }
    }
}
