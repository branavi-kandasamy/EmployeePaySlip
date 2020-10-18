using System.Collections.Generic;
using EmployeePaySlip.Models;

namespace EmployeePaySlip.Services
{
    public interface IPaySlipGenerator
    {
        string PrintPaySlip(List<PaySlip> paySlips);
    }
}
