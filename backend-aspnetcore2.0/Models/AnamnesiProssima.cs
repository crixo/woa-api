using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Woa.Models
{
    public class AnamnesiProssima
    {
        [Key]
        [Required]
        [Column("ID_consulto")]
        public int ConsultoId { get; set; }  

        [Required]
        [Column("ID_paziente")]
        public int PazienteId { get; set; }

        [Column("prima_volta")]
        public string PrimaVolta { get; set; }

        public string Tipologia { get; set; }

        public string Localizzazione { get; set; }

        public string Irradiazione { get; set; }

        [Column("periodo_insorgenza")]
        public string PeriodoInsorgenza { get; set; }

        public string Durata { get; set; }

        public string Familiarita { get; set; }

        [Column("altre_terapie")]
        public string AltreTerapie { get; set; }

        public string Varie { get; set; }
    }
}
