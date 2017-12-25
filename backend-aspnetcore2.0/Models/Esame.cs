using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Woa.Models
{
    public class Esame
    {
        public int ID { get; set; }

        [Required]
        [Column("ID_paziente")]
        public int PazienteId { get; set; }

        [Required]
        [Column("ID_consulto")]
        public int ConsultoId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Required]
        public DateTime? Data { get; set; }

        public TipoEsame Tipo { get; set; }

        [ForeignKey("Tipo")]
        [Column("Tipo")]
        [Required]
        public int TipoId { get; set; }

        [Required]
        public string Descrizione { get; set; }
    }
}