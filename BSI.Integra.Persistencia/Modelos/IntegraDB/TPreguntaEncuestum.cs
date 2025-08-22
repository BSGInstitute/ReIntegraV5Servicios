using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena las preguntas para las encuestas online
    /// </summary>
    public partial class TPreguntaEncuestum
    {
        /// <summary>
        /// Pk de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de tabla T_PreguntaEncuestaCategoria
        /// </summary>
        public int IdPreguntaEncuestaCategoria { get; set; }
        /// <summary>
        /// Id de tabla T_PreguntaEncuestaTipo
        /// </summary>
        public int IdPreguntaEncuestaTipo { get; set; }
        /// <summary>
        /// Definicion de pregunta
        /// </summary>
        public string? Pregunta { get; set; }
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
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion de registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion de registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion de registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// FEcha modificacion de registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// row version de registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
