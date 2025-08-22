using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPespecificoParticipacionExpositor
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla de T_PEspecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Orden del curso dentro de la capacitacion
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Grupo
        /// </summary>
        public int? Grupo { get; set; }
        /// <summary>
        /// Llave foranea con la tabla de T_Expositor, docente asignado a el curso
        /// </summary>
        public int? IdExpositorCurso { get; set; }
        /// <summary>
        /// Nombre del expositor asignado al curso
        /// </summary>
        public string? ExpositorCurso { get; set; }
        /// <summary>
        /// Llave foranea con la tabla de T_Expositor, docente asignado al grupo
        /// </summary>
        public int? IdExpositorGrupo { get; set; }
        /// <summary>
        /// Nombre del Expositor asignado al Grupo
        /// </summary>
        public string? ExpositorGrupo { get; set; }
        /// <summary>
        /// Llave foranea con la tabla de T_Expositor, docente asignado a el curso y grupo en V3
        /// </summary>
        public int? IdExpositorV3 { get; set; }
        /// <summary>
        /// Nombre del docente asignado al curso y grupo en v3
        /// </summary>
        public string? ExpositorV3 { get; set; }
        /// <summary>
        /// Llave foranea con la tabla de T_Expositor, docente confirmado por operaciones
        /// </summary>
        public int? IdExpositorGrupoConfirmado { get; set; }
        /// <summary>
        /// Llave foranea con la tabla de T_Proveedor, proveedor asignado al fur de honrario
        /// </summary>
        public int? IdProveedorFurHonorario { get; set; }
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
        /// <summary>
        /// Llave foranea con la tabla de T_Proveedor, proveedor asignado al grupo por planificacion
        /// </summary>
        public int? IdProveedorPlanificacionGrupo { get; set; }
        /// <summary>
        /// Llave foranea con la tabla de T_Proveedor, proveedor asignado al grupo por operaciones
        /// </summary>
        public int? IdProveedorOperacionesGrupoConfirmado { get; set; }
        public bool? EsSilaboAprobado { get; set; }
    }
}
