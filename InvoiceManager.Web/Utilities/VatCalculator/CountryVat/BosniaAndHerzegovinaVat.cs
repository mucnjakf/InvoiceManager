using System.ComponentModel.Composition;

namespace InvoiceApp.Utilities.VatCalculator.CountryVat
{
    [Export(typeof(IVatOperation))]
    [ExportMetadata("Vat", "17%")]
    class BosniaAndHerzegovina : IVatOperation
    {
        public decimal Operate(decimal price) => price * 1.17M;
    }
}
