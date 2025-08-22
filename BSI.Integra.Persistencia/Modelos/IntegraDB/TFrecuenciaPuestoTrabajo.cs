using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFrecuenciaPuestoTrabajo
    {
        public TFrecuenciaPuestoTrabajo()
        {
            TPuestoTrabajoFuncions = new HashSet<TPuestoTrabajoFuncion>();
            TPuestoTrabajoReportes = new HashSet<TPuestoTrabajoReporte>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la frecuencia
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla original en el sistema anterior
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual ICollection<TPuestoTrabajoFuncion> TPuestoTrabajoFuncions { get; set; }
        public virtual ICollection<TPuestoTrabajoReporte> TPuestoTrabajoReportes { get; set; }
    }
}
