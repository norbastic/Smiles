using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Smiles.Core.Models;
using Smiles.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smiles.API.Controllers
{
    [ApiController]
    public abstract class BaseSmilesController : ControllerBase
    {
        private readonly ILogger<BaseSmilesController> _logger;
        private readonly ISmilesService _smilesService;

        private async Task<string> ReadStreamAsync(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            return await reader.ReadToEndAsync();
        }

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
        [Route("upload")]
        public virtual async Task<ActionResult<int>> UploadSmiles()
        {
            try
            {
                var form = await Request.ReadFormAsync();
                var file = form.Files.FirstOrDefault();

                if (file != null && file.Length > 0)
                {
                    var result = await ReadStreamAsync(file);
                    var smiles = result.Split(Environment.NewLine);

                    foreach (var item in smiles)
                    {
                        await _smilesService.CreateSmilesEntity(new SmilesEntity() { Data = item });
                    }

                    return Ok(smiles.Count());
                }

                return BadRequest(-1);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);                
            }
        }


        [HttpPost]
        public virtual async Task<ActionResult> CreateSmiles([FromBody] SmilesEntity smiles)
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
