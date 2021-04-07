using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Smiles.Core.Models;
using Smiles.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smiles.API.Controllers
{
    [ApiController]
    public abstract class BaseSmilesController : ControllerBase
    {
        private readonly ILogger<BaseSmilesController> _logger;
        private readonly ISmilesService _smilesService;

        public BaseSmilesController(ILogger<BaseSmilesController> logger, ISmilesService smilesService)
        {
            _logger = logger;
            _smilesService = smilesService;
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<SmilesEntity>> Get(int id)
        {
            var result = await _smilesService.GetSmilesEntityById(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> UpdateSmiles(int id, SmilesEntity smiles)
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
        public virtual async Task<ActionResult> CreateSmiles(SmilesEntity smiles)
        {
            var result = await _smilesService.CreateSmilesEntity(smiles);

            if (result != null)
            {
                return Ok();
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> DeleteSmiles(int id)
        {
            await _smilesService.DeleteSmilesEntity(id);

            return Ok();
        }
    }
}
