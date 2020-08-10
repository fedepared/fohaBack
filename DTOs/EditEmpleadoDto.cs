using System;
using System.ComponentModel.DataAnnotations;

namespace Foha.Dtos
{

    public class EditEmpleadoDto
    {
        [Required]
        
        public string IdEmpleado { get; set; }
        public string NombreEmp { get; set; }
        public int? IdSector {get;set;}

        // public ICollection<Usuario> Usuario { get; set; }
    }
}