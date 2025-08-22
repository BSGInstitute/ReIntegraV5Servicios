using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProcesoSeleccion
    {
        public TProcesoSeleccion()
        {
            TConvocatoriaPersonals = new HashSet<TConvocatoriaPersonal>();
            TExamenAsignadoEvaluadors = new HashSet<TExamenAsignadoEvaluador>();
        }

        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del proceso de selección
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// FK T_PuestoTrabajo
        /// </summary>
        public int IdPuestoTrabajo { get; set; }
        /// <summary>
        /// Codigo del proceso de selección
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Url del proceso de selección
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// Estado del proceso de selecciomn
        /// </summary>
        public bool? Activo { get; set; }
        /// <summary>
        /// Fk T_Sede
        /// </summary>
        public int? IdSede { get; set; }
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
        /// Fecha de Inicio del proceso
        /// </summary>
        public DateTime? FechaInicioProceso { get; set; }
        /// <summary>
        /// Fecha de Fin del proceso
        /// </summary>
        public DateTime? FechaFinProceso { get; set; }

        public virtual ICollection<TConvocatoriaPersonal> TConvocatoriaPersonals { get; set; }
        public virtual ICollection<TExamenAsignadoEvaluador> TExamenAsignadoEvaluadors { get; set; }
    }
}
