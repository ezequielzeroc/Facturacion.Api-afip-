using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facturacion.Data.Contracts;
using Facturacion.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityDocumentTypeController : ControllerBase
    {
        private IIdentityDocumentTypeRepository _identityDocumentTypeRepository;
        public IdentityDocumentTypeController(IIdentityDocumentTypeRepository identityDocumentTypeRepository)
        {
            _identityDocumentTypeRepository = identityDocumentTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);
                return Ok(await _identityDocumentTypeRepository.List(CompanyID));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}