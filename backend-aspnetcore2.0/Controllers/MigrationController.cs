using Microsoft.AspNetCore.Mvc;
using Woa.Models;
using Woa.ApiModels;
using Woa.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Data.SqlClient;

namespace Woa.Controllers
{
    [Route("api/migrations")]
    public class MigrationController : CrudControllerBase<MigrationController>
    {
        private readonly IHostingEnvironment _env;

        public MigrationController(
            IHostingEnvironment env,
            WoaContext context, ILogger<MigrationController> logger)
            : base(context, logger) {
            this._env = env;
        }

        [HttpPost("migrate")]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Migrate()
        {
            var executedScripts = new List<string>();

            var scriptsFolderPath = Path.Combine(_env.ContentRootPath, "Data", "scripts");

            var filePaths = Directory.GetFiles(scriptsFolderPath).OrderBy(f=>f);

            this.Logger.LogDebug("scriptsFolderPath: {0}", scriptsFolderPath);

            //Context.Database.BeginTransaction();


            try
            {
                var migrationTableScript = "CREATE TABLE IF NOT EXISTS [wp_migration] ([name] nvarchar(50) PRIMARY KEY, [created_at_utc] DATETIME DEFAULT CURRENT_TIMESTAMP);";
                Context.Database.ExecuteSqlCommand(migrationTableScript);

                foreach (var path in filePaths)
                {
                    var scriptName = Path.GetFileName(path);
                    this.Logger.LogDebug("scriptName: {0}", scriptName);

                    Migration executedMigration = Context.Migrations.SingleOrDefault<Migration>(x=>x.Name == scriptName);

                    if (executedMigration == null)
                    {
                        var sqlScript = System.IO.File.ReadAllText(path);
                        Context.Database.ExecuteSqlCommand(sqlScript);
                        var m = new Migration { Name = scriptName, CreatedAtUtc=DateTime.UtcNow };
                        Context.Migrations.Add(m);
                        Context.SaveChanges();
                        executedScripts.Add(m.Name);
                    }
                }


                //Context.Database.CommitTransaction();
            }
            catch(Exception exc)
            {
                //Context.Database.RollbackTransaction();
                var errorMsg = exc.ToString();
                this.Logger.LogError(errorMsg);
                return StatusCode(500, errorMsg);
            }

            return Ok(executedScripts);
        }
    }
}
