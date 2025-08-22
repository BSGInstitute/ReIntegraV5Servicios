using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PreguntaRegistradaDTO
    {
    }
    public class PreguntaProgramaCapacitacionRegistradaDTO
    {
        public int Id { get; set; }
        public string Enunciado { get; set; }
        public int IdTipoRespuesta { get; set; }
        public int? IdPreguntaTipo { get; set; }
        public int? MinutosPorPregunta { get; set; }
        public bool? RespuestaAleatoria { get; set; }
        public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
        public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
        public bool? MostrarFeedbackInmediato { get; set; }
        public bool? MostrarFeedbackPorPregunta { get; set; }
        public int? NumeroMaximoIntento { get; set; }
        public bool? ActivarFeedbackMaximoIntento { get; set; }
        public string MensajeFeedbackIntento { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdPEspecifico { get; set; }
        public string PGeneral { get; set; }
        public int? IdCapitulo { get; set; }
        public int? IdSesion { get; set; }
        public int? IdTipoRespuestaCalificacion { get; set; }
        public int? FactorRespuesta { get; set; }
        public string GrupoPregunta { get; set; }
        public int? IdTipoMarcador { get; set; }
        public decimal? ValorMarcador { get; set; }
        public int? OrdenPreguntaGrupo { get; set; }
        public int? IdPreguntaIntento { get; set; }
    }
}
