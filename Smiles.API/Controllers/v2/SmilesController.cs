using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Smiles.Core.Models;
using Smiles.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smiles.API.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SmilesController : BaseSmilesController
    {
        private readonly ISmilesService _smilesService;

        public SmilesController(ILogger<SmilesController> logger, ISmilesService smilesService): base(logger, smilesService)
        {
            _smilesService = smilesService;
        }

        [NonAction]
        public override Task<ActionResult> CreateSmiles([FromBody] SmilesEntity smiles)
        {
            return base.CreateSmiles(smiles);
        }

        /// <summary>
        /// Honestly I would just simply add this extra parameter to the model
        /// </summary>
        /// <param name="imageFormat"></param>
        /// <param name="smiles"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateSmiles([FromBody] SmilesEntity smiles, [FromQuery] string imageFormat = null)
        {
            if ((imageFormat == null) || 
                (imageFormat.ToLower() != AllowedFormats.PNG && 
                imageFormat.ToLower() != AllowedFormats.SVG))
            {
                return BadRequest();
            }

            smiles.ImageFormat = imageFormat;

            var result = await _smilesService.CreateSmilesEntity(smiles);

            if (result != null)
            {
                return Ok();
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

    }
}
