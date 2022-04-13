using System.ComponentModel.Composition;

namespace InvoiceApp.Utilities.VatCalculator.CountryVat
{
    [Export(typeof(IVatOperation))]
    [ExportMetadata("Vat", "25%")]
    class CroatiaVat : IVatOperation
    {
        public decimal Operate(decimal price) => price * 1.25M;
    }
}
