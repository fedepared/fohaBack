using System;
using System.Collections.Generic;


namespace Foha.Models
{
    public partial class EtapaEmpleado
    {        

        public string IdEmpleado { get; set; }
        public int? IdEtapa { get; set; }

        public DateTime? DateIni {get;set;}
        public DateTime? DateFin {get;set;}
        public string TiempoParc {get;set;}
        public bool? IsEnded {get;set;}
        public string TiempoFin{get;set;}

        public Empleado Empleado { get; set; }
        public Etapa Etapa { get; set; }

        
    }
}
