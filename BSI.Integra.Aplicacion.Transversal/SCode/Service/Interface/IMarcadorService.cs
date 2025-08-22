using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IMarcadorService
    {
        ActividadAgendaMarcadorDTO? ObtenerActividad(int idAsesor);
        ActividadMarcadorLogDTO GuardarActividadMarcador(ActividadMarcadorLogDTO dto, string usuario);
        ActividadMarcadorLogDTO ObtenerPorIdActividadDetalleIdOportunidad(int idActividadDetalle, int idOportunidad);
        ActividadMarcadorLogDTO GuardarNoContestadoMarcador(ActividadMarcadorLogDTO dto, string usuario);
        ActividadMarcadorLogDTO GuardarContestadoMarcador(ActividadMarcadorLogDTO dto, string usuario);
    }
}
