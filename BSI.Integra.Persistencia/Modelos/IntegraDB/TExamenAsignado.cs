using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TExamenAsignado
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_ProcesoSeleccion
        /// </summary>
        public int IdProcesoSeleccion { get; set; }
        /// <summary>
        /// Fk T_Examen
        /// </summary>
        public int IdExamen { get; set; }
        /// <summary>
        /// Fk T_Postulante
        /// </summary>
        public int IdPostulante { get; set; }
        /// <summary>
        /// Indica el estado del examen
        /// </summary>
        public bool? EstadoExamen { get; set; }
        /// <summary>
        /// Fecha Inicio Examen
        /// </summary>
        public DateTime? FechaInicio { get; set; }
        /// <summary>
        /// Fecha Fin Examen
        /// </summary>
        public DateTime? FechaFin { get; set; }
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
        /// Estado de acceso
        /// </summary>
        public bool? EstadoAcceso { get; set; }
        /// <summary>
        /// Indica la version Centil del Examen (la maxima version vigente en la fecha del registro)
        /// </summary>
        public int? VersionCentil { get; set; }
    }
}
