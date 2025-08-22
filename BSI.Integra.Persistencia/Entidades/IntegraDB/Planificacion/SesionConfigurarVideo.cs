using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class SesionConfigurarVideo : BaseIntegraEntity
    {
        public int IdConfigurarVideoPrograma { get; set; }
        public int Minuto { get; set; }
        public int IdTipoVista { get; set; }
        public int? NroDiapositiva { get; set; }
        public int? IdEvaluacion { get; set; }
        public bool? ConLogoVideo { get; set; }
        public bool? ConLogoDiapositiva { get; set; }
    }
}
