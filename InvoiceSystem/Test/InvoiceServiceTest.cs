using InvoiceSystem.Data;
using InvoiceSystem.DTO;
using InvoiceSystem.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;

namespace InvoiceSystem.Test
{
    public class InvoiceServiceTest
    {
        private readonly AppDbContext _context;
        private readonly InvoiceServices _service;

        public InvoiceServiceTest()
        {
            //Used in memory database for fast testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InvoiceAppDB")
                .Options;

            _context = new AppDbContext(options);
            _service = new InvoiceServices(_context);
        }

        [Fact]
        public void CreateInvoice_ShouldCalculateCorrectTotalAndBalance()
        {
            try
            {
                //Create invoice DTO with sample items
                var invoiceDto = new InvoiceDto
                {
                    TransactionDate = DateTime.UtcNow,
                    Discount = 20,
                    Items = new List<InvoiceItemDto>
                    {
                        new InvoiceItemDto { ProductName = "Mouse", Quantity = 2, UnitPrice = 500 },
                        new InvoiceItemDto { ProductName = "Keyboard", Quantity = 1, UnitPrice = 2500 }
                    }
                };

                var result = _service.createInvoice(invoiceDto);

                //Check correctness of total, balance and items
                //TotalAmount = 500*2 + 2500 = 3500
                //BalanceAmount = 3500 - 20 = 3480
                Assert.NotNull(result);
                Assert.Equal(3500, result.TotalAmount);
                Assert.Equal(3480, result.BalanceAmount);
                Assert.NotNull(result.Items);
                Assert.Equal(2, result.Items.Count);
            }
            catch (Exception ex)
            {
                throw new Xunit.Sdk.XunitException($"Test failed with exception: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [Fact]
        public void CreateInvoice_WithEmptyItems_ShouldReturnZeroTotals()
        {
            try
            {
                //Create invoice DTO with no items
                var invoiceDto = new InvoiceDto
                {
                    TransactionDate = DateTime.UtcNow,
                    Discount = 0,
                    Items = new List<InvoiceItemDto>
                    {
                         new InvoiceItemDto { ProductName = "USB Cables", Quantity = 1, UnitPrice = 250 }
                    }
                };

                var result = _service.createInvoice(invoiceDto);
                var ex = Assert.Throws<InvalidOperationException>(() => _service.createInvoice(invoiceDto));
                Assert.Equal("Invoice must include at least one item.", ex.Message);

                //Assert totals are zero when no items are present
                Assert.NotNull(result);
                Assert.Equal(0, result.TotalAmount);
                Assert.Equal(0, result.BalanceAmount);
                Assert.NotNull(result.Items);
                Assert.Empty(result.Items);
            }
            catch (Exception ex)
            {
                throw new Xunit.Sdk.XunitException($"Test failed with exception: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
