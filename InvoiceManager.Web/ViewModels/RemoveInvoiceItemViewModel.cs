using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace InvoiceApp.ViewModels
{
    public class RemoveInvoiceItemViewModel
    {
        public Invoice Invoice { get; set; }
        public List<SelectListItem> Items { get; set; }

        public int InvoiceId { get; set; }
        public int ItemId { get; set; }

        public RemoveInvoiceItemViewModel()
        {
        }

        public RemoveInvoiceItemViewModel(Invoice invoice, IEnumerable<Item> items)
        {
            Items = new List<SelectListItem>();

            foreach (var item in items)
            {
                Items.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = $"{ item.Name } - Amount: { item.Amount } - Total price: { item.TotalPrice }"
                });
            }

            Invoice = invoice;
        }
    }
}

