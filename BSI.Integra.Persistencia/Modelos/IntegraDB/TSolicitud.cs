using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSolicitud
    {
        public TSolicitud()
        {
            TSolicitudAlumnos = new HashSet<TSolicitudAlumno>();
            TSolicitudInternas = new HashSet<TSolicitudInterna>();
        }

        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la solicitud
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Prioridad de la solicitud
        /// </summary>
        public string Prioridad { get; set; } = null!;
        /// <summary>
        /// Llave foranea de T_SolicitudSubCategoria
        /// </summary>
        public int IdSolicitudSubCategoria { get; set; }
        /// <summary>
        /// Llave foranea 1 de T_Personal
        /// </summary>
        public int IdPersonalRevision { get; set; }
        /// <summary>
        /// Llave foranea 2 de T_Personal
        /// </summary>
        public int IdPersonalSolucion { get; set; }
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

        public virtual TPersonal IdPersonalRevisionNavigation { get; set; } = null!;
        public virtual TPersonal IdPersonalSolucionNavigation { get; set; } = null!;
        public virtual TSolicitudSubCategorium IdSolicitudSubCategoriaNavigation { get; set; } = null!;
        public virtual ICollection<TSolicitudAlumno> TSolicitudAlumnos { get; set; }
        public virtual ICollection<TSolicitudInterna> TSolicitudInternas { get; set; }
    }
}
