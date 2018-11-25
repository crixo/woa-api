using Microsoft.AspNetCore.Mvc;
using Woa.ApiModels;
using Woa.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Woa.Controllers
{
    [Route("api/esami")]
    public class EsamiController : CrudControllerBase<EsamiController>
    {
        public EsamiController(WoaContext context, ILogger<EsamiController> logger)
            : base(context, logger) { }

        [HttpPost]
        [ProducesResponseType(typeof(EsameContract), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Post([FromBody]EsameContract contract) => Store(contract.ToDomain(), EntityState.Added);

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EsameContract), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Put([FromBody]EsameContract contract) => Store(contract.ToDomain(), EntityState.Modified);

        [HttpDelete("{id}")]
        public void Delete(int id) => Remove(Context.Esami.SingleOrDefault(x => x.ID == id));
    }
}
