using Data.Common;
using Domain.Interfaces;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly ITransferService _validateTransfer;
        public PurchaseController(ITransferService validateTransfer)
        {
            _validateTransfer = validateTransfer;
        }

        // POST api/<PurchaseController>
        [HttpPost]
        public IActionResult Post([FromBody]TransferHistory transfer)
        {
            try
            {
                var result = _validateTransfer.MakeTransaction(transfer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
