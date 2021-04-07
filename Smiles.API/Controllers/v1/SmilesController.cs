using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Smiles.Core.Models;
using Smiles.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smiles.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SmilesController : ControllerBase
    {
        private readonly ILogger<SmilesController> _logger;
        private readonly ISmilesService _smilesService;

        public SmilesController(ILogger<SmilesController> logger, ISmilesService smilesService)
        {
            _logger = logger;
            _smilesService = smilesService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SmilesEntity>> Get(int id)
        {
            var result = await _smilesService.GetSmilesEntityById(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSmiles(int id, SmilesEntity smiles)
        {
            var existing = await _smilesService.GetSmilesEntityById(id);
            if (existing == null)
            {
                return NotFound();
            }

            try
            {
                await _smilesService.UpdateSmilesEntity(existing, smiles);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);                
            }

            return Ok();            
        }

        [HttpPost]
        public async Task<ActionResult> CreateSmiles([FromBody]SmilesEntity smiles)
        {
            var result = await _smilesService.CreateSmilesEntity(smiles);

            if (result != null)
            {
                return Ok();
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSmiles(int id)
        {
            await _smilesService.DeleteSmilesEntity(id);

            return Ok();
        }

    }
}
