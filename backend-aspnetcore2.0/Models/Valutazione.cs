using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Woa.Models
{
    public class Valutazione
    {
        public int ID { get; set; }

        [Required]
        [Column("ID_paziente")]
        public int PazienteId { get; set; }

        [Required]
        [Column("ID_consulto")]
        public int ConsultoId { get; set; }

        public string Strutturale { get; set; }

        [Column("cranio_sacrale")]
        public string CranioSacrale { get; set; }

        [Column("ak_ortodontica")]
        public string AkOrtodontica { get; set; }
    }
}
