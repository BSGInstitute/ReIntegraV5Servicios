using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public partial class PgeneralConfiguracionPlantilla : BaseIntegraEntity
    {
        public int IdPlantillaFrontal { get; set; }
        public int? IdPlantillaPosterior { get; set; }
        public int IdPgeneral { get; set; }
        public DateTime? UltimaFechaRemplazarCertificado { get; set; }
        public List<PGeneralConfiguracionPlantillaDetalle> PGeneralConfiguracionPlantillaDetalle { get; set; }
    }
}



