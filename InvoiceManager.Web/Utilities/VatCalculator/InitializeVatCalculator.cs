using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace InvoiceApp.Utilities.VatCalculator
{
    public class InitializeVatCalculator
    {
        private CompositionContainer _container;

        [Import(typeof(IVatCalculator))]
        public IVatCalculator vatCalculator;

        public InitializeVatCalculator()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));

            _container = new CompositionContainer(catalog);

            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException ex)
            {
                throw new CompositionException(ex.Message);
            }
        }
    }
}
