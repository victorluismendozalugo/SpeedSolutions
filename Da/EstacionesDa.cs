using SpeedSolutions.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Da
{
    public class EstacionesDa : Base
    {
        public List<EstacionesModel> EstacionesConsultar(EstacionesModel estaciones)
        {
            var result = base.Consultar<EstacionesModel>("procEstacionesCon", new List<SqlParameter>()
            {
                new SqlParameter("@pEstacionClave", estaciones.EstacionClave)
          });
            return result;
        }
    }
}