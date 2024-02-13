using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using InvoiceApp.Utilities.VatCalculator;
using InvoiceApp.Utilities.VatCalculator.CountryVat;
using InvoiceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IRepository _db;

        public InvoiceController(IRepository db) => _db = db;

        // GET: Invoice
        public IActionResult Index() => View(_db.GetInvoices());

        // GET: Invoice/ViewInvoice/1
        public IActionResult ViewInvoice(int id)
        {
            List<InvoiceItems> items = _db.GetInvoiceItems(id);

            Invoice invoice = _db.GetInvoice(id);

            ViewInvoiceViewModel viewInvoiceViewModel = new ViewInvoiceViewModel()
            {
                InvoiceNumber = invoice.InvoiceNumber,
                CreationDate = invoice.CreationDate,
                DueDate = invoice.DueDate,
                TotalPriceWithoutVat = invoice.TotalPriceWithoutVat,
                TotalPriceWithVat = invoice.TotalPriceWithVat,
                Recipient = invoice.Recipient,
                Vat = invoice.Vat,
                ApplicationUser = $"{ invoice.ApplicationUser.FirstName } { invoice.ApplicationUser.LastName }",
                Invoice = invoice,
                Items = items
            };

            return View(viewInvoiceViewModel);
        }

        // GET: Invoice/Create
        public IActionResult Create() => View(new CreateInvoiceViewModel());

        // POST: Invoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateInvoiceViewModel createInvoiceViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = _db.GetApplicationUser(User.Identity.Name);

                Invoice invoice = new Invoice
                {
                    InvoiceNumber = createInvoiceViewModel.InvoiceNumber,
                    CreationDate = createInvoiceViewModel.CreationDate,
                    DueDate = createInvoiceViewModel.DueDate,
                    Recipient = createInvoiceViewModel.Recipient,
                    Vat = createInvoiceViewModel.Vat,
                    TotalPriceWithoutVat = 0,
                    TotalPriceWithVat = 0,
                    ApplicationUser = applicationUser
                };

                _db.CreateInvoice(invoice);

                return RedirectToAction(nameof(Index));
            }
            return View(createInvoiceViewModel);
        }

        // GET: Invoice/AddItem/1
        public IActionResult AddItem(int id)
        {
            Invoice invoice = _db.GetInvoice(id);
            List<InvoiceItems> invoiceItems = _db.GetInvoiceItems(id);
            
            List<Item> itemsInInvoice = new List<Item>();
            List<Item> allItems = _db.GetItems();

            if (invoiceItems.Count > 0)
            {
                foreach (InvoiceItems invoiceItem in invoiceItems)
                {
                    itemsInInvoice.Add(invoiceItem.Item);
                }

                IEnumerable<Item> items = allItems.Where(x => !itemsInInvoice.Any(y => y.Id == x.Id));

                return View(new AddInvoiceItemViewModel(invoice, items));
            }

            return View(new AddInvoiceItemViewModel(invoice, allItems));
        }

        // POST: Invoice/AddItem
        [HttpPost]
        public IActionResult AddItem(AddInvoiceItemViewModel addInvoiceItemViewModel)
        {
            var calculator = new InitializeVatCalculator();

            if (ModelState.IsValid)
            {
                int itemId = addInvoiceItemViewModel.ItemId;
                int invoiceId = addInvoiceItemViewModel.InvoiceId;

                Invoice invoice = _db.GetInvoice(invoiceId);
                Item item = _db.GetItem(itemId);

                IList<InvoiceItems> existingItems = _db.GetExistingInvoiceItems(itemId, invoiceId);

                if (existingItems.Count == 0)
                {
                    invoice.TotalPriceWithoutVat += item.TotalPrice;
                    invoice.TotalPriceWithVat = calculator.vatCalculator.Calculate(invoice.TotalPriceWithoutVat, invoice.Vat);                    

                    InvoiceItems invoiceItem = new InvoiceItems
                    {
                        Item = item,
                        Invoice = invoice
                    };

                    _db.AddInvoiceItem(invoiceItem);
                }

                return Redirect($"/Invoice/ViewInvoice/{addInvoiceItemViewModel.InvoiceId}");
            }

            return View(addInvoiceItemViewModel);
        }

        // GET: Invoice/RemoveItem/1
        public IActionResult RemoveItem(int id)
        {
            Invoice invoice = _db.GetInvoice(id);
            List<InvoiceItems> invoiceItems = _db.GetInvoiceItems(id);
            List<Item> items = new List<Item>();

            foreach (InvoiceItems invoiceItem in invoiceItems)
            {
                Item item = _db.GetItem(invoiceItem.ItemId);
                items.Add(item);
            }

            return View(new RemoveInvoiceItemViewModel(invoice, items));
        }

        // POST: Invoice/RemoveItem
        [HttpPost]
        public IActionResult RemoveItem(RemoveInvoiceItemViewModel removeInvoiceItemViewModel)
        {
            var calculator = new InitializeVatCalculator();

            if (ModelState.IsValid)
            {
                int itemId = removeInvoiceItemViewModel.ItemId;
                int invoiceId = removeInvoiceItemViewModel.InvoiceId;

                Item item = _db.GetItem(itemId);
                Invoice invoice = _db.GetInvoice(invoiceId);

                IList<InvoiceItems> existingInvoiceItems = _db.GetExistingInvoiceItems(itemId, invoiceId);

                if (existingInvoiceItems.Count != 0)
                {
                    invoice.TotalPriceWithoutVat -= item.TotalPrice;
                    invoice.TotalPriceWithVat = calculator.vatCalculator.Calculate(invoice.TotalPriceWithoutVat, invoice.Vat);                    

                    InvoiceItems invoiceItem = _db.GetInvoiceItem(item, invoice);

                    _db.RemoveInvoiceItem(invoiceItem);

                    return Redirect($"/Invoice/ViewInvoice/{removeInvoiceItemViewModel.InvoiceId}");
                }
            }

            return View(removeInvoiceItemViewModel);
        }

        // GET: Invoices/Delete/1
        public IActionResult Delete(int id) => View(_db.GetInvoice(id));

        // POST: Invoices/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.RemoveInvoice(_db.GetInvoice(id));

            return RedirectToAction(nameof(Index));
        }

        // GET: Invoices/Edit/1
        public IActionResult Edit(int id) => View(new EditInvoiceViewModel(_db.GetInvoice(id)));

        // POST: Invoices/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditInvoiceViewModel editInvoiceViewModel)
        {
            var calculator = new InitializeVatCalculator();

            if (ModelState.IsValid)
            {
                Invoice invoice = _db.GetInvoice(editInvoiceViewModel.Id);

                invoice.InvoiceNumber = editInvoiceViewModel.InvoiceNumber;
                invoice.CreationDate = editInvoiceViewModel.CreationDate;
                invoice.DueDate = editInvoiceViewModel.DueDate;
                invoice.Recipient = editInvoiceViewModel.Recipient;
                invoice.Vat = editInvoiceViewModel.Vat;
                invoice.TotalPriceWithVat = calculator.vatCalculator.Calculate(invoice.TotalPriceWithoutVat, invoice.Vat);

                try
                {
                    _db.UpdateInvoice(invoice);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_db.InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(editInvoiceViewModel);
        }
    }
}
