using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Woa.Infrastructure;

namespace Woa.ApiModels
{
    public class UserContract
    {

        public int ID { get; set; }

        [Required]
        [Column("username")]
        public string UserName { get; set; }

        [NotMapped]
        public string Password { get; set; }


        public Models.User ToDomain()
        {
            var salt = Salt.Create();
            var hash = Hash.Create(this.Password, salt);
            return new Models.User
            {
                ID = this.ID,
                UserName = this.UserName,
                PasswordHashed = hash,
                Salt = salt
            };
        }
    }

    public class UserAuthenticateContract
    {
        public string UserName { get; set; }

        public string Password { get; set; }        
    }
}
