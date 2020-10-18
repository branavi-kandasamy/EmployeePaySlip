namespace EmployeePaySlip.Models
{
    public class Employee
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AnnualSalary { get; set; }

        public decimal SuperRate { get; set; }

        public string PayPeriod { get; set; }
    }
}