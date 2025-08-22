using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena las solicitudes hechas.
    /// </summary>
    public partial class TSolicitudTi
    {
        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fecha de la solicitud
        /// </summary>
        public DateTime? FechaSolicitud { get; set; }
        /// <summary>
        /// Estado de la solicitud
        /// </summary>
        public string Estado { get; set; } = null!;
        /// <summary>
        /// Identificador de la sub-categoria
        /// </summary>
        public int? IdSolicitudTisubCategoria { get; set; }
        /// <summary>
        /// Identificador de la sede del usuario
        /// </summary>
        public int? IdSedeTrabajo { get; set; }
        /// <summary>
        /// Usuario que hizo la solicitud
        /// </summary>
        public string? UsuarioSolicitud { get; set; }
        /// <summary>
        /// Nivel de severidad
        /// </summary>
        public string? Severidad { get; set; }
        /// <summary>
        /// Descripcion del problema
        /// </summary>
        public string Problema { get; set; } = null!;
        /// <summary>
        /// Descripcion de la solucion
        /// </summary>
        public string? Solucion { get; set; }
        /// <summary>
        /// Tipo de solicitud
        /// </summary>
        public string? Tipo { get; set; }
        /// <summary>
        /// Identificador del usuario de integra que hizo la solicitud
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Identificador de la categoria a la que pertenece
        /// </summary>
        public int? IdSolicitudTicategoria { get; set; }
        /// <summary>
        /// Usuario   asignado
        /// </summary>
        public string? TecnicoAsignado { get; set; }
        /// <summary>
        /// Usuario asignador
        /// </summary>
        public string? UsuarioAsignador { get; set; }
        /// <summary>
        /// Nro de asignacion grupal
        /// </summary>
        public int? AsignacionGrupal { get; set; }
        /// <summary>
        /// Fecha estimada de asignacion
        /// </summary>
        public DateTime? FechaEstimadaAsignacion { get; set; }
        /// <summary>
        /// Fecga real de asignacion
        /// </summary>
        public DateTime? FechaRealAsignacion { get; set; }
        /// <summary>
        /// Fecha estimada de inicio de atencion
        /// </summary>
        public DateTime? FechaEstimadaInicioAtencion { get; set; }
        /// <summary>
        /// Fecha real de atencion
        /// </summary>
        public DateTime? FechaRealInicioAtencion { get; set; }
        /// <summary>
        /// Fecha estimada de fin de atencion
        /// </summary>
        public DateTime? FechaEstimadaFinAtencion { get; set; }
        /// <summary>
        /// Fecha real de fin de atencion
        /// </summary>
        public DateTime? FechaRealFinAtencion { get; set; }
        /// <summary>
        /// Nivel de satisfaccion de la atencion
        /// </summary>
        public int? NivelSatisfaccion { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modifico por ultima vez el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha que se modifico por ultima vez el registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        public string? RutaArchivo { get; set; }
        public string? NombreArchivo { get; set; }
        public string? ContentTypeArchivo { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }

        public virtual TPersonal? IdPersonalNavigation { get; set; }
        public virtual TSedeTrabajo? IdSedeTrabajoNavigation { get; set; }
        public virtual TSolicitudTicategorium? IdSolicitudTicategoriaNavigation { get; set; }
        public virtual TSolicitudTisubCategorium? IdSolicitudTisubCategoriaNavigation { get; set; }
    }
}
