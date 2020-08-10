using System;
using System.ComponentModel.DataAnnotations;

namespace Foha.Dtos
{

    public class EditUsuarioDto
    {
        [Required]
       public int IdUser { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreUs { get; set; }
        public string TokenRefresh { get; set; }
        public string Pass { get; set; }
        public bool IsOp { get; set; }

        // public Empleado IdEmpleadoNavigation { get; set; }
    }
}