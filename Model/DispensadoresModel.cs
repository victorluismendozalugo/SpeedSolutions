using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Model
{
    public class DispensadoresModel
    {
        public int EstacionClave { get; set; }
        public int DispensadorClave { get; set; }
        public string DispensadorNombre { get; set; }
        public List<DispensadoresDetalle> detalle { get; set; }
    }

    public class DispensadoresDetalle
    {
        public int DispensadorClave { get; set; }
        public int EstacionClave { get; set; }
        public int ProductoClave { get; set; }
        public double ProductoUltimoPrecio { get; set; }
    }
}