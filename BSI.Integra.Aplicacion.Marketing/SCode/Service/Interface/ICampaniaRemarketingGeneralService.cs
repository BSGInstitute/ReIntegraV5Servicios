using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    public interface ICampaniaRemarketingGeneralService
    {
        List<CampaniaRemarketingGeneralDTO> ObtenerListadoCampania();
        List<object> ObtenerRendimientoListadoCampanias(List<int> ids);
        CombosConfiguracionCampaniaDTO ObtenerCombosConfiguracionCampania();
        DetallesCampaniaDTO VerDetallesCampania(int id);
        bool EditarCampania();
        bool EliminarCampania(int id);
        MensajeGeneradoDTO ObtenerMensajeGeneradoPorId(int id);
        bool ReenviarMensajeGenerado(int id);
    }
}
