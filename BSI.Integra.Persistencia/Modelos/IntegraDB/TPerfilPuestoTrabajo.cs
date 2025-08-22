using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPerfilPuestoTrabajo
    {
        public TPerfilPuestoTrabajo()
        {
            TPuestoTrabajoCaracteristicaPersonals = new HashSet<TPuestoTrabajoCaracteristicaPersonal>();
            TPuestoTrabajoCursoComplementarios = new HashSet<TPuestoTrabajoCursoComplementario>();
            TPuestoTrabajoExperiencia = new HashSet<TPuestoTrabajoExperiencium>();
            TPuestoTrabajoFormacionAcademicas = new HashSet<TPuestoTrabajoFormacionAcademica>();
            TPuestoTrabajoFuncions = new HashSet<TPuestoTrabajoFuncion>();
            TPuestoTrabajoPuntajeCalificacions = new HashSet<TPuestoTrabajoPuntajeCalificacion>();
            TPuestoTrabajoRelacions = new HashSet<TPuestoTrabajoRelacion>();
            TPuestoTrabajoReportes = new HashSet<TPuestoTrabajoReporte>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK T_PuestoTrabajo
        /// </summary>
        public int IdPuestoTrabajo { get; set; }
        /// <summary>
        /// descripcion
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Objetivo
        /// </summary>
        public string Objetivo { get; set; } = null!;
        /// <summary>
        /// Nro de version del registro
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Determina si el registro es actual
        /// </summary>
        public bool? EsActual { get; set; }
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
        /// FK T_Personal - Personal que realiza la solicitud de version
        /// </summary>
        public int? IdPersonalSolicitud { get; set; }
        /// <summary>
        /// Fecha en la que se realiza la solicitud
        /// </summary>
        public DateTime? FechaSolicitud { get; set; }
        /// <summary>
        /// FK T_Personal - Personal que aprueba la solicitud de version
        /// </summary>
        public int? IdPersonalAprobacion { get; set; }
        /// <summary>
        /// Fecha en la que se aprueba la solicitud
        /// </summary>
        public DateTime? FechaAprobacion { get; set; }
        /// <summary>
        /// Observaciones encontradas en la version generada
        /// </summary>
        public string? Observacion { get; set; }
        /// <summary>
        /// Fk T_PerfilPuestoTrabajoEstadoSolicitud
        /// </summary>
        public int? IdPerfilPuestoTrabajoEstadoSolicitud { get; set; }

        public virtual TPerfilPuestoTrabajoEstadoSolicitud? IdPerfilPuestoTrabajoEstadoSolicitudNavigation { get; set; }
        public virtual ICollection<TPuestoTrabajoCaracteristicaPersonal> TPuestoTrabajoCaracteristicaPersonals { get; set; }
        public virtual ICollection<TPuestoTrabajoCursoComplementario> TPuestoTrabajoCursoComplementarios { get; set; }
        public virtual ICollection<TPuestoTrabajoExperiencium> TPuestoTrabajoExperiencia { get; set; }
        public virtual ICollection<TPuestoTrabajoFormacionAcademica> TPuestoTrabajoFormacionAcademicas { get; set; }
        public virtual ICollection<TPuestoTrabajoFuncion> TPuestoTrabajoFuncions { get; set; }
        public virtual ICollection<TPuestoTrabajoPuntajeCalificacion> TPuestoTrabajoPuntajeCalificacions { get; set; }
        public virtual ICollection<TPuestoTrabajoRelacion> TPuestoTrabajoRelacions { get; set; }
        public virtual ICollection<TPuestoTrabajoReporte> TPuestoTrabajoReportes { get; set; }
    }
}
