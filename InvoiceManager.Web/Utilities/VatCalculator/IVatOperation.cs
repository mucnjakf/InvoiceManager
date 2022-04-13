using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApp.Utilities.VatCalculator
{
    public interface IVatOperation
    {
        decimal Operate(decimal price);
    }
}
