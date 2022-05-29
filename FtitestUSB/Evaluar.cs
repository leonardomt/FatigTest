using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtitestUSB
{
    class Evaluar
    {

        public String signos { get; set; }

        public String puntos { get; set; }
        public String valor { get; set; }

        public Evaluar(String signos, String valor)
        {
            this.signos = signos;
            this.valor = valor;
            puntos = "3210123";
        }
    }
}
