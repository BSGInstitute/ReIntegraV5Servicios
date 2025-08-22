using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSeguimientoAlumnoDetalle
    {
        /// <summary>
        /// Pk de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// id de tabla fin.T_EstadoMatricula
        /// </summary>
        public int IdEstadoMatricula { get; set; }
        /// <summary>
        /// id de tabla fin.T_SubEstadoMatricula
        /// </summary>
        public int IdSubEstadoMatricula { get; set; }
        /// <summary>
        /// Id de tabla ope.T_SeguimientoAlumnoCategoria
        /// </summary>
        public int IdSeguimientoAlumnoCategoria { get; set; }
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
        /// Id de migracion (Si es que es migracion)
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
