using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface ICampaniaRemarketingGeneralRepository
    {
        List<CampaniaRemarketingGeneralDTO> ObtenerListadoCampania();
        List<object> ObtenerRendimientoListadoCampanias(List<int> ids);
        List<SegmentoCreadoDTO> ObtenerListadoSegmentosCreados();
        List<ResultadoTextoGeneradoDTO> ObtenerResultadosGeneracionTextoPorCampania(int id);
        bool InsertarCampaniaRemarketing(EnvioCampaniaRemarketingDTO request);
        bool ActualizarCampaniaRemarketing(EnvioCampaniaRemarketingDTO request);
        DetallesCampaniaDTO VerDetallesCampania(int id);
        CampaniaRemarketingIndividualDTO ObtenerCampaniaRemarketingPorId(int id);
        bool EliminarCampania(int id, string usuario);
        MensajeGeneradoDTO ObtenerMensajeGeneradoPorId(int id);

        // Métodos individuales para cada lista de CombosConfiguracionCampaniaDTO
        List<ElementoConfiguracionCampania> ObtenerMediosEnvio();
        List<ElementoConfiguracionCampania> ObtenerTiposMensaje();
        List<ElementoConfiguracionCampania> ObtenerLogicasEnvio();
        List<ElementoConfiguracionCampania> ObtenerArgumentos();
    }
}
