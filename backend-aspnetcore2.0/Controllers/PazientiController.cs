using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Woa.Infrastructure;
using Woa.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Woa.ApiModels;

namespace Woa.Controllers
{
    [Route("api/pazienti")]
    public class PazientiController : CrudControllerBase<PazientiController>
    {

        public PazientiController(WoaContext context, ILogger<PazientiController> logger)
            : base(context, logger) { }

        // GET api/values
        [HttpGet("page/{skip}/{take}")]
        [NoCache]
        [ProducesResponseType(typeof(PagingResult<PazienteContract>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> PazientiPage(int skip, int take, string filter = null)
        {
            try
            {
                var pagingResult = await GetPazientiPageAsync(skip, take, filter);
                Response.Headers.Add("X-InlineCount", pagingResult.TotalRecords.ToString());
                return Ok(pagingResult);
            }
            catch (Exception exp)
            {
                Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpGet("{id}", Name = "GetPazienteRoute")]
        [NoCache]
        [ProducesResponseType(typeof(Models.Paziente), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> GetPaziente(int id)
        {
            try
            {
                var paziente = await Context.Pazienti
                                             .Include(x => x.AnamnesiRemote)
                                                .ThenInclude(x => x.Tipo)

                                             .Include(x => x.Consulti)
                                                .ThenInclude(x=>x.Esami)
                                                    .ThenInclude(x => x.Tipo)
                                             .Include(x => x.Consulti)
                                                .ThenInclude(x => x.Trattamenti)
                                             .Include(x => x.Consulti)
                                                .ThenInclude(x => x.Valutazioni)


                                             .SingleOrDefaultAsync(x=>x.ID==id);

                var tipoEsami = await Context.TipoEsami.ToListAsync();
                

                if(paziente != null && paziente.Consulti.Any())
                {
                    foreach(var c in paziente.Consulti)
                    {
                        c.AnamnesiProssima = await Context.AnamnesiProssime.SingleOrDefaultAsync(x=>x.ConsultoId==c.ID);

                        //foreach (var esami in c.Esami)
                        //{
                        //    esami.Tipo = tipoEsami.SingleOrDefault(x => x.ID == esami.TipoId);
                        //}
                    }
                }


                return Ok(paziente);
            }
            catch (Exception exp)
            {
                Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false, ErrorMessage = exp.Message});
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(PazienteContract), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Post([FromBody]PazienteContract paziente) => Store(paziente.ToDomain(), EntityState.Added);

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PazienteContract), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Put(int id, [FromBody]PazienteContract paziente) => Store(paziente.ToDomain(), EntityState.Modified);

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var paziente = Context.Pazienti.SingleOrDefault(x => x.ID == id);
            if (paziente == null) return;

            var consulti = Context.Consulti.Where(x => x.PazienteId == id);
            foreach(var c in consulti)
            {
                var esami = Context.Esami.Where(x=>x.ConsultoId==c.ID);
                foreach(var e in esami)
                {
                    Context.Esami.Remove(e);
                }

                var trattamenti = Context.Trattamenti.Where(x => x.ConsultoId == c.ID);
                foreach (var e in trattamenti)
                {
                    Context.Trattamenti.Remove(e);
                }

                var valutazioni = Context.Valutazioni.Where(x => x.ConsultoId == c.ID);
                foreach (var e in valutazioni)
                {
                    Context.Valutazioni.Remove(e);
                }

                var anamnesiProssima = Context.AnamnesiProssime.SingleOrDefault(x => x.ConsultoId == c.ID);
                if(anamnesiProssima!=null){
                    Context.AnamnesiProssime.Remove(anamnesiProssima);
                }
            }

            var anamnesiRemote = Context.AnamnesiRemote.Where(x => x.PazienteId == id);
            foreach (var e in anamnesiRemote)
            {
                Context.AnamnesiRemote.Remove(e);
            }


            Context.Pazienti.Remove(paziente);

            Context.SaveChanges();
        }

        private async Task<PagingResult<PazienteContract>> GetPazientiPageAsync(int skip, int take, string filter)
        {
            int totalRecords;
            List<Models.Paziente> pazienti;


            Func<PazienteContract, bool> @where = x => x.ID > 0;

            if (string.IsNullOrWhiteSpace(filter)){
                totalRecords = await Context.Pazienti.CountAsync();
                pazienti = await Context.Pazienti
                 .OrderBy(c => c.Cognome)
                 .Skip(skip)
                 .Take(take)
                 .ToListAsync();
            }else{
                var pazientiQuery = Context.Pazienti.Where(x => x.Cognome.ToLower().Contains(filter.ToLower()));
                totalRecords = await pazientiQuery.CountAsync();
                pazienti = await pazientiQuery
                 .OrderBy(c => c.Cognome)
                 .Skip(skip)
                 .Take(take)
                 .ToListAsync(); 
            }

            return new PagingResult<PazienteContract>(pazienti, totalRecords);
        }
    }
}
