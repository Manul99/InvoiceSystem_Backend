using InvoiceSystem.DTO;
using InvoiceSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceServices _service;
        public InvoiceController(InvoiceServices services)
        {
            _service = services;
        }

        [HttpPost]
        public IActionResult createInvoice([FromBody] InvoiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data");
                }

                var invoice = _service.createInvoice(dto);
                return Ok(invoice);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("ERROR: " + innerMessage);
                Console.WriteLine(ex.StackTrace);
                return StatusCode(500, $"Error: {innerMessage}");
            }
        }
    }
}
