namespace InvoiceSystem.DTO
{
    public class InvoiceItemDto
    {
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
