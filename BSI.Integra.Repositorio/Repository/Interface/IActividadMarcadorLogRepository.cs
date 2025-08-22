using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IActividadMarcadorLogRepository : IGenericRepository<TActividadMarcadorLog>
    {
        #region Metodos Base
        TActividadMarcadorLog Add(ActividadMarcadorLog entidad);
        TActividadMarcadorLog AddAsync(ActividadMarcadorLog entidad);
        TActividadMarcadorLog Update(ActividadMarcadorLog entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TActividadMarcadorLog> Add(IEnumerable<ActividadMarcadorLog> listadoEntidad);
        IEnumerable<TActividadMarcadorLog> Update(IEnumerable<ActividadMarcadorLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<ActividadAgendaMarcadorDTO>? ObtenerActividadesProgramadaMarcador(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor);
        List<ActividadAgendaMarcadorDTO>? ObtenerActividadesNoProgramadaMarcador(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor);
        List<ActividadAgendaMarcadorDTO>? ObtenerActividadesMarcador(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor);
        ActividadAgendaMarcadorDTO? ObtenerActividadesAutomaticaMarcador(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool validarIntento);
        ActividadMarcadorLog? ObtenerPorIdActividadDetalleIdOportunidad(int idActividadDetalle, int idOportunidad);
        ActividadAgendaMarcadorDTO? ObtenerActividadesProgramadasManualesMarcador(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool validarIntento);
        ActividadAgendaMarcadorDTO? ObtenerActividadesIpIcPf(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool validarIntento);
    }
}