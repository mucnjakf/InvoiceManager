using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.ViewModels
{
    public class ViewInvoiceViewModel
    {
        [Display(Name = "Invoice number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Total price without VAT")]
        public decimal TotalPriceWithoutVat { get; set; }

        [Display(Name = "Total price with VAT")]
        public decimal TotalPriceWithVat { get; set; }

        public string Recipient { get; set; }

        [Display(Name = "VAT")]
        public string Vat { get; set; }

        [Display(Name = "Application user")]
        public string ApplicationUser { get; set; }

        public IList<InvoiceItems> Items { get; set; }
        public Invoice Invoice { get; set; }
    }
}
