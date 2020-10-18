using System.Collections.Generic;
using EmployeePaySlip.Models;

namespace EmployeePaySlip.Repository
{
    public interface ITaxDataRepository
    {
        List<TaxData> LoadTaxData();
    }
}
