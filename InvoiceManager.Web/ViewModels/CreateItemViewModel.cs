using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.ViewModels
{
    public class CreateItemViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
