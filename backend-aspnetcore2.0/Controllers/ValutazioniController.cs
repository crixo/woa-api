using Microsoft.AspNetCore.Mvc;
using Woa.ApiModels;
using Woa.Models;
using Woa.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Woa.Controllers
{
    [Route("api/valutazioni")]
    public class ValutazioniController : CrudControllerBase<ValutazioniController>
    {
        public ValutazioniController(WoaContext context, ILogger<ValutazioniController> logger)
            : base(context, logger) { }

        [HttpPost]
        [ProducesResponseType(typeof(Valutazione), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Post([FromBody]Valutazione contract) => Store(contract, EntityState.Added);

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Valutazione), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Put([FromBody]Valutazione contract) => Store(contract, EntityState.Modified);

        [HttpDelete("{id}")]
        public void Delete(int id) => Remove(Context.Valutazioni.SingleOrDefault(x => x.ID == id));
    }
}
