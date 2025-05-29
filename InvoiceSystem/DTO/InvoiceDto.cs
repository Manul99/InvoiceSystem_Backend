namespace InvoiceSystem.DTO
{
    public class InvoiceDto
    {
        public DateTime TransactionDate { get; set; }
        public decimal Discount { get; set; }
        public List<InvoiceItemDto>? Items { get; set; }
    }
}
