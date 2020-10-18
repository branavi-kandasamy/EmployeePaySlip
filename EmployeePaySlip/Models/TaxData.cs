namespace EmployeePaySlip.Models
{
    public class TaxData
    {
        public int MaxLimit { get; set; }

        public int MinLimit { get; set; }

        public decimal TaxRate { get; set; }

        public decimal BaseTax { get; set; }
    }
}