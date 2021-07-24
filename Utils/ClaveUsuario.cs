using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Utils
{
    public class ClaveUsuario
    {
        public string obtieneclave()
        {
            var random = new Random();
            return random.Next(0, 3).ToString();
        }
    }
}