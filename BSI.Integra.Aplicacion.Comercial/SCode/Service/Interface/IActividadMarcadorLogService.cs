using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IActividadMarcadorLogService
    {
        ActividadMarcadorLogDTO ObtenerPorIdActividadDetalleIdOportunidad(int idActividadDetalle, int idOportunidad);
        ActividadMarcadorLogDTO GuardarActividadMarcadorLog(ActividadMarcadorLogDTO jsonDTO, string usuario);
    }
}
