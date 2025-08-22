using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TActividadCrepLog
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Importar/Exportar
        /// </summary>
        public string TipoOperacion { get; set; } = null!;
        /// <summary>
        /// Eliminar/Actualizar/Nuevo
        /// </summary>
        public string TipoActividad { get; set; } = null!;
        /// <summary>
        /// identificador de EstadoOperacion
        /// </summary>
        public int EstadoOperacion { get; set; }
        /// <summary>
        /// Excepciones Capaturadas para el estado Error
        /// </summary>
        public string? ExcepcionProceso { get; set; }
        /// <summary>
        /// CREP Exportado/Importado
        /// </summary>
        public string Crep { get; set; } = null!;
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
    }
}
