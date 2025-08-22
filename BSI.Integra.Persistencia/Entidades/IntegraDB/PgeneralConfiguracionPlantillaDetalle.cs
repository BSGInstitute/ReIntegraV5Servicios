using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public partial class PGeneralConfiguracionPlantillaDetalle : BaseIntegraEntity
    {
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        public int IdModalidadCurso { get; set; }
        public int? IdOperadorComparacion { get; set; }
        public decimal? NotaAprobatoria { get; set; }
        public bool DeudaPendiente { get; set; }
        public List<PgeneralConfiguracionPlantillaEstadoMatricula> PgeneralConfiguracionPlantillaEstadoMatriculas { get; set; }
        public List<PgeneralConfiguracionPlantillaSubEstadoMatricula> PgeneralConfiguracionPlantillaSubEstadoMatriculas { get; set; }
    }
}


