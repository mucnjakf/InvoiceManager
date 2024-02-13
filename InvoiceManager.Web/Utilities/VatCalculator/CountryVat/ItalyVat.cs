using System.ComponentModel.Composition;

namespace InvoiceApp.Utilities.VatCalculator.CountryVat
{
    [Export(typeof(IVatOperation))]
    [ExportMetadata("Vat", "22%")]
    class ItalyVat : IVatOperation
    {
        public decimal Operate(decimal price) => price * 1.22M;
    }
}