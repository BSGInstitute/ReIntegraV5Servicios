using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ModalidadCurso : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(20)]
        public string Codigo { get; set; } = null!;
        public ICollection<CriterioEvaluacionModalidadCurso> CriterioEvaluacionModalidadCursos { get; set; }
        public ICollection<EsquemaEvaluacionPgeneralModalidad> EsquemaEvaluacionPgeneralModalidads { get; set; }
        public ICollection<PgeneralCodigoPartnerModalidadCurso> PgeneralCodigoPartnerModalidadCursos { get; set; }
        public ICollection<PGeneralConfiguracionPlantillaDetalle> PgeneralConfiguracionPlantillaDetalles { get; set; }
        public ICollection<PgeneralModalidad> PgeneralModalidads { get; set; }
    }
}
