using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Operacion
{
    public class CentroCostoCertificadoDTO
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public int? IdCertificadoBrochure { get; set; }

        public int? IdCertificadoPartnerComplemento { get; set; }
    }
   
}
