using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService currencyService;
        public CurrencyController(ICurrencyService dollar)
        {
            currencyService = dollar;
        }

        // GET api/<CurrencyController>/5
        [HttpGet("{currencyCode}")]
        public async Task<IActionResult> Get(string currencyCode)
        {
            IActionResult result;
            try
            {
                var currency = await currencyService.GetCurrencyByCode(currencyCode);
                result = Ok(currency);
            }
            catch (Exception ex)
            {
                result = NotFound(ex.Message);
            }
            return result;
        }
    }
}
