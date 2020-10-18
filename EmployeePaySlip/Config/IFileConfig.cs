namespace EmployeePaySlip.Config
{
    public interface IFileConfig
    {
        string TaxDataInputFile { get; set; }

        string EmployeeInputFile { get; set; }

        string EmployeeOutputFile { get; set; }
    }
}
