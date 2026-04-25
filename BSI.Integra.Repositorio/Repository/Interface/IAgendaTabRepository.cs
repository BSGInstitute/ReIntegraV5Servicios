using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAgendaTabRepository : IGenericRepository<TAgendaTab>
    {
        #region Metodos Base
        TAgendaTab Add(AgendaTab entidad);
        TAgendaTab Update(AgendaTab entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAgendaTab> Add(IEnumerable<AgendaTab> listadoEntidad);
        IEnumerable<TAgendaTab> Update(IEnumerable<AgendaTab> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<AgendaTabDTO> Obtener();
        IEnumerable<AgendaTabConfiguracionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo);
        IEnumerable<AgendaTabConfiguracionAlternoDTO> ObtenerTabsConfiguradosPorIdTab(string codigoAreaTrabajo, int idTab);
        List<ActividadAgendaDTO> ObtenerActividadesProgramada(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros);
        List<ActividadAgendaV2DTO> ObtenerActividadesProgramadaV2(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros);

        List<ActividadAgendaDTO> ObtenerActividadesNoProgramada(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros);
        List<ActividadAgendaV2DTO> ObtenerActividadesNoProgramadaV2(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros);

        List<ActividadAgendaDTO> ObtenerActividadesOperaciones(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros);
        List<ActividadAgendaV2DTO> ObtenerActividadesOperacionesV2(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros);

        List<ActividadAgendaDTO> ObtenerActividadesOperacionesFichaChat(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, int idMatriculaCabecera);
        int CantidadActividadesPorTabOperaciones(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros);
        List<ActividadAgendaDTO> ObtenerActividades(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros);
        List<ActividadAgendaV2DTO> ObtenerActividadesV2(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros);

        int CantidadActividadesPorTab(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros);
        List<PruebaActividadRealizadaDTO> ObtenerActividadesRealizadasSP(string idsAsesor, string fecha, int idCentroCosto, int idAlumno, int idFaseOportunidad, int idTipoDato, int idOrigen, int take, int skip, string idsCategoriaOrigen, int idProbabilidad, int idEstado);
        List<AgendaTabConfiguracionAlternoDTO> ObtenerTabsConfiguradosSinValidacion(string codigoAreaTrabajo);
        List<AgendaTabConfiguracionAlternoDTO> ObtenerTabsConfiguradosConValidacion(string codigoAreaTrabajo);
        List<AgendaTabConfiguracionAlternoDTO> ObtenerTabsConfiguradosConValidacionMarcador(string codigoAreaTrabajo);
        List<ComboDTO> CombosTabsAtencionAlCliente();
        AgendaTab ObtenerPorId(int id);
        List<ActividadAgendaDTO> ObtenerMensajesRecibidosComercial(int idPersonal);
        List<ActividadAgendaDTO> ObtenerMensajesRecibidosChatPortal(int idPersonal);
        List<ActividadAgendaDTO> ObtenerCorreosAgendaComercial(int idPersonal);
        List<ActividadAgendaDTO> ObtenerActividadesSolicitudesAgrupadas(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros);
    }
}