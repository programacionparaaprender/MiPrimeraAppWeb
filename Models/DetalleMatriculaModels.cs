using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DetalleMatriculaModels
    {
        public int IIDMATRICULA { get; set; }

        public int IIDCURSO { get; set; }

        public System.Nullable<decimal> NOTA1 { get; set; }

        public System.Nullable<decimal> NOTA2 { get; set; }

        public System.Nullable<decimal> NOTA3 { get; set; }

        public System.Nullable<decimal> NOTA4 { get; set; }

        public System.Nullable<decimal> PROMEDIO { get; set; }

        public System.Nullable<int> bhabilitado { get; set; }
    }
}
