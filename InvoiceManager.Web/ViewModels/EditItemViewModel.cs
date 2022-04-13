using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.ViewModels
{
    public class EditItemViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal Price { get; set; }

        public EditItemViewModel()
        {
        }

        public EditItemViewModel(Item item)
        {
            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            Amount = item.Amount;
            Price = item.Price;
        }
    }
}
