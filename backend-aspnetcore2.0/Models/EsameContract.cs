using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Woa.ApiModels
{
    public class EsameContract
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

        [ForeignKey("Tipo")]
        [Column("Tipo")]
        [Required]
        public int TipoId { get; set; }

        [Required]
        public string Descrizione { get; set; }

        public Models.Esame ToDomain()
        {
            return new Models.Esame
            {
                ID = this.ID,
                PazienteId = this.PazienteId,
                ConsultoId = this.ConsultoId,
                Data = this.Data,
                TipoId = this.TipoId,
                Descrizione = this.Descrizione
            };
        }
    }
}