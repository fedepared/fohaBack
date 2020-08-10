using System;
using System.Collections.Generic;


namespace Foha.Models
{
    public partial class TransfoEtap
    {        

        public int IdEtapa { get; set; }
        public int IdTransfo { get; set; }

        public Transformadores Transformadores { get; set; }
        public Etapa Etapa { get; set; }
        
    }
}
