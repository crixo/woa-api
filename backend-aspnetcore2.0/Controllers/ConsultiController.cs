using Microsoft.AspNetCore.Mvc;
using Woa.ApiModels;
using Woa.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Woa.Controllers
{
    [Route("api/consulti")]
    public class ConsultiController : CrudControllerBase<ConsultiController>
    {

        public ConsultiController(WoaContext context, ILogger<ConsultiController> logger)
            : base(context, logger){}

        [HttpPost]
        [ProducesResponseType(typeof(ConsultoContract), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Post([FromBody]ConsultoContract consulto) => Store(consulto.ToDomain(), EntityState.Added);

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ConsultoContract), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Put([FromBody]ConsultoContract consulto) => Store(consulto.ToDomain(), EntityState.Modified);

        [HttpDelete("{id}")]
        public void Delete(int id) => Remove(Context.Consulti.SingleOrDefault(x => x.ID == id));
    }
}
