using Microsoft.AspNetCore.Mvc;
using Woa.ApiModels;
using Woa.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Woa.Infrastructure;

namespace Woa.Controllers
{
    [Route("api/users")]
    public class UsersController : CrudControllerBase<UsersController>
    {
        public UsersController(WoaContext context, ILogger<UsersController> logger)
            : base(context, logger) { }

        [HttpGet()]
        public IActionResult GetUsers()
        {
            return Ok(Context.Users.Select(x=>x.ToContract()).ToList());
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserContract), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Post([FromBody]UserContract contract) => StoreAndReturnContract(contract.ToDomain(), EntityState.Added);

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserContract), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Put([FromBody]UserContract contract) => StoreAndReturnContract(contract.ToDomain(), EntityState.Modified);

        [HttpDelete("{id}")]
        public void Delete(int id) => Remove(Context.Users.SingleOrDefault(x => x.ID == id));

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(UserContract), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult Authenticate([FromBody]UserAuthenticateContract contract)
        {
            try
            {
                var user = Context.Users.SingleOrDefault(x => x.UserName == contract.UserName);
                if(user == null)
                {
                    Logger.LogInformation("User w/ username {0} not found", contract.UserName);
                    throw (new ApplicationException("Authentication failed"));
                }

                var validationResult = Hash.Validate(contract.Password, user.Salt, user.PasswordHashed);
                if(!validationResult)
                {
                    Logger.LogInformation("User {0} did not provide a valid pwd", contract.UserName);
                    throw (new ApplicationException("Authentication failed"));
                }


                return Ok(user.ToContract());
            }
            catch (Exception exp)
            {
                Logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false, ErrorMessage = exp.Message });
            }
        }

    }
}
