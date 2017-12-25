using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Woa.ApiModels
{
    public class ConsultoContract
    {
        public int ID { get; set; }

        [Required]
        [Column("ID_paziente")]
        public int PazienteId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Required]
        public DateTime? Data { get; set; }

        [Required]
        [Display(Name = "Problema Iniziale")]
        [Column("problema_iniziale")]
        public string ProblemaIniziale { get; set; }

        public Models.Consulto ToDomain()
        {
            return new Models.Consulto 
            { 
                ID=this.ID,
                PazienteId=this.PazienteId,
                Data = this.Data,
                ProblemaIniziale=this.ProblemaIniziale
            };
        }
    }        
}
