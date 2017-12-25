using Microsoft.AspNetCore.Mvc;
using Woa.ApiModels;
using Woa.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Woa.Controllers
{
    [Route("api/anamnesi-remote")]
    public class AnamnesiRemoteController : CrudControllerBase<AnamnesiRemoteController>
    {
        public AnamnesiRemoteController(WoaContext context, ILogger<AnamnesiRemoteController> logger)
            : base(context, logger) { }

        [HttpPost]
        [ProducesResponseType(typeof(AnamnesiRemotaContract), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Post([FromBody]AnamnesiRemotaContract contract) => Store(contract.ToDomain(), EntityState.Added);

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AnamnesiRemotaContract), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Put([FromBody]AnamnesiRemotaContract contract) => Store(contract.ToDomain(), EntityState.Modified);

        [HttpDelete("{id}")]
        public void Delete(int id) => Remove(Context.AnamnesiRemote.SingleOrDefault(x => x.ID == id));
    }
}
