using DataAccessLayer.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.ViewModels
{
    public class EditInvoiceViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Invoice number")]
        public string InvoiceNumber { get; set; }

        [Required]
        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        [Required]
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [Required]
        public string Recipient { get; set; }

        [Required]
        [Display(Name = "VAT")]
        public string Vat { get; set; }

        public EditInvoiceViewModel()
        {
        }

        public EditInvoiceViewModel(Invoice invoice)
        {
            Id = invoice.Id;
            InvoiceNumber = invoice.InvoiceNumber;
            CreationDate = invoice.CreationDate;
            DueDate = invoice.DueDate;
            Recipient = invoice.Recipient;
            Vat = invoice.Vat;
        }
    }
}
