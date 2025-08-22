using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPuestoTrabajoRelacionDetalle
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Primary key de gp.T_PerfilPuestoTrabajo
        /// </summary>
        public int IdPuestoTrabajoRelacion { get; set; }
        /// <summary>
        /// FK T_PuestoTrabajo - Multiselect
        /// </summary>
        public int? IdPuestoTrabajoDependencia { get; set; }
        /// <summary>
        /// FK T_PuestoTrabajo - Multiselect
        /// </summary>
        public int? IdPuestoTrabajoPuestoAcargo { get; set; }
        /// <summary>
        /// FK T_PersonalAreaTrabajo - Multiselect
        /// </summary>
        public int? IdPersonalAreaTrabajo { get; set; }
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

        public virtual TPersonalAreaTrabajo? IdPersonalAreaTrabajoNavigation { get; set; }
        public virtual TPuestoTrabajo? IdPuestoTrabajoDependenciaNavigation { get; set; }
        public virtual TPuestoTrabajo? IdPuestoTrabajoPuestoAcargoNavigation { get; set; }
        public virtual TPuestoTrabajoRelacion IdPuestoTrabajoRelacionNavigation { get; set; } = null!;
    }
}
