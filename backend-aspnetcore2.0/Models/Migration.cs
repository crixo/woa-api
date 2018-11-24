using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Woa.Models
{
    public class Migration
    {
        [Required]
        [Key]
        [Column("name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Column("created_at_utc")]
        public DateTime? CreatedAtUtc { get; set; }
    }        
}
