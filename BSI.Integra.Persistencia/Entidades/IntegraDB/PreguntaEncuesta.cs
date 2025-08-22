using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion
{
    public class PreguntaEncuesta : BaseIntegraEntity
    {
        public int IdPreguntaEncuestaCategoria { get; set; }
        public int IdPreguntaEncuestaTipo { get; set; }
        public string? Pregunta { get; set; }
        public bool? ActivarDescripcion { get; set; }
        public string? Descripcion { get; set; } = null!;
        public bool? PreguntaObligatoria { get; set; }
        public bool? PreguntaActiva { get; set; }

    }
}
