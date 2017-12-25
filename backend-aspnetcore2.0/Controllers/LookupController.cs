using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Woa.Data;
using Microsoft.Extensions.Logging;

namespace Woa.Controllers
{
    [Route("api/lookups")]
    public class LookupController : Controller
    {
        private readonly WoaContext _context;
        private readonly ILogger<LookupController> _logger;

        public LookupController(WoaContext context, ILogger<LookupController> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation("LookupController");
        }

        // GET: /<controller>/
        [HttpGet("province")]
        public IActionResult GetProvince()
        {
            return Ok(_context.Province.ToList());
        }

        [HttpGet("tipo-esami")]
        public IActionResult GetTipoEsami()
        {
            return Ok(_context.TipoEsami.ToList());
        }

        [HttpGet("tipo-anamnesi")]
        public IActionResult GetTipoAnamnesi()
        {
            return Ok(_context.TipoAnamnesi.ToList());
        }
    }
}
