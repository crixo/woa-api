using Microsoft.AspNetCore.Mvc;
using Woa.ApiModels;
using Woa.Models;
using Woa.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Woa.Controllers
{
    [Route("api/anamnesi-prossime")]
    public class AnamnesiProssimeController : CrudControllerBase<AnamnesiProssimeController>
    {
        public AnamnesiProssimeController(WoaContext context, ILogger<AnamnesiProssimeController> logger)
            : base(context, logger) { }

        [HttpPost]
        [ProducesResponseType(typeof(AnamnesiProssima), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Post([FromBody]AnamnesiProssima contract) => Store(contract, EntityState.Added);

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Trattamento), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Put([FromBody]AnamnesiProssima contract) => Store(contract, EntityState.Modified);

        [HttpDelete("{id}")]
        public void Delete(int id) => Remove(Context.AnamnesiProssime.SingleOrDefault(x => x.ConsultoId == id));
    }
}
