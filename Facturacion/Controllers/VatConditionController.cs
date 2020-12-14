using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facturacion.Data.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatConditionController : ControllerBase
    {
        private IVatConditionRepository _vatConditionRepository;
        public VatConditionController(IVatConditionRepository vatConditionRepository)
        {
            _vatConditionRepository = vatConditionRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> List()
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);
                return Ok(await _vatConditionRepository.List(CompanyID));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}