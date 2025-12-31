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
        List<SegmentoCreadoDTO> ObtenerListadoSegmentosCreados();
        List<ResultadoTextoGeneradoDTO> ObtenerResultadosGeneracionTextoPorCampania(int id);
        bool EjecutarEnvioCampaniaRemarketing(EnvioCampaniaRemarketingDTO request, string usuario);
        DetallesCampaniaDTO VerDetallesCampania(int id);
        CampaniaRemarketingIndividualDTO ObtenerCampaniaRemarketingPorId(int id);
        bool EditarCampania();
        bool EliminarCampania(int id, string usuario);
        MensajeGeneradoDTO ObtenerMensajeGeneradoPorId(int id);
        bool ReenviarMensajeGenerado(int id);
    }
}
