using SpeedSolutions.Model;
using SpeedSolutions.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Da
{
    public class ProductosDa : Base
    {
        public Respuesta ProductosGuardar(ProductosModel producto)
        {
            var result = base.Guardar("procProductosGuardar", new List<SqlParameter>()
            {
                new SqlParameter("@pEstacionClave", producto.EstacionClave),
                new SqlParameter("@pProductoClave", producto.ProductoClave),
                new SqlParameter("@pProductoNombre", producto.ProductoNombre),
                new SqlParameter("@pProuctoPrecio", producto.ProductoPrecio)
          });
            return result;
        }

        public List<ProductosModel> ProductosConsultar(ProductosModel productos)
        {
            var result = base.Consultar<ProductosModel>("procProductosCon", new List<SqlParameter>()
            {
                new SqlParameter("@pEstacionClave", productos.EstacionClave),
                new SqlParameter("@pProductoClave", productos.ProductoClave)
          });
            return result;
        }
    }
}