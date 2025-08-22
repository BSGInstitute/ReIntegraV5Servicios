using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPreguntum
    {
        public TPreguntum()
        {
            TExamenRealizadoRespuestaEvaluadors = new HashSet<TExamenRealizadoRespuestaEvaluador>();
            TPreguntaEvaluacionTrabajos = new HashSet<TPreguntaEvaluacionTrabajo>();
        }

        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk de la tabla T_TipoRespuesta
        /// </summary>
        public int? IdTipoRespuesta { get; set; }
        /// <summary>
        /// Fk tabla T_PreguntaEscalaValor
        /// </summary>
        public int? IdPreguntaEscalaValor { get; set; }
        /// <summary>
        /// Enunciado de la pregunta
        /// </summary>
        public string? EnunciadoPregunta { get; set; }
        /// <summary>
        /// Comparacion de valor de la pregunta
        /// </summary>
        public bool? ConparacionValor { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Define si la pregunta requiere un tiempo limite
        /// </summary>
        public bool? RequiereTiempo { get; set; }
        /// <summary>
        /// Tiempo limite de pregunta
        /// </summary>
        public int? MinutosPorPregunta { get; set; }
        /// <summary>
        /// Define si respuestas de pregunta se muestra de forma aleatoria
        /// </summary>
        public bool? RespuestaAleatoria { get; set; }
        /// <summary>
        /// Muestra feedback de respuesta correcta
        /// </summary>
        public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
        /// <summary>
        /// Muestra feedback de respuesta Incorrecta
        /// </summary>
        public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
        /// <summary>
        /// Muestra feedback de manera inmediata
        /// </summary>
        public bool? MostrarFeedbackInmediato { get; set; }
        /// <summary>
        /// Muestra Feedback al finalizar la pregunta
        /// </summary>
        public bool? MostrarFeedbackPorPregunta { get; set; }
        /// <summary>
        /// Es Foreing Key de T_PreguntaIntento
        /// </summary>
        public int? IdPreguntaIntento { get; set; }
        /// <summary>
        /// FK T_PreguntaTipo
        /// </summary>
        public int? IdPreguntaTipo { get; set; }
        /// <summary>
        /// Fk T_TipoRespuestaCalificacion
        /// </summary>
        public int? IdTipoRespuestaCalificacion { get; set; }
        /// <summary>
        /// Factor que se usara para el calculo de puntaje de las respuestas
        /// </summary>
        public int? FactorRespuesta { get; set; }
        /// <summary>
        /// Fk T_PreguntaCategoria
        /// </summary>
        public int? IdPreguntaCategoria { get; set; }
        /// <summary>
        /// Flag de descripcion de pregunta
        /// </summary>
        public bool? ActivarDescripcion { get; set; }
        /// <summary>
        /// Descripcion de pregunta
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// flag de pregunta obligatoria
        /// </summary>
        public bool? PreguntaObligatoria { get; set; }
        /// <summary>
        /// flag de pregunta activa
        /// </summary>
        public bool? PreguntaActiva { get; set; }

        public virtual ICollection<TExamenRealizadoRespuestaEvaluador> TExamenRealizadoRespuestaEvaluadors { get; set; }
        public virtual ICollection<TPreguntaEvaluacionTrabajo> TPreguntaEvaluacionTrabajos { get; set; }
    }
}
