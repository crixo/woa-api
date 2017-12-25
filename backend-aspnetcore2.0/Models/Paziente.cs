using System.Collections.Generic;

namespace Woa.Models
{
    public class Paziente : Woa.ApiModels.PazienteContract
    {
        public ICollection<AnamnesiRemota> AnamnesiRemote { get; set; }

        public ICollection<Consulto> Consulti { get; set; }

        public Paziente()
        {
        }
    }
}
