using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories
{
    public interface IRepository
    {
        void SaveChanges();

        // Invoice
        List<Invoice> GetInvoices();
        Invoice GetInvoice(int invoiceId);
        void CreateInvoice(Invoice invoice);
        void UpdateInvoice(Invoice invoice);
        void RemoveInvoice(Invoice invoice); 
        bool InvoiceExists(int invoiceId);

        // Item
        List<Item> GetItems();
        Item GetItem(int itemId);
        void CreateItem(Item item); 
        void UpdateItem(Item item);
        void RemoveItem(Item item);
        bool ItemExists(int itemId);

        // InvoiceItems
        List<InvoiceItems> GetInvoiceItems(int invoiceId);
        List<InvoiceItems> GetExistingInvoiceItems(int itemId, int invoiceId);
        InvoiceItems GetInvoiceItem(Item item, Invoice invoice);
        void AddInvoiceItem(InvoiceItems invoiceItem);
        void RemoveInvoiceItem(InvoiceItems invoiceItem);

        // ApplicationUser
        ApplicationUser GetApplicationUser(string email);
    }
}
