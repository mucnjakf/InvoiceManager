using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Total price")]
        public decimal TotalPrice { get; set; }

        public IList<InvoiceItems> InvoiceItems { get; set; }
    }
}
