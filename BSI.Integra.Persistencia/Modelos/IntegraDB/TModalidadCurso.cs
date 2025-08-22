using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TModalidadCurso
    {
        public TModalidadCurso()
        {
            TCriterioEvaluacionModalidadCursos = new HashSet<TCriterioEvaluacionModalidadCurso>();
            TEncuestaOnlines = new HashSet<TEncuestaOnline>();
            TEsquemaEvaluacionPgeneralModalidads = new HashSet<TEsquemaEvaluacionPgeneralModalidad>();
            TPgeneralCodigoPartnerModalidadCursos = new HashSet<TPgeneralCodigoPartnerModalidadCurso>();
            TPgeneralConfiguracionPlantillaDetalles = new HashSet<TPgeneralConfiguracionPlantillaDetalle>();
            TPgeneralExpositors = new HashSet<TPgeneralExpositor>();
            TPgeneralModalidads = new HashSet<TPgeneralModalidad>();
        }

        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// nombre de la modalidad del curso
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Codigo de la Modalidad, se utiliza para la generacion del Codigo de Centro de Costo
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
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
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual ICollection<TCriterioEvaluacionModalidadCurso> TCriterioEvaluacionModalidadCursos { get; set; }
        public virtual ICollection<TEncuestaOnline> TEncuestaOnlines { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralModalidad> TEsquemaEvaluacionPgeneralModalidads { get; set; }
        public virtual ICollection<TPgeneralCodigoPartnerModalidadCurso> TPgeneralCodigoPartnerModalidadCursos { get; set; }
        public virtual ICollection<TPgeneralConfiguracionPlantillaDetalle> TPgeneralConfiguracionPlantillaDetalles { get; set; }
        public virtual ICollection<TPgeneralExpositor> TPgeneralExpositors { get; set; }
        public virtual ICollection<TPgeneralModalidad> TPgeneralModalidads { get; set; }
    }
}
