using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Operacion
{
    public class CentroCostoCertificado : BaseIntegraEntity
    {


        public int IdCentroCosto { get; set; }

        public int? IdCertificadoBrochure { get; set; }

        public int? IdCertificadoPartnerComplemento { get; set; }
      
    }

}
    