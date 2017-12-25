using System;
using System.ComponentModel.DataAnnotations;

namespace Woa.Models
{
    public class Provincia
    {
        [Key]
        public string Sigla { get; set; }
        public string Descrizione { get; set; }

        public Provincia()
        {
        }
    }
}
