using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoArbolesBinarios
{
    class Nodo
    {
        public char dato { get; set; }
        public int numero { get; set; }
        public Nodo izquierda { get; set; }
        public Nodo derecha { get; set; }
        public Nodo anterior { get; set; }
        public Nodo siguiente { get; set; }
    }
}
