using System;
using System.ComponentModel.DataAnnotations;

namespace Foha.Dtos
{

    public class AddRangoDto
    {
        [Required]
        public int IdRango { get; set; }
        public int? Cantidad { get; set; }

        // public ICollection<Transformadores> Transformadores { get; set; }
    }
}