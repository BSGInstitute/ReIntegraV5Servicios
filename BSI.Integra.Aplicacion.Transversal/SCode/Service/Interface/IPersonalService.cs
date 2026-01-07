using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;
using MacDTO = BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.MacDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPersonalService
    {
        #region Metodos Base
        Personal Add(Personal entidad);
        Personal Update(Personal entidad);
        bool Delete(int id, string usuario);

        List<Personal> Add(List<Personal> listadoEntidad);
        List<Personal> Update(List<Personal> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PersonalComboDTO> ObtenerCombo();
        IEnumerable<PersonalDTO> ObtenerPersonal();
        IEnumerable<PersonalComboAreaDTO> ObtenerPersonaAreaTrabajo();

        StringDTO ObtenerPrimerNombreApellidoPaternoPorUserName(string usuario);
        BoolDTO ExistePersonalPorCorreo(string email);
        Personal ObtenerPorId(int idPersonal);
        PersonalInformacionCorreoDTO ObtenerNombreApellido(string email);
        List<PersonalAsignadoDTO> ObtenerPersonalAsignado(int idPersonal);
        Personal ObtenerPersonalPorId(int idPersonal);
        List<ReportePersonalDTO> ObtenerAsesoresVentasOficial();
        IEnumerable<PersonalAutocompleteDTO> CargarPersonalParaFiltro();
        List<PersonalAsignadoDTO> PersonalAsignadoOperacionesTotal(int idPersonal);
        List<PersonalAsignadoDTO> AsesoresVentasOficialReporteSeguimiento();
        List<PersonalAsignadoDTO> PersonalAsignadoVentas(int idPersonal);
        PersonalMinReasignacionDTO ObtenerPersonalReasignacion(int idAsesor);
        List<ReportePersonalDTO> ObtenerCoordinadoresVentasOficial();
        PersonalDatosAgendaDTO ObtenerDatosPersonalAgenda(int idPersonal);
        public List<DatosPersonalAsesorPorGrupoIdDTO> ObtenerAsesoresPorGrupoId(int idGrupo);
        bool ExistePorId(int idPlantilla);
        string ObtenerHorarioTrabajo(int id);
        string ObtenerFirmaCorreoImagen(string urlFoto, int? idCodigoPais = 0, int? idCiudad = 0);
        List<PersonalAutocompleteDTO> CargarPersonalAutoComplete(string nombre);
        List<FiltroDTO> ObtenerCoordinadoresParaFiltro();
        List<PersonalAsignadoDTO> ObtenerAsesoresVentasOficialReporteSeguimiento();
        List<PersonalConfiguracionOpenVoxDTO> ObtenerConfiguracionOpenVoxPorIdPersonal(int idPersonal);
        List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesTotalV2(int idPersonal);
        List<PersonalActivoEmailDTO> ObtenerTodoPersonalActivoParaFiltro();
        List<AsesorNombreFiltroDTO> ObtenerTodoAsesorCoordinadorVentas();
        IEnumerable<PersonalComboDTO> ObtenerPersonalPorMarketing();
        List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesUsuarioTotal(int idPersonal);
        PersonalAsignadoReportePendienteDTO ObtenerDatosUsuariosReportePendiente(string usuario);
        List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperaciones(int idPersonal);
        List<AsesorFiltroDTO> ObtenerPersonalAsesoresOperacionesActivos();
        (List<ReportePersonalDTO> asesores, List<ReportePersonalDTO> coordinadores) ObtenerAsesorCoordinadorVentasCombo();
        public List<FiltroCombosDTO> ObtenerComboPersonalGestionPersonas();
        public List<AsesorNombreFiltroDTO> ObtenerPersonalVentasV4();
        public List<PersonalAutocompleteDTO> ObtenerNombresFiltroAutoComplete(string valor);
        public List<PersonalAutocompleteDTO> ObtenerAsistenteAcademicoMatricula(string valor);
        public List<PersonalAutocompleteDTO> ObtenerPersonalAgendaLiberadaOperaciones();
        public IEnumerable<RegistrosMarcacionPersonal> ProcesarExcelRegistroMarcacion(IFormFile ArchivoExcel);
        public int ObtenerIdPersonalPorUserName(string UserName);
        PersonalWhatsAppDTO ObtenerDatosPersonalPorID(int IdPersonal);
        List<InduccionPersonalCalificacionAgrupadaDTO> ObtenerReportePersonal();
        List<InduccionPersonalCalificacionAgrupadaDTO> ObtenerReportePersonalFiltro(FiltroInduccionPersonalDTO filtro);
        Object ObtenerCombosInduccion();


        #region
        //Ficha de Datos de Postulante
        IEnumerable<PersonalFichaDatosDTO> ObtenerFichaDatosPersonal();
        ComboFichaDatosPersonalDTO ObtenerCombosFichaDatosPersonal();
        accesoPortalDTO ObtenerPEspecificoPersonalAccesoTemporalCombo();
        List<MaestroPersonalGrupoAccesoTemporalDTO> ObtenerListaAccesoTemporal(int idPersonal);
        FichaDatosPersonalDTO ObtenerInformacionPersonal(int idPersonal);
        bool Insertar(MaestroPersonalCompuestoDTO dto, string usuario);
        bool Actualizar(MaestroPersonalCompuestoDTO dto, string usuario);
        ArchivoDTO ObtenerArchivoPersonal(int idPersonalArchivo);
        DescargarArchivoDTO DescargarArchivoPersonal(int idPersonalArchivo);
        bool Eliminar(int id, string usuario);
        List<MaestroPersonalGrupoAccesoTemporalDTO> ActualizarAccesoTemporal(ActualizarAccesoTemporalDTO dto, string usuario);
        bool ActualizarAccesosTemporalesIntegra(ActualizarAccesoTemporalDTO datosAccesoTemporal, string usuario);
        void ReemplazarEtiquetasAlumnoSinOportunidad(EtiquetaParametroAlumnoSinOportunidadDTO parametrosEtiquetasOpcionales, ReemplazoEtiquetaPlantillaDTO reemplazoEtiquetaPlantilla);
        bool EliminarAccesoTemporal(EliminarAccesoTemporalDTO AccesoTemporal, string usuario);
        bool RegistrarArchivoFotoExpositor(ArchivoPersonalDTO files, string usuario);
        bool GuardarHorario(HorarioDTO dto, string usuario);
        #endregion


        // Ficha Reporte Personal Jefatura
        #region
        IEnumerable<FiltroPersonalJefaturaFiltroDTO> ObtenerReporteTodoPersonal(FiltroPersonalJefaturaDTO filtro);
        IEnumerable<ComboDTO> ObtenerCombosJefatura();
        PersonalJefaturaIteradorDTO ObtenerPersonalEncargadoJefatura();

        List<PersonalAutocompleteDTO> CargarPersonalAutoCompleteContrato(string nombre);



        #endregion
        //parte victor
        IEnumerable<PersonalDetalleDTO> ObtenerTodoPersonal();

        ResultadoDTOv2 InsertarPersonal(PersonalDetalleDTO Json, string usuarioIntegra);

        ResultadoDTOv2 ActualizarPersonal(PersonalDetalleDTO Json, string usuarioIntegra);

        public IEnumerable<ComboDTO> ObtenerPersonalActivo();

        public bool EnviarMensajeValidacionAcceso(EnvioCorreoValidacionAccesoDTO Json);

        public bool ResetarIp(MacDTO json);
        public bool GuardarHorario(PersonalHorarioDTO Json, string Usuario);

        public ResultadoDTOv2 InsertarMarcacionPersonalV2(string Usuario, int TipoBoton, string DNI);
    }
}
