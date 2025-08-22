using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPreguntaProgramaCapacitacion
    {
        public TPreguntaProgramaCapacitacion()
        {
            TRespuestaPreguntaProgramaCapacitacions = new HashSet<TRespuestaPreguntaProgramaCapacitacion>();
        }

        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK T_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Orden fila en la que se encuentra el capitulo
        /// </summary>
        public int? OrdenFilaCapitulo { get; set; }
        /// <summary>
        /// Orden de fila en la que se encuentra la sesion
        /// </summary>
        public int? OrdenFilaSesion { get; set; }
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
        /// Representa el grupo al que pertenece la pregunta
        /// </summary>
        public string? GrupoPregunta { get; set; }
        /// <summary>
        /// Fk T_TipoMarcador
        /// </summary>
        public int? IdTipoMarcador { get; set; }
        /// <summary>
        /// Valor del marcador seleccionado
        /// </summary>
        public decimal? ValorMarcador { get; set; }
        /// <summary>
        /// Orden de la pregunta dentro del grupo establecido
        /// </summary>
        public int? OrdenPreguntaGrupo { get; set; }
        /// <summary>
        /// FK T_PEspecifico
        /// </summary>
        public int? IdPespecifico { get; set; }

        public virtual TPespecifico? IdPespecificoNavigation { get; set; }
        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
        public virtual TPreguntaEscalaValor? IdPreguntaEscalaValorNavigation { get; set; }
        public virtual TPreguntaIntento? IdPreguntaIntentoNavigation { get; set; }
        public virtual TPreguntaTipo? IdPreguntaTipoNavigation { get; set; }
        public virtual TTipoMarcador? IdTipoMarcadorNavigation { get; set; }
        public virtual TTipoRespuestaCalificacion? IdTipoRespuestaCalificacionNavigation { get; set; }
        public virtual TTipoRespuestum? IdTipoRespuestaNavigation { get; set; }
        public virtual ICollection<TRespuestaPreguntaProgramaCapacitacion> TRespuestaPreguntaProgramaCapacitacions { get; set; }
    }
}
