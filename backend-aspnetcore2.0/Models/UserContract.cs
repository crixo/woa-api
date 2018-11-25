using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Woa.ApiModels
{
    public class PazienteContract
    {

        public int ID { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Indirizzo { get; set; }

        public string Citta { get; set; }

        public string Professione { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public string Cellulare { get; set; }

        public string Prov { get; set; }

        [MaxLength(5)]
        public string CAP { get; set; }

        [Column("data_nascita")]
        [DataType(DataType.Date)]
        public DateTime DataDiNascita { get; set; }

        public Models.Paziente ToDomain()
        {
            return new Models.Paziente
            {
                ID = this.ID,
                Cognome = this.Cognome,
                Nome = this.Nome,
                Indirizzo = this.Indirizzo,
                Citta = this.Citta,
                Professione = this.Professione,
                Email = this.Email,
                Telefono = this.Telefono,
                Cellulare = this.Cellulare,
                Prov = this.Prov,
                CAP = this.CAP,
                DataDiNascita=this.DataDiNascita
            };
        }
    }


}
