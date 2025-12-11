using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPersonalRepository : IGenericRepository<TPersonal>
    {
        #region Metodos Base
        TPersonal Add(Personal entidad);
        TPersonal Update(Personal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonal> Add(IEnumerable<Personal> listadoEntidad);
        IEnumerable<TPersonal> Update(IEnumerable<Personal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PersonalComboDTO> ObtenerCombo();
        IEnumerable<PersonalDTO> ObtenerPersonal();
        StringDTO ObtenerPrimerNombreApellidoPaternoPorUserName(string usuario);
        BoolDTO ExistePersonalPorCorreo(string email);
        Task<IntDTO> ObtenerDiferenciaHorariaAsync(int idPersonal);
        IntDTO? ObtenerDiferenciaHoraria(int idPersonal);
        Personal? ObtenerPorId(int idPersonal);
        PersonalInformacionCorreoDTO ObtenerNombreApellido(string email);
        List<PersonalAsignadoDTO> ObtenerPersonalAsignado(int idPersonal);
        Personal ObtenerPersonalPorId(int idPersonal);
        List<ReportePersonalDTO> ObtenerAsesoresVentasOficial();
        List<ReportePersonalDTO> ObtenerAsesoresVentasOficialRI(int idPersonal);
        List<ReportePersonalDTO> ObtenerAsesoresVentasOficial_CF(int idPersonal);
        List<ReportePersonalDTO> ObtenerAsesoresVentasOficial_CONT(int idPersonal);
        IEnumerable<PersonalAutocompleteDTO> CargarPersonalParaFiltro();
        List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesTotal(int idPersonal);
        List<PersonalAsignadoDTO> ObtenerAsesoresVentasOficialReporteSeguimiento();
        List<PersonalAsignadoDTO> ObtenerPersonalAsignadoVentas(int idPersonal);
        List<PersonalAsignadoDTO> ObtenerPersonalAsignadoVentasRS(int idPersonal);
        List<ReportePersonalDTO> ObtenerCoordinadoresVentasOficialTCC(int idPersonal);
        Task<List<PersonalAsignadoDTO>> ObtenerPersonalAsignadoVentasAsync(int idPersonal);
        List<AsesorFiltroDTO> ObtenerPersonalAsesoresFiltro();
        List<CoordinadorFiltroDTO> ObtenerPersonalCoordinadoresFiltro();
        PersonalMinReasignacionDTO ObtenerPersonalReasignacion(int idAsesor);
        List<ReportePersonalDTO> ObtenerCoordinadoresVentasOficial();
        List<ReportePersonalDTO> ObtenerCoordinadoresVentasOficialRI(int idPersonal);
        PersonalDatosAgendaDTO ObtenerDatosPersonalAgenda(int idPersonal);
        public List<DatosPersonalAsesorPorGrupoIdDTO> ObtenerAsesoresPorGrupoId(int idGrupo);
        DatoCompletoPersonalDTO ObtenerDatoPersonal(int id);
        string ObtenerHorarioTrabajo(int id);
        public List<PersonalActivoEmailDTO> ObtenerTodoPersonalActivoParaFiltro();
        List<PersonalAutocompleteDTO> CargarPersonalAutoComplete(string nombre);
        List<FiltroDTO> ObtenerCoordinadoresParaFiltro();
        List<PersonalConfiguracionOpenVoxDTO> ObtenerConfiguracionOpenVoxPorIdPersonal(int idPersonal);
        List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesTotalV2(int idPersonal);


        List<AsesorNombreFiltroDTO> ObtenerTodoAsesorCoordinadorVentas();
        IEnumerable<PersonalComboDTO> ObtenerPersonalPorMarketing();
        List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesUsuarioTotal(int idPersonal);
        PersonalAsignadoReportePendienteDTO ObtenerDatosUsuariosReportePendiente(string usuario);
        object ObtenerAsesorPorApellidos();
        public object ObtenerCoordinadorPorApellidos();
        List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperaciones(int idPersonal);
        List<AsesorFiltroDTO> ObtenerPersonalAsesoresOperacionesActivos();
        public List<PersonalComboAprobadoDTO> ObtenerPersonalAprobadoPorApellido(Dictionary<string, string> Valor);
        public StringDTO ObtenerAnexoPersonal(int idPersonal);
        public StringDTO ObtenerAnexo3CXPersonal(int idPersonal);
        public StringDTO ObtenerCentralPersonal(int idPersonal);
        public StringDTO ObtenerEmailPorId(int idPersonal);
        IEnumerable<PersonalComboDTO> ObtenerCoordinadorasOperaciones();
        public List<FiltroCombosDTO> ObtenerComboPersonalGestionPersonas();
        FiltroCombosDTO ObtenerPersonalGestionPersonasPorId(int idPersonal);
        public List<AsesorNombreFiltroDTO> ObtenerPersonalVentasV4();
        List<ComboDTO> ObtenerCoordinadorasDocente();
        int? ObtenerPaisSedePersonal(int idPersonal);

        public List<PersonalAutocompleteDTO> ObtenerNombresFiltroAutoComplete(string valor);
        public List<PersonalAutocompleteDTO> ObtenerAsistenteAcademicoMatricula(string valor);
        IEnumerable<ComboDTO> ObtenerPersonalAutocomplete(string valor);
        IEnumerable<PersonalComboAreaDTO> ObtenerPersonalAreaTrabajo();

        bool InsertarNuevaContrasena(PersonalNuevaContraseniaDTO dto);
        public List<PersonalAutocompleteDTO> ObtenerPersonalAgendaLiberadaOperaciones();

        public bool InsertarMarcacionPersonal(string data, string usuario);
        public int ObtenerIdPersonalPorUserName(string UserName);
        PersonalWhatsAppDTO ObtenerDatosPersonalPorID(int IdPersonal);

        List<InduccionPersonalDTO> ObtenerReportePersonal();
        List<InduccionPersonalDTO> ObtenerReportePersonalFiltro(FiltroInduccionPersonalDTO filtro);
        List<PersonalFormularioDTO> ObtenerInfoContrato(int IdPersonal);
        List<PersonalExperienciaFormularioDTO> ObtenerPersonalExperiencia(int IdPersonal);
        List<PersonalFormacionFormularioDTO> ObtenerPersonalFormacion(int IdPersonal);
        List<PersonalIdiomaFormularioDTO> ObtenerPersonalIdioma(int IdPersonal);
        List<ContratoHistoricoRegistroDTO> ObtenerContratoHistorico(int IdPersonal);
        List<PersonalAutocompleteDTO> CargarPersonalAutoCompleteContrato(string nombre);
        string EsPersonalCoordinador(int IdPersonal);


        MaestroPersonalPuestoSedeDTO ObtenerInformacionPersonalPuestoSede(int idPersonal);
        //***********************
        //Ficha Datos Personal
        //***********************
        #region
        IEnumerable<PersonalFichaDatosDTO> ObtenerFichaDatosPersonal();

        IEnumerable<ComboDTO> ObtenerComboNombre();
        IEnumerable<ComboDTO> ObtenerAsesorCerrador();

        List<PersonalDireccionVistaDTO> ObtenerPersonalDireccionDomiciliaria(int idPersonal);
        int? ObtenerPersonalEliminadoEmailRepetido(string email);
        bool ActivarPersonal(int Id);
        #endregion


        //***********************
        //Reporte Personal Jerarquia
        //***********************
        #region

        List<FiltroPersonalJefaturaFiltroDTO> ObtenerPersonalJefaturaFiltro(string condiciones);
        List<PersonalJefaturaDTO> ObtenerPersonalJefatura();


        #endregion



        List<DatoPersonalPersonalAprobacionDTO> ObtenerDatosPersonal();

        //victor hinojosa

        IEnumerable<PersonalDetalleDTO> ObtenerTodoPersonal();

        public Personal ObtenerListaPersonalPorEmail(string email, int id);

        //public int? ObtenerPersonalEliminadoEmailRepetido(string email);

        //public bool ActivarPersonal(int id);

        public IEnumerable<ComboDTO> ObtenerPersonalActivo();

        public List<PersonalDTO> ObtenerValidacionAnexo(int id, string anexo);
        PersonalInformacionCorreoDTO ObtenerNombreApellidoPorId(int id);

        DatoPersonalDTO ObtenerIdentidadUsusarioDNI(string usuario, string dni);

        // Métodos para RegistroMarcadorFecha
        bool InsertarRegistroMarcacion(RegistroMarcadorFechaBO registro);
        bool ActualizarRegistroMarcacion(RegistroMarcadorFechaBO registro);
        RegistroMarcadorFechaBO ObtenerRegistroMarcacionPorFiltro(int idPersonal, DateTime fecha, string pin);
    }
}
