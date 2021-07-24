using SpeedSolutions.Da;
using SpeedSolutions.Model;
using SpeedSolutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Bo
{
    public class UsuariosBo
    {
        Encriptacion encrypta = new Encriptacion();
        ClaveUsuario clave = new ClaveUsuario();
        public bool UsuarioAutentificar(UsuariosModel login)
        {
            string res = new UsuariosDa().UsuarioAutentificar(login).ToString();
            return  res != login.UsuarioContraseña ? false : true;
        }

        public Respuesta UsuariosGuardar(UsuariosModel usuario)
        {
            usuario.UsuarioClave = GeneraClaveUsuario(usuario);
            usuario.UsuarioContraseña = GeneraContraseña(usuario);
            return new UsuariosDa().UsuariosGuardar(usuario);
        }

        public List<UsuariosModel> UsuariosConsultar(UsuariosModel usuario) => new UsuariosDa().UsuariosConsultar(usuario);

        public string GeneraClaveUsuario(UsuariosModel usuario)
        {
            return (usuario.UsuarioNombre + "" + usuario.UsuarioApellido.Substring(0, 1) + "" + clave.obtieneclave());
        }

        public string GeneraContraseña(UsuariosModel usuario)
        {
            return encrypta.EncriptarContraseña(usuario.UsuarioClave, usuario.UsuarioContraseña);
        }
    }
}