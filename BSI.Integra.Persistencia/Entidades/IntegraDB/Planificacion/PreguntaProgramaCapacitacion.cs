using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PreguntaProgramaCapacitacion : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public int? OrdenFilaSesion { get; set; }
        public int? IdTipoRespuesta { get; set; }
        public int? IdPreguntaEscalaValor { get; set; }
        public string? EnunciadoPregunta { get; set; }
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
        public string? GrupoPregunta { get; set; }
        public int? IdTipoMarcador { get; set; }
        public decimal? ValorMarcador { get; set; }
        public int? OrdenPreguntaGrupo { get; set; }
        public int? IdPespecifico { get; set; }
        public List<RespuestaPreguntaProgramaCapacitacion> RespuestaPreguntaProgramaCapacitacions { get; set; }
    }
}
