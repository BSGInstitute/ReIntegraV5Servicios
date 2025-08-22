using BSI.Integra.Aplicacion.Base; 

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public partial class PGeneralCodigoPartner : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public string Codigo { get; set; } = null!;
        public int? Pdu { get; set; }
        public List<PgeneralCodigoPartnerModalidadCurso> PgeneralCodigoPartnerModalidadCurso { get; set; }
        public List<PgeneralCodigoPartnerVersionPrograma> PgeneralCodigoPartnerVersionPrograma { get; set; }
    }
}
