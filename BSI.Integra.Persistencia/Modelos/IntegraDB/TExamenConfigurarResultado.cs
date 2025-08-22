using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TExamenConfigurarResultado
    {
        /// <summary>
        /// Clave primario del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Verdadero si el examen va a ser calificable de lo contrario falso
        /// </summary>
        public bool ExamenCalificado { get; set; }
        /// <summary>
        /// Puntaje total de Examen
        /// </summary>
        public decimal? PuntajeExamen { get; set; }
        /// <summary>
        /// Puntaje minimo de Aprobacion
        /// </summary>
        public decimal? PuntajeAprobacion { get; set; }
        /// <summary>
        /// Mostrar Resultado de Examen
        /// </summary>
        public bool MostrarResultado { get; set; }
        /// <summary>
        /// Mostrar puntaje Total
        /// </summary>
        public bool MostrarPuntajeTotal { get; set; }
        /// <summary>
        /// Mostrar Puntaje por Pregunta
        /// </summary>
        public bool MostrarPuntajePregunta { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de la tabla
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
