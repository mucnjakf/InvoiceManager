namespace InvoiceApp.Utilities.VatCalculator
{
    public interface IVatCalculator
    {
        decimal Calculate(decimal price, string vat);
    }
}
