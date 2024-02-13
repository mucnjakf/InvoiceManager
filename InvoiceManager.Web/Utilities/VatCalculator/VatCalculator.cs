using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace InvoiceApp.Utilities.VatCalculator
{
    [Export(typeof(IVatCalculator))]
    class VatCalculator : IVatCalculator
    {
        [ImportMany]
        IEnumerable<Lazy<IVatOperation, IVatOperationData>> vatOperations;

        public decimal Calculate(decimal price, string vat)
        {
            foreach (Lazy<IVatOperation, IVatOperationData> i in vatOperations)
            {
                if (i.Metadata.Vat.Equals(vat)) return i.Value.Operate(price);
            }
            return 0;
        }
    }
}
