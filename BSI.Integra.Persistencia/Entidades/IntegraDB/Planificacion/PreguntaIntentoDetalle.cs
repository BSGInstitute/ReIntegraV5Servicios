using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PreguntaIntentoDetalle :BaseIntegraEntity
    {
        public int IdPreguntaIntento { get; set; } 
        public int? PorcentajeCalificacion { get; set; }
    }
}
