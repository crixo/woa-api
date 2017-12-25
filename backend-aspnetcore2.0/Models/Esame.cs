using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Woa.Models
{
    public class Esame : ApiModels.EsameContract
    {
        public TipoEsame Tipo { get; set; }
    }
}