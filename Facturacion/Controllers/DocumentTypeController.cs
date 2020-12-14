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
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeRepository _DocumentTypeRepository;
        public DocumentTypeController(IDocumentTypeRepository DocumentTypeRepository)
        {
            _DocumentTypeRepository = DocumentTypeRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _DocumentTypeRepository.GetTypes());
            }
            catch (Exception e )
            {
                return BadRequest();
            }
        }
    }

}