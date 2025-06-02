using System.ComponentModel.DataAnnotations;

namespace InvoiceSystem.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();


    }
}
