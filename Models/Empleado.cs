using System;
using System.Collections.Generic;

namespace Foha.Models
{
    public partial class Empleado
    {
        public string IdEmpleado { get; set; }
        public string NombreEmp { get; set; }
        public int? IdSector {get;set;}
       
       public ICollection <EtapaEmpleado> EtapaEmpleado {get;set;}
       public Sectores Sector{get;set;}
    }
}
