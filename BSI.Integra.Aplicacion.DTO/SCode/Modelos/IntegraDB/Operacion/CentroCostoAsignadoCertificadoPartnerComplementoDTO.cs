using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Operacion
{
    public class CentroCostoAsignadoCertificadoPartnerComplementoDTO
    {
        public int? IdCertificadoPartnerComplemento { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
    }

    public class CentroCostoAsignadoCertificadoBrochureDTO
    {
        public int? IdCertificadoBrochure { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
    }
}
