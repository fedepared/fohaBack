using Foha.Models;

namespace Foha.Dtos
{

    public class UsuarioResponseDto
    {
  
       public int IdUser { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreUs { get; set; }
        public string Token { get; set; }
        public string Pass { get; set; }
        public bool IsOp { get; set; }

        public Empleado IdEmpleadoNavigation { get; set; }
    }
}