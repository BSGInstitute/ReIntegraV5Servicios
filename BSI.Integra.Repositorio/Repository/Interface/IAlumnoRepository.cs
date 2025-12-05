using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAlumnoRepository : IGenericRepository<TAlumno>
    {
        #region Metodos Base
        TAlumno Add(Alumno entidad);
        TAlumno Update(Alumno entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAlumno> Add(IEnumerable<Alumno> listadoEntidad);
        IEnumerable<TAlumno> Update(IEnumerable<Alumno> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<AlumnoDTO> ObtenerAlumno();
        Alumno? ObtenerPorId(int idAlumno);
        Task<Alumno> ObtenerPorIdAsync(int idAlumno);
        IEnumerable<AlumnoComboDTO> ObtenerCombo();
        IEnumerable<AlumnoComboDTO> ObtenerAutocomplete(string nombreParcial);
        IEnumerable<AlumnoComboDTO> ObtenerAlumnoMatriculadoAutocomplete(string nombreParcial);
        IntDTO ObtenerIdPaisPorIdAlumno(int idAlumno);
        Task<IntDTO> ObtenerIdPaisPorIdAlumnoAsync(int idAlumno);
        AlumnoCiudadPaisDTO ObtenerCiudadPaisPorIdAlumno(int idAlumno);
        AlumnoDatosDocumentoDTO ObtenerDatosDocumentoPorIdAlumno(int idAlumno);
        AlumnoInformacionDTO ObtenerInformacionAlumnoPorIdClasificacionPersona(int idClasificacionPersona);
        AlumnoPorCelularDTO? ObtenerAlumnoPorCelular(string celular);
        StringDTO? ObtenerEnvioMasivoSMSPorIdAlumno(int idAlumno);
        string ObtenerCiudadOrigen(int idAlumno);
        Task<string> ObtenerCiudadOrigenAsync(int idAlumno);
        string ObtenerPaisOrigen(int idAlumno);
        Task<string> ObtenerPaisOrigenAsync(int idAlumno);
        EnvioSMSOportunidad Obtener_EnvioSMSPorDiaOportunidad(int idOportunidad, DateTime fecha);
        bool InsertaSMSOportunidadUsuario(string celular, int idPersonal, int idAlumno, string mensaje, int parteMensaje, int idPais, string usuario);
        ValorIntDTO InsertaSMSOportunidad(int idOportunidad, DateTime fechaEnvio);
        AlumnoEmailDTO? ValidarEmail1Alumno(string email);
        AlumnoEmailDTO ValidarEmail2Alumno(string email);
        AlumnoCuponDTO ObtenerCuponPorIdAlumno(int idAlumno);
        ValorIntDTO InsertarSolicitudVisualizarDatosOportunidad(int idOportunidad, int idPersonal);
        List<AlumnoEmailDTO> ObtenerAlumnoPorEmail(string email1, string email2);
        AlumnoEmailDTO? ObtenerEmailAlumno(int idAlumno);
        bool ExisteContacto(string email1, string email2, int id = 0);
        string ObtenerNombreProgramaGeneralUltimoEnvioMasivo(int id);
        string ObtenerNombreProgramaGeneralUltimaSolicitudInformacion(int id);
        AlumnoPorCelularDTO ObtenerPorCelular(string numero, string numeroAlterno);
        bool EliminarFisicaAlumno(string NombreTablaV3, string NombreTablaV4, int IdV4, string Idv3, int? Idv3Int);
        string ObtenerFechaInicioCapacitacion(int idMatriculaCabecera);
        string ObtenerFechaFinCapacitacion(int idMatriculaCabecera);
        string ObtenerNotaPromedio(int idMatriculaCabecera);
        string ObtenerFechaEmision();
        string ObtenerCodigoCertificado(int idMatriculaCabecera);
        string ObtenerUrlImagenFelizCumpleanios();
        AlumnoValidarEmailDTO ValidarEmailALumno(string Email1, string Email2);
        AlumnoInformacionMessengerDTO ObtenerAlumnoInformacionMessengerChatPorId(int idAlumno);
        List<AlumnoComboDTO> ObtenerTodoComboAutoComplete(string valor);
        List<AlumnoComboDTO> AlumnnosTodoComboAutoCompletePorEmail(string valor);
        List<AlumnoComboDTO> ObtenerTodoFiltroAutoCompleteReferido(int idR);
        string ObtenerFechaInicioCapacitacionPortalWeb(int idMatriculaCabecera);
        string ObtenerFechaFinCapacitacionPortalWeb(int idMatriculaCabecera);
        List<CronogramaNotaDTO> ObtenerCronogramaNota(int idMatriculaCabecera);
        List<CronogramaAsistenciaDTO> ObtenerCronogramaAsistencia(int idMatriculaCabecera);
        public Alumno ObtenerPorEmail(string email1, string email2);
        AlumnoInformacionDTO ObtenerEstadoWhatsapp(int idAlumno);
        AlumnoDatosDocumentoDTO ObtenerDatosAlumnoDocumentoPorId(int id);
        public IEnumerable<AlumnoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoComplete(string valor);
        AlumnoComprobanteDTO ObtenerDatosAlumnoPorId(int id);
        List<AlumnoWhatsappDTO> ObtenerALumnosaValidarWhatsapp();
        string ObtenerNumeroWhatsApp(int codigoPais, string celular);
        AccesosMoodleDTO ObtenerAccesosMoodle(string usuarioMoodle);
        List<AlumnoDTO> ObtenerALumnosaValidarWhatsappPeru(int cantidad, int iterador);
        public IntDTO ActualizarValidos(string alumnos, int estadoWhatsApp);
        IntDTO ActualizarValidosSecundario(string alumnos, int estadoWhatsApp);
        List<AlumnoDTO> ObtenerALumnosaValidarWhatsappColombia(int cantidad, int iterador);
        public List<AlumnoDTO> ObtenerALumnosaValidarWhatsappBolivia(int cantidad, int iterador);
        public List<AlumnoDTO> ObtenerALumnosaValidarWhatsappInternacional(int cantidad, int iterador);
        public List<AlumnoDTO> ObtenerALumnosaRegularizarWhatsappPeru();
        public List<AlumnoDTO> ObtenerALumnosaRegularizarWhatsappColombia();
        public List<AlumnoDTO> ObtenerALumnosaRegularizarWhatsappBolivia();
        public List<AlumnoDTO> ObtenerALumnosaRegularizarWhatsappInternacional();
        AlumnoInformacionDTO ObtenerPorIdClasificacionPersona(int idClasificacionPersona);
        AlumnoInformacionDTO ObtenerPorIdActividadDetalle(int idActividadDetalle);
        AlumnoAccesosDTO ObtenerAccesosAlumno(int idAlumno);
        DatosCorbranzaAlumnoDTO obtenerDatosCobranzaAlumno(int idMatriculaCabecera);
        AvanceAonlineAlumnoDTO obtenerDatosAvanceAonline(int idMatriculaCabecera);
        AvanceOnlineAlumnoDTO obtenerDatosAvanceOnline(int idMatriculaCabecera);

        List<AlumnoReferidosDTO> ObtenerReferidos(int idReferido);
        List<AlumnoFiltroAutocompleteDTO> ObtenerTodoFiltrosAutoComplete(string valor);
        string guardarArchivosQR(byte[] archivo, string tipo, string nombreArchivo);

        public StringDTO ObtenerCelularPrincipalPorId(int idAlumno);
        public StringDTO ObtenerEmailPrincipalPorId(int idAlumno);
        //NombreCompletoAlumnoDTO ObtenerNombreCompletoAlumnoPorEmail1(string email);
        public string ObtenerEmail(int id);
        IEnumerable<ComboDTO> ObtenerTodoFiltroTipoCategoriaError();
        AlumnoEmailPrincipalDTO ValidarEmailPrincipal(string email);
        AlumnoEmailPrincipalDTO ValidarEmailSecundario(string email2);
        NombreCompletoAlumnoDTO ObtenerNombreCompletoAlumnoPorId(int id);
        ResultadoFinalDTO ActualizarContestoPredictivo(int id);
        ResultadoFinalDTO ActualizarCreoOportunidadPredictivo(int id, int idOportunidadCreada);

        bool ObtenerAlumnoPorDNI(StringDTO valor);
        PruebaCFD ObtenerAlumnoPorDNIV2(StringDTO valor);
        InformacionAlumnoDTO ObtenerInformacionAlumno(int idAlumno);
        AlumnoInformacionDTO ObtenerInformacionAlumnoPorIdAlumno(int idAlumno);
        bool ActualizarAlumnoWhatsapp(DatosAlumnoWhatsappDTO valor);
        StringDTO RegistrarLoginPortal(int idAlumno, string usuario);
        AvatarAlumnoDTO ObtenerAvatar(int IdAlumno, string Genero);
        Alumno? ObtenerPorEmail1(string email1);
        CredencialesPortalWebAlumnoDTO ObtenerCredencialesPortalWebPorIdAlumno(int idAlumno);

    }
}
