using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalAreaTrabajo
    {
        public TPersonalAreaTrabajo()
        {
            TConvocatoriaPersonals = new HashSet<TConvocatoriaPersonal>();
            TPuestoTrabajoRelacionDetalles = new HashSet<TPuestoTrabajoRelacionDetalle>();
            TPuestoTrabajos = new HashSet<TPuestoTrabajo>();
            TUrlContenedorPermisos = new HashSet<TUrlContenedorPermiso>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Codigo
        /// </summary>
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
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

        public virtual ICollection<TConvocatoriaPersonal> TConvocatoriaPersonals { get; set; }
        public virtual ICollection<TPuestoTrabajoRelacionDetalle> TPuestoTrabajoRelacionDetalles { get; set; }
        public virtual ICollection<TPuestoTrabajo> TPuestoTrabajos { get; set; }
        public virtual ICollection<TUrlContenedorPermiso> TUrlContenedorPermisos { get; set; }
    }
}
