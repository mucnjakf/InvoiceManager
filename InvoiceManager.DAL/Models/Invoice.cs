using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Invoice number")]
        public string InvoiceNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalPriceWithoutVat { get; set; }

        [Display(Name = "Total price with VAT")]
        public decimal TotalPriceWithVat { get; set; }
        public string Recipient { get; set; }
        public string Vat { get; set; }

        public string ApplicationUserId { get; set; }

        [Display(Name = "Application user")]
        public ApplicationUser ApplicationUser { get; set; }

        public IList<InvoiceItems> InvoiceItems { get; set; }
    }
}
