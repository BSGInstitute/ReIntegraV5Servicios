using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IInformacionProgramaService
    {
        CargarInformacionProgramaAutomaticoRespuestaDTO CargarInformacionProgramaAutomatico(int idCentroCosto, int codigoPais, int idMatriculaCabecera, int idOportunidad);
        Task<CargarInformacionProgramaInversionRespuestaDTO> CargarInformacionProgramaInversionAsync(int idPGeneral, int codigoPais);
        Task<CargarInformacionProgramaPresentacionRespuestaDTO> CargarInformacionProgramaPresentacionAsync(int idPGeneral);
        Task<CargarInformacionProgramaPublicoObjetivoRespuestaDTO> CargarInformacionProgramaPublicoObjetivoAsync(int idPGeneral);
        CargarInformacionProgramaAutomaticoRespuestaDTO CargarInformacionProgramaAutomaticoSpeech(int idCentroCosto, int codigoPais, int idMatriculaCabecera, int idOportunidad);
        CargarInformacionProgramaRespuestaDTO CargarInformacionPrograma(int idPGeneral, int codigoPais, int idMatriculaCabecera, int idOportunidad);

        string ObtenerContenidoHorarios(List<ModalidadProgramaDTO> modalidades, string initContenido, int idPGeneral);
        string ObtenerContenidoTarifario(List<TarifarioDetalleAgendaDTO> tarifarios);
        List<ResumenProgramaV2DTO> CargarResumenProgramasV2(Dictionary<string, string> filtros);
        List<PreguntaFrecuenteSeccionesDTO> CargarInformacionPrograma(List<PreguntaFrecuentePGeneralRespuestaDTO> repositorioPreguntaFrecuente);
        List<PreguntaFrecuenteSeccionesV2DTO> CargarInformacionProgramaV2(List<PreguntaFrecuentePGeneralRespuestaDTO> repositorioPreguntaFrecuente);
        List<PreguntaFrecuenteSeccionesDTO> CargarInformacionProgramaChange(List<PreguntaFrecuentePGeneralRespuestaDTO> preguntaFrecuentePGeneralDTO);
        CargarInformacionProgramaEndpointsDTO ObtenerInformacionPrograma(int idCentroCosto, int codigoPais, int idMatriculaCabecera, int idOportunidad);
        CargarInformacionProgramaEndpointsDTO CargarInformacionProgramaSinHTML(int idPGeneral, int codigoPais, int idMatriculaCabecera, int idOportunidad);
        InformacionProgramaSpeechDTO CargarInformacionProgramaAutomaticoSpeechV2(int idCentroCosto, int codigoPais, int idMatriculaCabecera, int idOportunidad);
    }
}
