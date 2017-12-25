using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Woa.Models
{
    [Table("lkp_anamnesi")]
    public class TipoAnamnesi
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Descrizione { get; set; }
    }
}
