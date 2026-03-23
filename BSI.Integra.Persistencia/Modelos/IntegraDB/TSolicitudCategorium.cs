using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla agrupa las solicitudes por categoria de producto
    /// </summary>
    public partial class TSolicitudCategorium
    {
        public TSolicitudCategorium()
        {
            TSolicitudProblemas = new HashSet<TSolicitudProblema>();
            TSolicitudSubCategoria = new HashSet<TSolicitudSubCategorium>();
        }

        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la solicitud
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Llave foranea de T_SolicitudTipoReporte
        /// </summary>
        public int? IdSolicitudTipoReporte { get; set; }
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
        /// <summary>
        /// Descripcion de la categoria
        /// </summary>
        public string? Descripcion { get; set; }

        public virtual TSolicitudTipoReporte? IdSolicitudTipoReporteNavigation { get; set; }
        public virtual ICollection<TSolicitudProblema> TSolicitudProblemas { get; set; }
        public virtual ICollection<TSolicitudSubCategorium> TSolicitudSubCategoria { get; set; }
    }
}
