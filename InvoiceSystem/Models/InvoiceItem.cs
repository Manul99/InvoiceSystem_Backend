using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InvoiceSystem.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public int InvoiceId { get; set; }
        [JsonIgnore]
        public Invoice? Invoice { get; set; }
    }
}
