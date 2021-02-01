using Facturacion.Data.Contracts;
using Facturacion.Models.FinancialMovements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialMovementsController : ControllerBase
    {
        private IFinancialMovementsRepository _financialMovementsRepository;
        private ILogger<FinancialMovementsController> _logger;
        public FinancialMovementsController(IFinancialMovementsRepository financialMovements, ILogger<FinancialMovementsController> logger)
        {
            _financialMovementsRepository = financialMovements;
            _logger = logger;
        }
        [HttpGet]
        [Route("get/{type}")]
        public async Task<IActionResult> get(int type)
        {
            int CompanyID;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);
                return Ok(await _financialMovementsRepository.Get(CompanyID, type));
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("markAsPaid")]
        public async Task<IActionResult> markAsPaid(ConfirmModel confirm)
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);
                return Ok(new { status = await _financialMovementsRepository.MarkAsPaid(CompanyID,confirm.invoiceId) });
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
