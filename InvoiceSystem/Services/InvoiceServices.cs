using InvoiceSystem.Data;
using InvoiceSystem.DTO;
using InvoiceSystem.Models;

namespace InvoiceSystem.Services
{
    public class InvoiceServices
    {
        private readonly AppDbContext _context;
        public InvoiceServices(AppDbContext context)
        {
            _context = context;
        }

        public Invoice createInvoice(InvoiceDto dto)
        {
            //Checking fields are empty or not
            if (dto == null||dto.Items == null || !dto.Items.Any())
                throw new ArgumentException("Invoice must include at least one item.");

            if (dto.TransactionDate == default)
                throw new ArgumentException("Fill relevant fileds");

            foreach(var item in dto.Items)
            {
                if(string.IsNullOrWhiteSpace(item.ProductName) || item.Quantity <= 0 || item.UnitPrice <= 0)
                {
                    throw new ArgumentException("Fill relevant fields");
                }
            }
            // Calculate the total price by summing up (UnitPrice * Quantity) for all items
            var total = dto.Items.Sum(i => i.UnitPrice *  i.Quantity);
            var discountAmount = (dto.Discount / 100m) * total;
            var balance = total - discountAmount;

            //Created a new object with basic transaction
            var invoice = new Invoice
            {
                TransactionDate = dto.TransactionDate,
                Discount = dto.Discount,
                TotalAmount = total,
                BalanceAmount = balance,
            };

            invoice.Items = dto.Items.Select(i => new InvoiceItem
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Invoice = invoice 
            }).ToList();

            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            return invoice;
        }
    }
}
