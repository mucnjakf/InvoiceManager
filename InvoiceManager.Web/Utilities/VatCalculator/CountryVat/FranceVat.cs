using System.ComponentModel.Composition;

namespace InvoiceApp.Utilities.VatCalculator.CountryVat
{
    [Export(typeof(IVatOperation))]
    [ExportMetadata("Vat", "20%")]
    class FranceVat : IVatOperation
    {
        public decimal Operate(decimal price) => price * 1.20M;
    }
}
