using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.ViewModels
{
    public class AddInvoiceItemViewModel
    {
        public Invoice Invoice { get; set; }
        public List<SelectListItem> Items { get; set; }

        public int InvoiceId { get; set; }

        [Display(Name = "Item")]
        public int ItemId { get; set; }

        public AddInvoiceItemViewModel()
        {
        }

        public AddInvoiceItemViewModel(Invoice invoice, IEnumerable<Item> items)
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
