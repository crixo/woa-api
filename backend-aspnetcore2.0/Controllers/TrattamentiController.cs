using Microsoft.AspNetCore.Mvc;
using Woa.ApiModels;
using Woa.Models;
using Woa.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Woa.Controllers
{
    [Route("api/trattamenti")]
    public class TrattamentiController : CrudControllerBase<TrattamentiController>
    {
        public TrattamentiController(WoaContext context, ILogger<TrattamentiController> logger)
            : base(context, logger) { }

        [HttpPost]
        [ProducesResponseType(typeof(Trattamento), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Post([FromBody]Trattamento contract) => Store(contract, EntityState.Added);

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Trattamento), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Put([FromBody]Trattamento contract) => Store(contract, EntityState.Modified);

        [HttpDelete("{id}")]
        public void Delete(int id) => Remove(Context.Trattamenti.SingleOrDefault(x => x.ID == id));
    }
}
