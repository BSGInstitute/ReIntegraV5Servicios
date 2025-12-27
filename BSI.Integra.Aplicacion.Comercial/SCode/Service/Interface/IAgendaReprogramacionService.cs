using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IAgendaReprogramacionService
    {
        Task<RespuestaFinalizarYProgramarActividadAlterno> FinalizarYProgramarActividadAlternoAsync(FinalizarProgramarActividadAlternoDTO dto);
        Task<bool> FinalizarYProgramarGestionAsync(FinalizarProgramarGestionPlaDTO dto);
        Task<(CompuestoActividadEjecutadaDTO realizada, int idOportunidad)> CerrarActividadAsync(CerrarActividadDTO jsonDTO);
        Task<(CompuestoActividadEjecutadaDTO realizada, int idOportunidad)> FinalizarActividadCrearOportunidadAlternoAsync(VentaCruzadaDTO ventaCruzadaDTO);
        ActividadAgendaDTO RealizarCambioCentroCosto(int idOportunidad, int idCentroCosto, string usuario);
        Task EnviarPlantillaAsync(List<OportunidadWhatsappEnvioDTO> lista);
    }
}
