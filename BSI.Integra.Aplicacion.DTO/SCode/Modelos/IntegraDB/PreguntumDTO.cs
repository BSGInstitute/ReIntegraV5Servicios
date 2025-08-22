using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class PreguntumDTO
    {
        public int Id { get; set; }
        public int? IdTipoRespuesta { get; set; }
        public int? IdPreguntaEscalaValor { get; set; }
        public string? EnunciadoPregunta { get; set; }
        public bool? ConparacionValor { get; set; }
        public bool? RequiereTiempo { get; set; }
        public int? MinutosPorPregunta { get; set; }
        public bool? RespuestaAleatoria { get; set; }
        public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
        public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
        public bool? MostrarFeedbackInmediato { get; set; }
        public bool? MostrarFeedbackPorPregunta { get; set; }
        public int? IdPreguntaIntento { get; set; }
        public int? IdPreguntaTipo { get; set; }
        public int? IdTipoRespuestaCalificacion { get; set; }
        public int? FactorRespuesta { get; set; }
        public int? IdPreguntaCategoria { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }

    public class PreguntumEntradaDTO
    {
        public int? Id { get; set; }
        public int? IdTipoRespuesta { get; set; }
        public int? IdPreguntaEscalaValor { get; set; }
        public string? EnunciadoPregunta { get; set; }
        public bool? ConparacionValor { get; set; }
        public bool? RequiereTiempo { get; set; }
        public int? MinutosPorPregunta { get; set; }
        public bool? RespuestaAleatoria { get; set; }
        public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
        public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
        public bool? MostrarFeedbackInmediato { get; set; }
        public bool? MostrarFeedbackPorPregunta { get; set; }
        public int? IdPreguntaIntento { get; set; }
        public int? IdPreguntaTipo { get; set; }
        public int? IdTipoRespuestaCalificacion { get; set; }
        public int? FactorRespuesta { get; set; }
        public int? IdPreguntaCategoria { get; set; }
        public string Usuario { get; set; }
        public bool? ActivarDescripcion { get; set; }
        public string? Descripcion { get; set; } = null!;
        public bool? PreguntaObligatoria { get; set; }
        public bool? PreguntaActiva { get; set; }
    }

    public class BancoPreguntumDTO
    {
        public int IdPregunta { get; set; }
        public int IdTipoRespuesta { get; set; }
        public int IdPreguntaTipo { get; set; }
        public string EnunciadoPregunta { get; set; }
        public string NombreTipoRespuesta { get; set; }
        public string NombrePreguntaTipo { get; set; }
        public bool? ActivarDescripcion { get; set; }
        public string? Descripcion { get; set; } = null!;
        public bool? PreguntaObligatoria { get; set; }
        public bool? PreguntaActiva { get; set; }
    }

}

