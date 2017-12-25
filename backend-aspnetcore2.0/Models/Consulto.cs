using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Woa.Models
{
    public class Consulto : Woa.ApiModels.ConsultoContract
    {
        [NotMapped]
        public AnamnesiProssima AnamnesiProssima { get; set; }

        public ICollection<Trattamento> Trattamenti { get; set; }
        public ICollection<Esame> Esami { get; set; }
        public ICollection<Valutazione> Valutazioni { get; set; }
    }
}
