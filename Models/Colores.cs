using System;
using System.Collections.Generic;

namespace Foha.Models
{
    public partial class Colores
    {
        public Colores()
        {
            Etapa = new HashSet<Etapa>();
        }

        public int IdColor { get; set; }
        public string CodigoColor { get; set; }

        public string Leyenda {get; set; }

        public ICollection<Etapa> Etapa { get; set; }
    }
}
