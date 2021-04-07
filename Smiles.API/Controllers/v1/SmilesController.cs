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
    public class SmilesController : BaseSmilesController
    {
        public SmilesController(ILogger<SmilesController> logger, ISmilesService smilesService) : base(logger, smilesService)
        {
        }
    }
}
