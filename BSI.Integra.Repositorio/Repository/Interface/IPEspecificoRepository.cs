using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPEspecificoRepository : IGenericRepository<TPespecifico>
    {
        #region Metodos Base
        TPespecifico Add(PEspecifico entidad);
        TPespecifico Update(PEspecifico entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPespecifico> Add(IEnumerable<PEspecifico> listadoEntidad);
        IEnumerable<TPespecifico> Update(IEnumerable<PEspecifico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PEspecifico> Obtener();
        PEspecifico? ObtenerPorId(int idPEspecifico);
        List<PEspecifico> ObtenerPorIds(IEnumerable<int> ids);
        IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerFiltro();
        PEspecificoPorIdCentroCostoDTO? ObtenerPorIdCentroCosto(int idCentroCosto);
        Task<PEspecificoPorIdCentroCostoDTO> ObtenerPorIdCentroCostoAsync(int idCentroCosto);
        List<SeccionEtiquetaDTO> ObtenerSeccionEtiquetaPorIdCentroCosto(int idCentroCosto);
        Task<List<SeccionEtiquetaDTO>> ObtenerSeccionEtiquetaPorIdCentroCostoAsync(int idCentroCosto);
        List<PEspecificoPorIdPGeneral> ObtenerPorIdPGeneral(int idPGeneral);
        FechaInicioProgramaEspecificoDTO FechaProgramaEspecifico(int idProgramaGeneral, int idProgramaEspecifico);
        Task<FechaInicioProgramaEspecificoDTO> FechaProgramaEspecificoAsync(int idProgramaGeneral, int idProgramaEspecifico);
        SeccionEtiquetaDTO? ObtenerContenidoTemplate(Guid IdPlantillaPW, Guid IdSeccionPW, int idCentroCosto);
        PEspecificoInformacionDTO ObtenerPespecificoPorOportunidad(int idOportunidad);
        PEspecificoIdTipoDTO ObtenerTipoIdPorIdCentroCosto(int idCentroCosto);
        List<PEspecificoPGeneralFiltroDTO> ObtenerCombo();
        IntDTO ObtenerIdCiudad(int idPEspecifico);
        StringDTO ObtenerSeccionEtiquetaAgendaMensaje(string idSeccion, string idPlantilla, int idCentroCosto);
        PeriodoDuracionProgramaEspecificoDTO ObtenerPeriodoDuracion(int idPEspecifico, int idMatriculaCabecera);
        string ObtenerUrlAccesoSesionOnline(int id);
        List<ConjuntoSesionProgramaEspecificoDTO> ObtenerConjuntoSesionProgramaEspecifico(int idPEspecifico);
        List<ConjuntoSesionProgramaEspecificoDTO> ObtenerProximoConjuntoSesionProgramaEspecifico(int idPEspecifico, int cantidadDias);
        List<ConjuntoSesionProgramaEspecificoDTO> ObtenerProximoConjuntoSesionProgramaEspecificoWebex(int idPEspecifico, int cantidadDias);
        PEspecificoValorDTO ObtenerDetalle(int idMatriculaCabecera);
        IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerFiltroPorIdPGeneral(int idPGeneral);
        string ObtenerUrlQuejaSugerenciaNDiasNHora(int idMatriculaCabecera, int cantidadDias, int cantidadHoras);
        string ObtenerNombreCursoEncuestaNDiasNHora(int idMatriculaCabecera, int cantidadDias, int cantidadHoras);
        string ObtenerUrlEncuestaNDiasNHora(int idMatriculaCabecera, int cantidadDias, int cantidadHoras);
        string ObtenerFechaEmisionUltimoCertificado(int id, int idMatriculaCabecera);
        List<PEspecificoPGeneralFiltroDTO> ObtenerPEspecificoNuevaAulaVirtual();
        bool ExisteId(int idPEspecifico);
        List<PEspecificoNuevoAulaVirtualDTO> ObtenerPEspecificoNuevoAulaVirtualTipo();
        List<PEspecificoComboDTO> ObtenerPEspecificoRelacionadoPorIdPGeneral(int idPEspecifico, int idMatriculaCabecera);
        List<PEspecificoComboDTO> ObtenerPEspecificoRelacionadoPGeneral(int idPEspecifico, int idMatriculaCabecera);
        IEnumerable<ComboDTO> ObtenerProgramasEspecificosAdicional();
        List<PEspecificoComboDTO> ObtenerPEspecificoRelacionadoIrca(int idPEspecifico, int idMatriculaCabecera, bool esCursoDSig);
        string ObtenerNombrePEspecifico(int idPEspecifico);
        List<PEspecificoPGeneralFiltroDTO> ObtenerListaProgramaEspecificoParaTabla();
        public List<PEspecificoComboDTO> ObtenerPEspecificoPorCentroCosto(string nombre);
        public List<ComboDTO> ObtenerPorNombreAutocomplete(string valor);
        public List<CursosCentroCostoDTO> ObtenerCursosCentroCosto(int idPEspecifico = 0);
        IEnumerable<ProgramaEspecificoPadreIndividualDTO> ObtenerProgramaEspecificoPadreIndividualFiltro(PEspecificoFiltroSPDTO filtro);
        IEnumerable<ComboDTO> ObtenerProgramaEspecifico();
        IEnumerable<ComboDTO> ObtenerProgramaEspecificoPorIdPGeneral(List<int> idPGeneral);
        IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerFiltroPorTipo(bool aplicaTipo);
        Task<IEnumerable<PEspecificoPGeneralFiltroDTO>> ObtenerFiltroPorTipoAsync(bool aplicaTipo);
        IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerPEspecificoHijoFiltro();
        Task<IEnumerable<PEspecificoPGeneralFiltroDTO>> ObtenerPEspecificoHijoFiltroAsync();
        IEnumerable<PEspecificoRelacionadoFiltroDTO> ObtenerListaPEspecificosRelacionados();
        Task<IEnumerable<PEspecificoRelacionadoFiltroDTO>> ObtenerListaPEspecificosRelacionadosAsync();
        DatosConfiguracionProgramasWebexDTO? ObtenerConfiguracionWebinarPEspecifico(int idPEspecifico);
        DatosProgramaEspecificoDTO? ObtenerProgramaEspecificoPorCodigo(int idPEspecifico);
        RegistroProgramaEspecificoDTO? ObtenerRegistroPespecificoPorId(int idPespecifico);
        List<ComboDTO> ObtenerPEspecificoWebinar();
        DatosPGeneralDTO ObtenerDatosPGeneralParaPEspecifico(int idProgramaGeneral);
        CategoriaCiudadDTO ObtenerCiudadCategoria(int idCiudad, int idCategoriaPrograma);
        public IEnumerable<ComboDTO> ObtenerGruposSesiones(int idPadre);
        IEnumerable<ComboDTO> ObtenerGrupoEdicionDisponible(int idPEspecifico);
        IEnumerable<ComboDTO> ObtenerGruposSesionesIndividuales(int idPadre);
        IEnumerable<CronogramaGrupoDTO> ObtenerCronogramaPEspecificoGrupoSesionIndividual(int idPEspecifico, int numeroGrupo);
        IEnumerable<CronogramaGrupoDTO> ObtenerCronogramaPEspecificoGrupo(int idPEspecifico, List<int> listaPespecifico, int numeroGrupo);
        IEnumerable<DatosProgramaEspecificoDuracionDTO> ObtenerDatosDuracionPorIdPespecificoPadre(int idPespecificoPadre);
        IEnumerable<ReporteAmbienteDTO> ObtenerExcelReporteAmbiente(FiltroReporteAmbienteDTO filtro);
        DatosListaPespecificoDTO? ObtenerDatosCompletosPespecificoPorId(int idPespecifico);
        IntDTO EliminarFrecuenciaWebinar(int idProgramaEspecifico, string usuario = "SYSTEM");
        public IntDTO InsertarFrecuenciaWebinar(ParametrosInsertaFrecuenciaDTO dto);
        List<CruceSesionPEspecificoDTO> ValidarFechaExpositorCruce(DocenteAmbientePEspecificoDTO dto, IEnumerable<PEspecificoSesionFechasDTO> fechas);
        IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerComboSinValidacion();
        IEnumerable<PEspecificoGrupoDTO> ObtenerPEspecificoGruposPorPEspecificoPadre();
        IEnumerable<ComboDTO> ObtenerPEspecificoWebinarPorIdPGeneral(int idPGeneral);
        List<ProgramaEspecificoMaterialDTO> ObtenerPorFiltro(ProgramaEspecificoMaterialFiltroDTO filtro);
        List<PEspecificoProgramaGeneralFiltroDTO> ObtenerProgramasEspecificosPadres(int? tipo);
        string? ObtenerDuracionProgramaEspecificoModulo(int IdPespecifico, int IdMatriculaCabecera);
        public PespecificoCentroCostoDTO ObtenerCentroCostoPresencial(string nombre, string ciudad);
        public PespecificoCentroCostoDTO ObtenerCentroCostoOnline(string nombre);

        //Ficha Datos Personal
        #region
        List<PEspecificoNuevoAulaVirtualDTO> ObtenerPEspecificoPersonalNuevoAulaVirtualTipo();
        #endregion

        IEnumerable<PEspecificoDetalleFechaByPGeneral> ObtenerFiltroV2PorIdPGeneral(int idPGeneral);
        PEspecificoDetalleFechaByPGeneral ObtenerFechaInicioCursoPorIdPEspeficico(int idPEspecifico);
		IEnumerable<PEspecificoByPGeneral> ObtenerPEspecificoByProgramaGeneral(int idPGeneral);
        public bool ActualizarConfiguracionPEspecificoAlumnoResumen(int idPEspecifico, string usuario);
    }
}