using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Facturacion.Data.Contracts;
using Facturacion.Data.Models.Invoices;
using Facturacion.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Facturacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private  IInvoiceRepository _invoiceRepository;
        private ILogger<InvoiceController> _logger;
        public InvoiceController(IInvoiceRepository invoiceRepository, ILogger<InvoiceController> logger)
        {
            _invoiceRepository = invoiceRepository;
            _logger = logger;
        }


        [HttpPost]
        [Authorize]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Invoices invoice)
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);
                
                return Ok(await _invoiceRepository.Create(CompanyID, invoice));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {nameof(Create)}: {e.StackTrace}");
                return BadRequest();
            }
        }


        [HttpPost]
        [Authorize]
        [Route("CancelInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelInvoice([FromBody] InvoiceCancelModel invoice)
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);
                return Ok(await _invoiceRepository.CancelInvoice(CompanyID, invoice.InvoiceID));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {nameof(Create)}: {e.StackTrace}");
                return BadRequest();
            }
        }


        [HttpGet]
        [Authorize]
        [Route("List")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> List()
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);
                return Ok(await _invoiceRepository.GetInvoices(CompanyID));
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);
                return Ok(await _invoiceRepository.Delete(id));
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var fs = await _invoiceRepository.Download(id);
            string mimeType = "application/octet-stream";
            if (fs != null)
                return File(fs, "application/pdf", Guid.NewGuid().ToString() + ".pdf");
            else
                return BadRequest();

        }

        [HttpPost]
        public async Task<IActionResult> FinalAction(Models.Invoices.FinalActionModel finalActionModel)
        {
            /// 
            return Ok();
        }
    }
}