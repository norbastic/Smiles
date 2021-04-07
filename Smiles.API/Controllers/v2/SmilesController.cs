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
        public SmilesController(ILogger<SmilesController> logger, ISmilesService smilesService): base(logger, smilesService)
        {
        }

        [HttpPost]
        public async override Task<ActionResult> CreateSmiles([FromBody] string smiles)
        {
            // Do nothing just log;
            Console.WriteLine($"Smiles data: {smiles.Data}");

            return Ok();
        }

    }
}
