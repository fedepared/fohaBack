using System;
using System.Collections.Generic;

namespace Foha.Models
{
    public partial class Transformadores
    {
        public Transformadores()
        {
            Etapa = new HashSet<Etapa>();
        }

        public int IdTransfo { get; set; }
        public string OPe { get; set; }
        public int? OTe { get; set; }
        public string Observaciones { get; set; }
        public int RangoInicio { get; set; }
        public int RangoFin { get; set; }
        public int? IdCliente { get; set; }
        public string NombreCli { get; set; }
        public int Potencia { get; set; }
        public int? IdTipoTransfo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public int? Prioridad { get; set; }

        public Cliente IdClienteNavigation { get; set; }
        public TipoTransfo IdTipoTransfoNavigation { get; set; }
        public ICollection<Etapa> Etapa { get; set; }
    }
}
