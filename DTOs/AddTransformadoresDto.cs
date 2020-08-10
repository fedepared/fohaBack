using System;
using System.ComponentModel.DataAnnotations;

namespace Foha.Dtos
{

public partial class AddTransformadoresDto
    {
        
        public int IdTransfo { get; set; }
        public string OPe { get; set; }
        // public int? IdRango { get; set; }
        public int? OTe { get; set; }
        public int? IdCliente { get; set; }
        public string Observaciones { get; set; }
        public int? IdOrden { get; set; }

        public int rangoInicio {get;set;}

        public int rangoFin {get;set;}

        public int Potencia { get; set; }

        public string nombreCli{get;set;}

        public int? cantTransfo{get;set;}

        public DateTime? FechaCreacion{get;set;}
        
        public int? IdTipoTransfo{get;set;}

        public int? Anio {get; set;}
        public int? Mes {get;set;}

        public int? Prioridad { get; set;}
        

        // public Cliente IdClienteNavigation { get; set; }
        // public OrdenDePedidos IdOrdenNavigation { get; set; }
        // public Rango IdRangoNavigation { get; set; }
        // public ICollection<Etapa> Etapa { get; set; }
    }
}