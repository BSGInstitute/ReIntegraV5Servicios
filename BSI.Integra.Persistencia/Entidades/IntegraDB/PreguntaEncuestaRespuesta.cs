using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PreguntaEncuestaRespuesta : BaseIntegraEntity
    {

        public int IdPreguntaEncuesta { get; set; }
        public string? Respuesta { get; set; }
        public int? Orden { get; set; }
        public decimal? Puntaje { get; set; }
    }
}
