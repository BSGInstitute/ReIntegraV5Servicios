using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPGeneralRepository : IGenericRepository<TPgeneral>
    {
        #region Metodos Base
        TPgeneral Add(PGeneral entidad);
        TPgeneral Update(PGeneral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneral> Add(IEnumerable<PGeneral> listadoEntidad);
        IEnumerable<TPgeneral> Update(IEnumerable<PGeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PGeneralComboDTO> ObtenerCombo();
        public IEnumerable<PGeneralComboDTO> ObtenerComboPorIdArea(int IdArea);
        IEnumerable<ComboDTO> ObtenerPGeneralLanzamientoPorEjecucion();
        IEnumerable<ProgramaGeneralComboDTO> ObtenerComboUrl();
        IEnumerable<PGeneralAlternoDTO> ObtenerPGeneral();
        PGeneralCabeceraSpeechAgendaDTO ObtenerCabeceraSpeechAgenda(int idOportunidad, int idCentroCosto);
        IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> ObtenerPublicoObjetivoProgramaParaAgenda(int idCentroCosto, int idOportunidad);
        IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3(int idCentroCosto, int idOportunidad);
        PGeneralAtributosPrincipalesDTO ObtenerPGeneralAtributosPrincipalesPorId(int idPGeneral);
        PgeneralDocumentoSeccionDTO ObtenerPgeneralDocumentoPorId(int id);
        PGeneralNombreDTO ObtenerPGeneralPorIdBusqueda(int idBusqueda);
        PGeneralAreaSubAreaDTO ObtenerPGeneralPEspecificoPorCentroCosto(int idCentroCosto);
        List<PadrePespecificoHijoDTO> ObtenerPadreHijoEspecificoV2(int idPGeneral);
        List<FrecuenciaProgramaGeneralDTO> ObtenerFrecuenciasPorIdPGeneral(int idPGeneral);
        List<PEspecificoSesionDTO> ObtenerSesionesProgramaGeneralValidadoVisualizacionAgenda(int idPGeneral);
        List<PEspecificoSesionDTO> ObtenerSesionesPorProgramaGeneral(int idPGeneral);
        List<MontoPagoProgramaDTO> ObtenerResumenProgramaV2(Dictionary<string, string> filtros);
        List<ProgramasPorCodigoPaisComboDTO> ObtenerProgramasPorCodigoPais(int codigoPais);
        List<ModalidadProgramaDTO> ObtenerModalidadesPorProgramaGeneral(int idPGeneral);
        List<InformacionProgramaDTO> ObtenerSeccionesInformacionProgramaPorProgramaGeneral(int idPGeneral);
        List<CorreosGmailDTO> ObtenerCorreosIdPersonalAprobacion(int idPersonal);
        PGeneralAlternoDTO ObtenerPGeneralPorId(int idPGeneral);
        PGeneralAreaSubAreaDTO ObtenerAreaSubAreaPorIdPGeneral(int idPGeneral);
        IEnumerable<PGeneralPrincipalDTO> ObtenerTodoGrid();
        public List<PGeneralProgramaCriticoSubAreaDTO> ObtenerPGeneralProgramaCriticoPorSubArea();
        public List<PGeneralSubAreaCapacitacionFiltroDTO> ObtenerProgramaSubAreaFiltroTodo();
        public int InsertaPGeneralSinIdentity(PGeneral programa);
        public IEnumerable<ComboDTO> ObtenerProgramasFiltro();
        Task<IEnumerable<ComboDTO>> ObtenerProgramasFiltroAsync();
        string ObtenerBeneficiosVersion(int idMatriculaCabecera);
        string ObtenerBeneficiosPorVersion(int id, int idPaquete, int idCodigoPais = 0);
        List<BeneficioDTO> ObtenerBeneficios(int idPGeneral, int idPais = 0);
        List<MontoPagoModalidadDTO> ObtenerMontosPorId(int idPGeneral);
        List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1(int idPGeneral, int codigoPais = 0);
        BeneficioDTO ObtenerBeneficiosPGeneralTipo2(int idPGeneral);
        string ObtenerVersion(int idMatriculaCabecera);
        string ObtenerDuracionMeses(int idMatriculaCabecera);
        List<PGeneralSubAreaCapacitacionFiltroDTO> ObtenerProgramaGeneralPorSubAreaId(int id);
        IEnumerable<ProgramaGeneralComboDTO> ProgramaGneralconPEspecifico();
        List<PGeneralCursoIrcaDTO> ObtenerCursosIrcaAlumno(int idMatriculaCabecera);
        ProgramaCentroCostoDTO ObtenerDatosPFrecuentes(int idCentroCosto);
        List<PGeneralSubAreaCapacitacionFiltroDTO> ObtenerTodoFiltro();
        List<IdDTO> ObtenerTodosPorIdArea(List<int> listaAreas);
        List<IdDTO> ObtenerTodosPorIdSubArea(List<int> listaSubAreas);
        List<PGeneralSubAreaCapacitacionFiltroDTO> ObtenerProgramaSubAreaFiltro();
        string? ObtenerCodigoPartner(int idMatriculacabecera);
        PGeneral ObtenerPorId(int id);
        Task<PGeneral> ObtenerPorIdAsync(int id);
        ProgramaSeccionIndividualDTO SeccionIndividualPGeneral(int idPGeneral, string seccion);
        Task<ProgramaSeccionIndividualDTO> SeccionIndividualPGeneralAsync(int idPGeneral, string seccion);
        List<ModalidadProgramaDTO> ObtenerFechaInicioProgramaGeneral(int idPGeneral, int IdCodigoPais);
        CuotasProgramaDTO ObtenerProgramaParaCuotas(int idMatricula);
        PgeneralDocumentoSeccionDTO ObtenePgeneralPorIdBusqueda(int idBusqueda);
        InformacionProgramaDocumentosDTO ObtenerPGeneralParaDocumentosPorIdAlumno(int idAlumno);
        bool ProgramaGeneralPadre(int idPGeneral);
        Task<bool> ProgramaGeneralPadreAsync(int idPGeneral);
        bool ProgramaGeneralEsTecnico(int idPGeneral);
        Task<bool> ProgramaGeneralEsTecnicoAsync(int idPGeneral);
        List<ListaCursosPorProgramaDTO> ListaCursosHijoPorIdPGeneral(int idPGeneral);
        int ObtenerDuracionCursoHijo(int idPGeneral);
        List<ContenidoHijoDTO> ContenidoEstructuraHijoPadre(int idPGeneral);
        bool ActualizarPgeneral(PGeneralAsignaVentaDTO Pgneral);
        IEnumerable<PGeneralSubAreaFiltroDTO> ObtenerPGeneralSubArea();
        IEnumerable<PGeneralSubAreaFiltroDTO> ObtenerFiltroPorTipo(bool aplicaTipo);
        Task<IEnumerable<PGeneralSubAreaFiltroDTO>> ObtenerFiltroPorTipoAsync(bool aplicaTipo);
        public IEnumerable<PGeneralHijoDTO> ObtenerPgeneralCursos(int idProgramaGeneral);
        PGeneralDatosDTO? ObtenerPGeneralParaPEspecifico(int idProgramaGeneral);
        IEnumerable<PgeneralWebinarDTO> ObtenerPGeneralWebinar();
        ProgramaGeneralTroncalDTO? ObtenerProgramaGeneralParaPespecificoPorId(int idProgramaGeneral);
        Task<IEnumerable<PGeneralPeriodoAsincronicoDTO>> ObtenerPgeneralPeriodoAsincronicoAsync();
        IEnumerable<PGeneralDTO> ListarProgramaGeneral(FiltroProgramaGeneralDTO filtro);
        List<PgeneralFechaOnlineDTO> ObtenerPGeneralFechaInicioOnline(int idPGeneral);
        List<PgeneralFechaPresencialDTO> ObtenerPGeneralFechaInicioPresencial(int idPGeneral);
        List<PgeneralFechaAonlineDTO> ObtenerPgeneralFechaInicioAonline(int IdProgramaGeneral);
        public List<PGeneralMontoPagoPanelDTO> ListarProgramaGeneralParaMontoPago();
        bool ValidarPRogramaPadreParaProyectoAPlicacion(int idPGeneral);
        IEnumerable<WebinarDetalleSesionDTO> ObtenerWebinarPorFiltro(WebinarReporteFiltroDTO filtro);
        List<ProgramaGeneralSubAreaFiltroDTO> ObtenerProgramaGeneralPadre(int? tipo);
        string ObtenerNombrePorIdPespecifico(int idPespecifico);
        List<PGeneralDTO> ObtenerTodo();
        IEnumerable<ComboDTO> ObtenerCentroCostoPorIdPgeneralFicha(int idPgeneral, int? idPais, int? idCiudad);
        public PgeneralIdPaginaDTO ObtenerIdPagina(int idCentroCosto);
        public IEnumerable<ProgramaGeneralComboDTO> ProgramaGneralconUrlVersion();
        public int? ObtenerPdu(int idMatriculacabecera);
        public int? ObtenerPduPorIdPGeneral(int IdPgeneral);
        IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3PorAlumno(int idOportunidad, int idAlumno);
    }
}
