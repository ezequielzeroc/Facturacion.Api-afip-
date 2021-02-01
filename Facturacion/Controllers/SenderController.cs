using Facturacion.Data.Contracts;
using Facturacion.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private ISender _iSender;
        public SenderController(ISender iSender)
        {
            _iSender = iSender;
        }
        [HttpPost]
        [Route("addSend")]
        public async Task<IActionResult> AddSend(DocumentToSend document)
        {
            try
            {
                return Ok(await _iSender.addSend(document));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "error al intentar agregar el envío solicitado." });
            }
        }
    }
}
