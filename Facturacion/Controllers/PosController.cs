using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facturacion.Data.Contracts;
using Facturacion.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PosController : ControllerBase
    {
        private IPosRepository posRepository;

        public PosController(IPosRepository _posRepository)
        {
            posRepository = _posRepository;
        }
        
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Pos pos)
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);

                pos.CompanyId = CompanyID;
                return Ok(new { message = await posRepository.Create(pos) });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("List")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> List()
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);

                return Ok(await posRepository.List(CompanyID));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);

                return Ok(await posRepository.GetPos(id, CompanyID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);

                return Ok(await posRepository.Delete(id,CompanyID));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPatch]
        public async Task<ActionResult<bool>> Update(Pos pos)
        {
            int CompanyID = 0;
            try
            {
                CompanyID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "CompanyID").Value);

                pos.CompanyId = CompanyID;
                var res = await posRepository.Save(pos);
                return Ok(res);
            }
            catch ( Exception e)
            {

                throw;
            }

        }


    }
}