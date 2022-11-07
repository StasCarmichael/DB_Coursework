using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptService receiptService;


        public ReceiptsController(IReceiptService receiptService)
        {
            this.receiptService = receiptService;
        }



        // GET/api/receipts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptModel>>> GetAll()
        {
            try
            {
                var response = await receiptService.GetAllAsync();

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return NotFound(); }
        }

        // GET/api/receipts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptModel>> GetById(int id)
        {
            try
            {
                var response = await receiptService.GetByIdAsync(id);

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return NotFound(); }
        }

        // GET/api/receipts/{id}/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<IEnumerable<ReceiptDetailModel>>> GetDetailsById(int id)
        {
            try
            {
                var response = await receiptService.GetReceiptDetailsAsync(id);

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return NotFound(); }
        }

        // GET/api/receipts/{id}/sum
        [HttpGet("{id}/sum")]
        public async Task<ActionResult<IEnumerable<ReceiptDetailModel>>> GetSumById(int id)
        {
            try
            {
                var response = await receiptService.ToPayAsync(id);

                return Ok(response);
            }
            catch { return BadRequest(); }
        }

        // GET/api/receipts/period?startDate=2021-12-1&endDate=2020-12-31
        [HttpGet("period/{startDate:DateTime?}/{endDate:DateTime?}")]
        public async Task<ActionResult<IEnumerable<ReceiptModel>>> Get
            ([FromQuery] string startDate, [FromQuery] string endDate)
        {
            try
            {
                var _startDate = DateTime.Parse(startDate);
                var _endDate = DateTime.Parse(endDate);

                var response = await receiptService.GetReceiptsByPeriodAsync(_startDate, _endDate);

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return BadRequest(); }
        }



        // POST/api/receipts
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ReceiptModel value)
        {
            try
            {
                await receiptService.AddAsync(value);
                return Ok(value);
            }
            catch { return BadRequest(); }
        }



        // PUT/api/receipts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ReceiptModel value)
        {
            try
            {
                value.Id = id;

                await receiptService.UpdateAsync(value);
                return Ok(value);
            }
            catch { return BadRequest(); }
        }

        // PUT/api/receipts/{id}/products/add/{productId}/{quantity}
        [HttpPut("{id}/products/add/{productId}/{quantity}")]
        public async Task<ActionResult> AddProductToReceipt(int id, int productId, int quantity)
        {
            try
            {
                await receiptService.AddProductAsync(productId, id, quantity);
                return Ok();
            }
            catch { return BadRequest(); }
        }

        // PUT/api/receipts/{id}/products/remove/{productId}/{quantity}
        [HttpPut("{id}/products/remove/{productId}/{quantity}")]
        public async Task<ActionResult> RemoveProductToReceipt(int id, int productId, int quantity)
        {
            try
            {
                await receiptService.RemoveProductAsync(productId, id, quantity);
                return Ok();
            }
            catch { return BadRequest(); }
        }

        // PUT/api/receipts/{id}/checkout
        [HttpPut("{id}/checkout")]
        public async Task<ActionResult> CheckoutReceipt(int id)
        {
            try
            {
                await receiptService.CheckOutAsync(id);
                return Ok();
            }
            catch { return BadRequest(); }
        }



        //  DELETE/api/receipts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await receiptService.DeleteAsync(id);
                return Ok();
            }
            catch { return Ok(); }
        }
        
    }
}
