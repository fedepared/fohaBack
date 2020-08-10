using System;
using System.Collections.Generic;

namespace Foha.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Transformadores = new HashSet<Transformadores>();
        }

        public int IdCliente { get; set; }
        public string NombreCli { get; set; }

        public ICollection<Transformadores> Transformadores { get; set; }
    }
}
