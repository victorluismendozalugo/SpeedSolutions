using SpeedSolutions.Da;
using SpeedSolutions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Bo
{
    public class EstacionesBo
    {
        public List<EstacionesModel> EstacionesConsultar(EstacionesModel estaciones) => new EstacionesDa().EstacionesConsultar(estaciones);
    }
}