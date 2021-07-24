using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Model
{
    public class ProductosModel
    {
        public int EstacionClave { get; set; }
        public string EstacionNombre { get; set; }
        public int ProductoClave { get; set; }
        public string ProductoNombre { get; set; }
        public double ProductoPrecio { get; set; }
        public string FechaActualizacion { get; set; }
    }
}