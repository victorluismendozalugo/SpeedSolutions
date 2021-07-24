using Newtonsoft.Json;
using SpeedSolutions.Bo;
using SpeedSolutions.Model;
using SpeedSolutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeedSolutions
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["UsuarioClave"] == "" || Session["UsuarioClave"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        [System.Web.Services.WebMethod]
        public static IRespuesta UsuarioGuardar(UsuariosModel usuario)
        {
            try
            {
                return new UsuariosBo().UsuariosGuardar(usuario);
            }
            catch (Exception ex)
            {
                return Respuesta.PublishException(ex);
            }
        }

        [System.Web.Services.WebMethod]
        public static string UsuariosConsultar(UsuariosModel usuario)
        {
            List<UsuariosModel> resultado = new UsuariosBo().UsuariosConsultar(usuario);
            return JsonConvert.SerializeObject(resultado);
        }

        [System.Web.Services.WebMethod]
        public static IRespuesta ProductosGuardar(ProductosModel producto)
        {
            try
            {
                return new ProductosBo().ProductosGuardar(producto);
            }
            catch (Exception ex)
            {
                return Respuesta.PublishException(ex);
            }
        }

        [System.Web.Services.WebMethod]
        public static string EstacionesConsultar(EstacionesModel estaciones)
        {
            List<EstacionesModel> resultado = new EstacionesBo().EstacionesConsultar(estaciones);
            return JsonConvert.SerializeObject(resultado);
        }

        [System.Web.Services.WebMethod]
        public static string ProductosConsultar(ProductosModel productos)
        {
            List<ProductosModel> resultado = new ProductosBo().ProductosConsultar(productos);
            return JsonConvert.SerializeObject(resultado);
        }
    }
}