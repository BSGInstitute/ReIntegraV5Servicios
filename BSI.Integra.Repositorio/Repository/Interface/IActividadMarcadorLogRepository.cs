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
        ActividadAgendaMarcadorDTO? ObtenerActividadesUnaMasdeUna(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento);
        ActividadAgendaMarcadorDTO? ObtenerActividadesIpIcPfWavix(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento,DateTime? fechaActual);
        ActividadAgendaMarcadorDTO? ObtenerActividadesAutomaticaMarcadorWavix(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorDTO? ObtenerActividadesRN2BMarcadorWavix(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorDTO? ObtenerActividadesRN2AMarcadorWavix(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento);
        ActividadAgendaMarcadorDTO? ObtenerActividadesProgramadasManualesMarcadorWavix(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento,DateTime? FechaActual);

        ActividadAgendaMarcadorATCDTO? ObtenerActividadesPrioridad1ATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesPrioridad2ATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesClasesOnlineATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesCompromisosATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesPagoAtrasadoMyPATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesPagoAtrasadoATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesPagoDelDiaATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesPagoAlDiaATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesSeguimientoATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesPorAbandonarATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesPreReporteATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesReporteATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesSinContactoATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesReservaConDeudaATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesCulminadoATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        List<ActividadAgendaMarcadorATCDTO>? ObtenerActividadesMasdeUnaTabATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        List<ActividadAgendaMarcadorATCDTO>? ObtenerActividadesCuotaVenceHoyATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);
        ActividadAgendaMarcadorATCDTO? ObtenerActividadesPrioridad6ATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual);

    }
}