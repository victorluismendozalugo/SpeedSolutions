using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SpeedSolutions
{
    public class Encriptacion
    {
        public string EncriptarContraseña(string strIdUmedica, string strUsuario, string strPassword)
        {
            SHA512Managed mhash = default(SHA512Managed);
            byte[] bytValue = null;
            byte[] bytHash = null;
            string strClave = (strIdUmedica.ToUpper() + strUsuario.ToUpper());

            mhash = new SHA512Managed();

            bytValue = System.Text.Encoding.UTF8.GetBytes(strClave);

            bytHash = mhash.ComputeHash(bytValue);

            mhash.Clear();
            return Convert.ToBase64String(bytHash);
        }


        public string EncriptarContraseña(string email, string strPassword)
        {
            SHA512Managed mhash = default(SHA512Managed);
            byte[] bytValue = null;
            byte[] bytHash = null;
            string strClave = (email.ToUpper() + strPassword.ToUpper());

            mhash = new SHA512Managed();

            bytValue = System.Text.Encoding.UTF8.GetBytes(strClave);

            bytHash = mhash.ComputeHash(bytValue);

            mhash.Clear();
            return Convert.ToBase64String(bytHash);
        }
    }
}