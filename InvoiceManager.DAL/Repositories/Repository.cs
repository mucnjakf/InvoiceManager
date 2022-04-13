using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class Repository : IRepository
    {
        private readonly InvoiceAppDbContext _context;

        public Repository(InvoiceAppDbContext context) => _context = context;

        public void SaveChanges() => _context.SaveChanges();

        // Invoice

        public List<Invoice> GetInvoices() => _context.Invoices.ToList();

        public Invoice GetInvoice(int invoiceId) => _context.Invoices.Include("ApplicationUser").Single(invoice => invoice.Id == invoiceId);

        public void CreateInvoice(Invoice invoice)

        {
            _context.Invoices.Add(invoice);
            SaveChanges();
        }

        public void UpdateInvoice(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            SaveChanges();
        }

        public void RemoveInvoice(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            SaveChanges();
        }

        public bool InvoiceExists(int invoiceId) => _context.Invoices.Any(e => e.Id == invoiceId);

        // Item

        public List<Item> GetItems() => _context.Items.ToList();

        public Item GetItem(int itemId) => _context.Items.Single(item => item.Id == itemId);

        public void CreateItem(Item item)
        {
            _context.Items.Add(item);
            SaveChanges();
        }

        public void UpdateItem(Item item)
        {
            _context.Update(item);
            SaveChanges();
        }

        public void RemoveItem(Item item)
        {
            _context.Items.Remove(item);
            SaveChanges();
        }

        public bool ItemExists(int itemId) => _context.Items.Any(e => e.Id == itemId);

        // InvoiceItems

        public List<InvoiceItems> GetInvoiceItems(int invoiceId)
            => _context.InvoiceItems
            .Include(item => item.Item)
            .Where(invoiceItem => invoiceItem.InvoiceId == invoiceId)
            .ToList();

        public List<InvoiceItems> GetExistingInvoiceItems(int itemId, int invoiceId)
            => _context.InvoiceItems
            .Where(invoiceItem => invoiceItem.ItemId == itemId)
            .Where(invoiceItem => invoiceItem.InvoiceId == invoiceId)
            .ToList();

        public InvoiceItems GetInvoiceItem(Item item, Invoice invoice) 
            => _context.InvoiceItems.Single(invoiceItem => invoiceItem.Item == item && invoiceItem.Invoice == invoice);

        public void AddInvoiceItem(InvoiceItems invoiceItem)
        {
            _context.InvoiceItems.Add(invoiceItem);
            SaveChanges();
        }

        public void RemoveInvoiceItem(InvoiceItems invoiceItem)
        {
            _context.InvoiceItems.Remove(invoiceItem);
            SaveChanges();
        }

        // ApplicationUser

        public ApplicationUser GetApplicationUser(string username) => _context.ApplicationUsers.Single(au => au.UserName == username);
        
    }
}
