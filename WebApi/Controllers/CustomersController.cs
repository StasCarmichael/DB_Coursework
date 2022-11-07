using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;


        //Inject customer service via constructor
        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }


        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> Get()
        {
            try
            {
                return Ok(await customerService.GetAllAsync());
            }
            catch { return NotFound(); }
        }

        //GET: api/customers/1
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetById(int id)
        {
            try
            {
                var response = await customerService.GetByIdAsync(id);

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return NotFound(); }
        }

        //GET: api/customers/products/1
        [HttpGet("products/{id}")]
        public async Task<ActionResult<CustomerModel>> GetByProductId(int id)
        {
            try
            {
                var response = await customerService.GetCustomersByProductIdAsync(id);

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return NotFound(); }
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CustomerModel value)
        {
            try
            {
                await customerService.AddAsync(value);
                return Ok(value);
            }
            catch { return BadRequest(); }
        }

        // PUT: api/customers/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] CustomerModel value)
        {
            try
            {
                value.Id = id;

                await customerService.UpdateAsync(value);
                return Ok(value);
            }
            catch { return BadRequest(); }
        }

        // DELETE: api/customers/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await customerService.DeleteAsync(id);
                return Ok();
            }
            catch { return BadRequest(); }
        }
    }
}
