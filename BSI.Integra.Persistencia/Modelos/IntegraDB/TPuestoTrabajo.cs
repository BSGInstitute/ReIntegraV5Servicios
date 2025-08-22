using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPuestoTrabajo
    {
        public TPuestoTrabajo()
        {
            TDatoContratoPersonals = new HashSet<TDatoContratoPersonal>();
            TGrupoComparacionProcesoSeleccions = new HashSet<TGrupoComparacionProcesoSeleccion>();
            TModuloSistemaPuestoTrabajos = new HashSet<TModuloSistemaPuestoTrabajo>();
            TPerfilPuestoTrabajoPersonalAprobacions = new HashSet<TPerfilPuestoTrabajoPersonalAprobacion>();
            TPersonalPuestoSedeHistoricos = new HashSet<TPersonalPuestoSedeHistorico>();
            TPuestoTrabajoRelacionDetalleIdPuestoTrabajoDependenciaNavigations = new HashSet<TPuestoTrabajoRelacionDetalle>();
            TPuestoTrabajoRelacionDetalleIdPuestoTrabajoPuestoAcargoNavigations = new HashSet<TPuestoTrabajoRelacionDetalle>();
            TPuestoTrabajoRemuneracions = new HashSet<TPuestoTrabajoRemuneracion>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del puesto de trabajo
        /// </summary>
        public string Nombre { get; set; } = null!;
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
        /// Sistema Automatico Fecha de Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Relacion con el id de la tabla original
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// primary key de gp.T_PersonalAreaTrabajo
        /// </summary>
        public int? IdPersonalAreaTrabajo { get; set; }

        public virtual TPersonalAreaTrabajo? IdPersonalAreaTrabajoNavigation { get; set; }
        public virtual ICollection<TDatoContratoPersonal> TDatoContratoPersonals { get; set; }
        public virtual ICollection<TGrupoComparacionProcesoSeleccion> TGrupoComparacionProcesoSeleccions { get; set; }
        public virtual ICollection<TModuloSistemaPuestoTrabajo> TModuloSistemaPuestoTrabajos { get; set; }
        public virtual ICollection<TPerfilPuestoTrabajoPersonalAprobacion> TPerfilPuestoTrabajoPersonalAprobacions { get; set; }
        public virtual ICollection<TPersonalPuestoSedeHistorico> TPersonalPuestoSedeHistoricos { get; set; }
        public virtual ICollection<TPuestoTrabajoRelacionDetalle> TPuestoTrabajoRelacionDetalleIdPuestoTrabajoDependenciaNavigations { get; set; }
        public virtual ICollection<TPuestoTrabajoRelacionDetalle> TPuestoTrabajoRelacionDetalleIdPuestoTrabajoPuestoAcargoNavigations { get; set; }
        public virtual ICollection<TPuestoTrabajoRemuneracion> TPuestoTrabajoRemuneracions { get; set; }
    }
}
