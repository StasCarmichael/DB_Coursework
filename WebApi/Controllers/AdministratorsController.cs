using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorsController : ControllerBase
    {
        private readonly IAdministratorService administratorService;



        //Inject customer service via constructor
        public AdministratorsController(IAdministratorService administratorService)
        {
            this.administratorService = administratorService;
        }



        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministratorModel>>> Get()
        {
            try
            {
                return Ok(await administratorService.GetAllAsync());
            }
            catch { return NotFound(); }
        }

        //GET: api/customers/1
        [HttpGet("{id}")]
        public async Task<ActionResult<AdministratorModel>> GetById(int id)
        {
            try
            {
                var response = await administratorService.GetByIdAsync(id);

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return NotFound(); }
        }


        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AdministratorModel value)
        {
            try
            {
                await administratorService.AddAsync(value);
                return Ok(value);
            }
            catch { return BadRequest(); }
        }

        // PUT: api/customers/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AdministratorModel value)
        {
            try
            {
                value.Id = id;

                await administratorService.UpdateAsync(value);
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
                await administratorService.DeleteAsync(id);
                return Ok();
            }
            catch { return BadRequest(); }
        }
    }
}
