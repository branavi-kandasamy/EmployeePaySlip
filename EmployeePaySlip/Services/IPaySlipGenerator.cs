using System.Collections.Generic;
using EmployeePaySlip.Models;

namespace EmployeePaySlip.Services
{
    public interface IPaySlipGenerator
    {
        bool PrintPaySlip(List<PaySlip> paySlips);
    }
}
