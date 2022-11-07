using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;


        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }



        // GET/api/products
        // GET/api/products?categoryId=1&minPrice=20&maxPrice=50
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> Get
            ([FromQuery] int? categoryId = null, [FromQuery] int? minPrice = null, [FromQuery] int? maxPrice = null)
        {
            try
            {
                var filterModel = new FilterSearchModel();
                if (categoryId.HasValue) filterModel.CategoryId = categoryId.Value;
                if (minPrice.HasValue) filterModel.MinPrice = minPrice.Value;
                if (maxPrice.HasValue) filterModel.MaxPrice = maxPrice.Value;

                var response = await productService.GetByFilterAsync(filterModel);

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return BadRequest(); }
        }

        //GET/api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetById(int id)
        {
            try
            {
                var response = await productService.GetByIdAsync(id);

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return NotFound(); }
        }

        // POST/api/products – add a product
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProductModel value)
        {
            try
            {
                await productService.AddAsync(value);
                return Ok(value);
            }
            catch { return BadRequest(); }
        }

        // PUT/api/products/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductModel value)
        {
            try
            {
                value.Id = id;

                await productService.UpdateAsync(value);
                return Ok(value);
            }
            catch { return BadRequest(); }
        }

        //  DELETE/api/products/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await productService.DeleteAsync(id);
                return Ok();
            }
            catch { return BadRequest(); }
        }





        // GET/api/products/categories
        [HttpGet("categories/")]
        public async Task<ActionResult<ProductCategoryModel>> GetProductCategory()
        {
            try
            {
                var response = await productService.GetAllProductCategoriesAsync();

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return NotFound(); }
        }

        // POST/api/products/categories
        [HttpPost("categories/")]
        public async Task<ActionResult> AddProductCategory([FromBody] ProductCategoryModel value)
        {
            try
            {
                await productService.AddCategoryAsync(value);
                return Ok(value);
            }
            catch { return BadRequest(); }
        }

        // PUT/api/products/categories{id}
        [HttpPut("categories/{id}")]
        public async Task<ActionResult> UpdateProductCategory(int id, [FromBody] ProductCategoryModel value)
        {
            try
            {
                value.Id = id;

                await productService.UpdateCategoryAsync(value);
                return Ok(value);
            }
            catch { return BadRequest(); }
        }

        //  DELETE/api/products/categories/{id}
        [HttpDelete("categories/{id}")]
        public async Task<ActionResult> DeleteProductCategory(int id)
        {
            try
            {
                await productService.RemoveCategoryAsync(id);
                return Ok();
            }
            catch { return BadRequest(); }
        }
    }
}
