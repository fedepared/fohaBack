using System;
using System.Collections.Generic;

namespace Foha.Models
{
    public partial class TipoEtapa
    {
        public TipoEtapa()
        {
            Etapa = new HashSet<Etapa>();
        }

        public int IdTipoEtapa { get; set; }
        public string NombreEtapa { get; set; }

        public ICollection<Etapa> Etapa { get; set; }
    }
}
