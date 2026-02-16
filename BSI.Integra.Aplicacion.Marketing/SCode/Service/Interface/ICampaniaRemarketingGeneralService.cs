using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using System.Threading.Tasks;
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
        RendimientoCampaniaDTO ObtenerRendimientoListadoCampanias(List<int> ids);
        CombosConfiguracionCampaniaDTO ObtenerCombosConfiguracionCampania();
        List<SegmentoCreadoDTO> ObtenerListadoSegmentosCreados();
        Task<EstadoEjecucionLlamadaIA> ObtenerResultadosGeneracionTextoPorCampania(string idLlamadaIA);
        Task<bool> ActualizarEjecutarEnvioCampaniaRemarketing(ConfiguracionCampaniaRemarketingDTO request, string usuario);
        DetallesCampaniaDTO VerDetallesCampania(int idCampania);
        CampaniaRemarketingIndividualDTO ObtenerCampaniaRemarketingPorId(int id);
        bool EliminarCampania(int id, string usuario);
        Task<List<MensajeGeneradoIA>> ObtenerMensajeGeneradoPorId(string identificadorLlamadaIA, int idAlumno);
        Task<bool> ReenviarMensajeGenerado(ReenviarMensajeRequest request);
        Task<bool> GenerarListadoTextosRemarketing(ConfiguracionCampaniaRemarketingDTO request, string usuario);
        Task<RespuestaIdentificadorLlamadaIA> GenerarMensajesIAPorListaAlumnos(string canal, string tipoMensaje, string logicaEnvio, string categoriaArgumento, List<int> idsAlumno, List<int> versionesArgumento);
        Task<List<MensajeGeneradoIA>> ObtenerMensajesGeneradosPorIdLlamadaIA(string idLlamadaIA, bool argumentos);
        Task<EstadoEjecucionLlamadaIA> ObtenerEstadoEjecucionLlamada(string idLlamadaIA);

        // Métodos para el envío masivo de correos
        Task<ResultadoEnvioMasivoDTO> EjecutarEnvioCampaniaRemarketing(ConfiguracionCampaniaRemarketingDTO request, string usuario, bool argumentos);
        Task EjecutarCampaniasProgramadas();

        // Canvas
        bool InsertarCampaniaCanvas(CampaniaCanvasDTO request, string usuario);
        bool ActualizarCampaniaCanvas(CampaniaCanvasDTO request, string usuario);
        CampaniaCanvasDTO ObtenerCampaniaCanvas(int idRemarketingCampaniaGeneral);
        bool EliminarCampaniaCanvas(int idRemarketingCampaniaGeneral, string usuario);
    }
}
