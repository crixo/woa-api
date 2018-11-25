using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Woa.ApiModels;

namespace Woa.Models
{
    public class User : UserContract, IEntity
    {
        [Required]
        [Column("pwd_hash")]
        public string PasswordHashed { get; set; }

        [Required]
        [Column("salt")]
        public string Salt { get; set; }

        [DataType(DataType.Date)]
        [Column("created_at_utc")]
        public DateTime? CreatedAtUtc { get; set; }


        public object ToContract()
        {
            return new UserContract
            {
                ID = this.ID,
                UserName = this.UserName,
            };
        }
    }


}
