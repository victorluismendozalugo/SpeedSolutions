using SpeedSolutions.Da;
using SpeedSolutions.Model;
using SpeedSolutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Bo
{
    public class ProductosBo
    {
        public Respuesta ProductosGuardar(ProductosModel producto) => new ProductosDa().ProductosGuardar(producto);

        public List<ProductosModel> ProductosConsultar(ProductosModel productos) => new ProductosDa().ProductosConsultar(productos);
    }
}