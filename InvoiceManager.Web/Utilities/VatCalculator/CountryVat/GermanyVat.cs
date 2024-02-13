using System.ComponentModel.Composition;

namespace InvoiceApp.Utilities.VatCalculator.CountryVat
{
    [Export(typeof(IVatOperation))]
    [ExportMetadata("Vat", "19%")]
    class GermanyVat : IVatOperation
    {
        public decimal Operate(decimal price) => price * 1.19M;
    }
}
