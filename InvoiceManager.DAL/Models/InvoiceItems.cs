namespace DataAccessLayer.Models
{
    public class InvoiceItems
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public override string ToString()
        {
            return $"{InvoiceId}: {Invoice} - {ItemId}: {Item}";
        }
    }
}
