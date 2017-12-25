using System;
using Microsoft.AspNetCore.Mvc;
using Woa.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Woa.ApiModels;

namespace Woa.Controllers
{
    public class CrudControllerBase<TController> : Controller
        where TController : Controller
    {
        protected WoaContext Context;
        protected ILogger<TController> Logger;

        protected CrudControllerBase(WoaContext context, ILogger<TController> logger)
        {
            Context = context;
            Logger = logger;
            Logger.LogInformation(typeof(TController).Name);
        }

        protected IActionResult Store<TEntity>(TEntity entity, EntityState state) where TEntity : class
        {
            try
            {
                Context.Entry(entity).State = state;
                Context.SaveChanges();
                return Ok(entity);
            }
            catch (Exception exp)
            {
                Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false, ErrorMessage = exp.Message });
            }
        }
    }
}
