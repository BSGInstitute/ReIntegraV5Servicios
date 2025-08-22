using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDocumentoPw
    {
        public TDocumentoPw()
        {
            TBandejaPendientePws = new HashSet<TBandejaPendientePw>();
            TConfigurarEvaluacionTrabajos = new HashSet<TConfigurarEvaluacionTrabajo>();
            TDocumentoSeccionPws = new HashSet<TDocumentoSeccionPw>();
            TPgeneralDocumentoPws = new HashSet<TPgeneralDocumentoPw>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del documento
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Es Foreing Key T_Plantilla_PW
        /// </summary>
        public int IdPlantillaPw { get; set; }
        /// <summary>
        /// El estado de la aprobacion segun el flujo
        /// </summary>
        public int EstadoFlujo { get; set; }
        /// <summary>
        /// Asignado o no asignado
        /// </summary>
        public bool Asignado { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
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
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TBandejaPendientePw> TBandejaPendientePws { get; set; }
        public virtual ICollection<TConfigurarEvaluacionTrabajo> TConfigurarEvaluacionTrabajos { get; set; }
        public virtual ICollection<TDocumentoSeccionPw> TDocumentoSeccionPws { get; set; }
        public virtual ICollection<TPgeneralDocumentoPw> TPgeneralDocumentoPws { get; set; }
    }
}
