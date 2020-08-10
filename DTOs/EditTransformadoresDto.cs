using System;
using System.ComponentModel.DataAnnotations;
using Foha.Models;

namespace Foha.Dtos
{

public class EditTransformadoresDto
    {
        [Required]
        public int IdTransfo { get; set; }
        public string OPe { get; set; }
        public int? IdRango { get; set; }
        public int? OTe { get; set; }
        public int? IdCliente { get; set; }
        public string Observaciones { get; set; }
        public int? IdOrden { get; set; }

        public int potencia{get;set;}

        public int rangoInicio {get;set;}
        public int rangoFin {get;set;}

        public string nombreCli {get;set;}

        public int? idTipoTransfo{get;set;}

        public int? Anio {get; set;}
        public int? Mes {get;set;}

        public int? Prioridad { get; set;}

        


        // public static implicit operator EditTransformadoresDto(Transformadores v)
        // {
        //     throw new NotImplementedException();
        // }



        // public Cliente IdClienteNavigation { get; set; }
        // public OrdenDePedidos IdOrdenNavigation { get; set; }
        // public Rango IdRangoNavigation { get; set; }
        // public ICollection<Etapa> Etapa { get; set; }
    }
}