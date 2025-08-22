using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TRespuestaPreguntaProgramaCapacitacion
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_Pregunta
        /// </summary>
        public int? IdPreguntaProgramaCapacitacion { get; set; }
        /// <summary>
        /// Respuesta correcta
        /// </summary>
        public bool? RespuestaCorrecta { get; set; }
        /// <summary>
        /// Nro de orden
        /// </summary>
        public int NroOrden { get; set; }
        /// <summary>
        /// Enunciado respuesta
        /// </summary>
        public string? EnunciadoRespuesta { get; set; }
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
        /// Define la respuesta de las preguntas de ordenamiento
        /// </summary>
        public int? NroOrdenRespuesta { get; set; }
        /// <summary>
        /// Define puntaje por respuesta
        /// </summary>
        public int? Puntaje { get; set; }
        /// <summary>
        /// Define Feedback por respuesta asertada
        /// </summary>
        public string? FeedbackPositivo { get; set; }
        /// <summary>
        /// Define Feedback por respuesta erronea
        /// </summary>
        public string? FeedbackNegativo { get; set; }
        /// <summary>
        /// Define si se muestra el feedback o no se muestra.
        /// </summary>
        public bool? MostrarFeedBack { get; set; }
        /// <summary>
        /// Puntaje que se autocalcula en base al tipo de respuesta calificacion escogida en la pregunta
        /// </summary>
        public int? PuntajeTipoRespuesta { get; set; }

        public virtual TPreguntaProgramaCapacitacion? IdPreguntaProgramaCapacitacionNavigation { get; set; }
    }
}
