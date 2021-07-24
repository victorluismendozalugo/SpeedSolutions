using Newtonsoft.Json;
using SpeedSolutions.Bo;
using SpeedSolutions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeedSolutions
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((string)Session["UsuarioClave"]))
            {
                Session.Clear();
            }
        }

        [System.Web.Services.WebMethod]
        public static string Logeo(UsuariosModel login)
        {
            Encriptacion encrypta = new Encriptacion();
            login.UsuarioContraseña = encrypta.EncriptarContraseña(login.UsuarioClave, login.UsuarioContraseña);
            bool Autentifica = new UsuariosBo().UsuarioAutentificar(login);
            if (Autentifica)
            {
                Login loginSesion = new Login();
                loginSesion.GeneraVariablesSesion(login.UsuarioClave);
            }
            return JsonConvert.SerializeObject(Autentifica);
        }

        public void GeneraVariablesSesion(string usuario)
        {
            Session["UsuarioClave"] = usuario;
        }
    }
}