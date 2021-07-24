using SpeedSolutions.Model;
using SpeedSolutions.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Da
{
    public class UsuariosDa : Base

    {
        public string UsuarioAutentificar(UsuariosModel login)
        {
            string result = base.Consulta("procUsuariosAutentificar", new List<SqlParameter>()
            {
                new SqlParameter("@pUsuarioClave", login.UsuarioClave),
                new SqlParameter("@pUsuarioContraseña", login.UsuarioContraseña),
          }, "UsuarioContraseña");

            return result;
        }

        public Respuesta UsuariosGuardar(UsuariosModel usuario)
        {
            var result = base.Guardar("procUsuariosGuardar", new List<SqlParameter>()
            {
                new SqlParameter("@pUsuarioID", usuario.UsuarioID),
                new SqlParameter("@pUsuarioClave", usuario.UsuarioClave),
                new SqlParameter("@pUsuarioNombre", usuario.UsuarioNombre),
                new SqlParameter("@pUsuarioApellido", usuario.UsuarioApellido),
                new SqlParameter("@pUsuarioContraseña", usuario.UsuarioContraseña),
                new SqlParameter("@pUsuarioEstatus", usuario.UsuarioEstatus)

          });
            return result;
        }

        public List<UsuariosModel> UsuariosConsultar(UsuariosModel usuario)
        {
            var result = base.Consultar<UsuariosModel>("procUsuariosCon", new List<SqlParameter>()
            {
                new SqlParameter("@pUsuarioID", usuario.UsuarioID)
          });
            return result;
        }
    }
}

