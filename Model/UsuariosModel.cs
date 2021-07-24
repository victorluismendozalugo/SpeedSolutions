using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Model
{
    public class UsuariosModel
    {
        public int UsuarioID { get; set; }
        public string UsuarioClave { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellido { get; set; }
        public string UsuarioContraseña { get; set; }
        public byte UsuarioEstatus { get; set; }
    }
}