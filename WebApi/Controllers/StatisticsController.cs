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
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService statisticService;


        public StatisticsController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }



        // GET/api/statistic/popularProducts?productCount=2
        [HttpGet("popularProducts/")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetMostPopular([FromQuery] int productCount)
        {
            try
            {
                var response = await statisticService.GetMostPopularProductsAsync(productCount);

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return BadRequest(); }
        }


        // GET/api/statisic/customer/{id}/{productCount}
        [HttpGet("customer/{id}/{productCount}")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetMostPopularOfCustomer
            (int id, int productCount)
        {
            try
            {
                var response = await statisticService.GetCustomersMostPopularProductsAsync(productCount, id);

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return BadRequest(); }
        }



        //Ошибка Сервиса

        //  GET/api/statistic/activity/{customerCount}?startDate= 2020-7-21&endDate= 2020-7-22
        [HttpGet("activity/{customerCount}")]
        public async Task<ActionResult<IEnumerable<CustomerActivityModel>>> GetMostActiveCustomer
            (int customerCount, [FromQuery] string startDate = null, [FromQuery] string endDate = null)
        {
            try
            {
                var _startDate = DateTime.Parse(startDate);
                var _endDate = DateTime.Parse(endDate);

                var response = await statisticService.GetMostValuableCustomersAsync(customerCount, _startDate, _endDate);

                if (response != null) return Ok(response);
                else return NotFound(response);
            }
            catch { return BadRequest(); }
        }


        //   GET/api/statistic/income/{categoryId}?startDate= 2020-7-21&endDate= 2020-7-22
        [HttpGet("income/{categoryId}")]
        public async Task<ActionResult<decimal>> GetIncomeOfCategory
            (int categoryId, [FromQuery] string startDate = null, [FromQuery] string endDate = null)
        {
            try
            {
                DateTime _startDate;
                DateTime _endDate ;

                if (startDate == null) { _startDate = new DateTime(1972, 01, 01); }
                else { _startDate = DateTime.Parse(startDate); }


                if (endDate == null) { _endDate = DateTime.Now; }
                else { _endDate = DateTime.Parse(endDate); }


                var response = await statisticService.GetIncomeOfCategoryInPeriod(categoryId, _startDate, _endDate);

                return Ok(response);
            }
            catch { return BadRequest(); }
        }

    }
}
