using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAlumnoService
    {
        #region Metodos Base
        Alumno Add(Alumno entidad);
        Alumno Update(Alumno entidad);
        bool Delete(int id, string usuario);

        List<Alumno> Add(List<Alumno> listadoEntidad);
        List<Alumno> Update(List<Alumno> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        Alumno Alumno { get; set; }
        Alumno ObtenerPorId(int idAlumno);
        List<Alumno> Update(List<AlumnoDTO> entidad);
        IEnumerable<AlumnoComboDTO> ObtenerCombo();
        IEnumerable<AlumnoComboDTO> ObtenerAutocomplete(string nombreParcial);
        IntDTO ObtenerIdPaisPorIdAlumno(int idAlumno);
        AlumnoCiudadPaisDTO ObtenerCiudadPaisPorIdAlumno(int idAlumno);
        AlumnoDatosDocumentoDTO ObtenerDatosDocumentoPorIdAlumno(int idAlumno);
        AlumnoInformacionDTO ObtenerInformacionAlumnoPorIdClasificacionPersona(int idClasificacionPersona);
        string ObtenerCiudadOrigen(int idAlumno);
        string ObtenerPaisOrigen(int idAlumno);
        EnvioSMSOportunidad Obtener_EnvioSMSPorDiaOportunidad(int idOportunidad, DateTime fecha);
        bool InsertaSMSOportunidadUsuario(string celular, int idPersonal, int idAlumno, string mensaje, int parteMensaje, int idPais, string usuario);
        ValorIntDTO InsertaSMSOportunidad(int idOportunidad, DateTime fechaEnvio);
        Alumno ValidarEstadoContactoWhatsAppTemporalAlterno(Alumno alumno);
        //Alumno ValidarEstadoContactoWhatsAppTemporalGilmer(Alumno alumno);
        void ValidarEstadoContactoWhatsAppTemporal();
        Alumno MapeoEntidadDesdeDTO(AlumnoDTO dto);
        AlumnoEmailDTO ValidarEmail1Alumno(string email);
        AlumnoEmailDTO ValidarEmail2Alumno(string email);
        AlumnoCuponDTO ObtenerCuponPorIdAlumno(int idAlumno);
        ValorIntDTO InsertarSolicitudVisualizarDatosOportunidad(int idOportunidad, int idPersonal);
        string ObtenerNroWhatsAppCoordinador(int idCodigoPais);
        string ObtenerNroTelefonoCoordinador(int idCodigoPais, int idCiudad);
        string ObtenerFormaPago(int idCodigoPais);
        string ObtenerNroCelularCompleto(int idCodigoPais, string celular);
        string ObtenerNombreCompleto(Alumno alumno);
        bool EliminarFisicaAlumno(string tablaV3, string tablaV4, int idV4, string idv3, int? idv3Int);
        bool ExisteContacto(string email1, string email2, int idAlumno = 0);
        string ObtenerNombreProgramaGeneralUltimoEnvioMasivo(int id);
        string ObtenerNombreProgramaGeneralUltimaSolicitudInformacion(int id);
        AlumnoPorCelularDTO ObtenerPorCelular(string numero, string numeroAlterno);
        string ObtenerFechaInicioCapacitacion(int idMatriculaCabecera);
        string ObtenerFechaFinCapacitacion(int idMatriculaCabecera);
        string ObtenerNotaPromedio(int idMatriculaCabecera);
        string ObtenerFechaEmision();
        string ObtenerCodigoCertificado(int idMatriculaCabecera);
        string ObtenerUrlImagenFelizCumpleanios();
        string ObtenerFormaPagoReferencia(int idCodigoPais, int idModalidadCurso, int idCiudad, string codigoMatricula, string monedaCronograma);
        string CalcularCuentaAbonar(int idModalidadCurso, int idCiudad, string monedaCronograma);
        List<AlumnoComboDTO> ObtenerTodoComboAutoComplete(string valor);
        List<AlumnoComboDTO> AlumnnosTodoComboAutoCompletePorEmail(string valor);
        AlumnoInformacionMessengerDTO ObtenerAlumnoInformacionMessengerChatPorId(int idAlumno);
        List<AlumnoComboDTO> ObtenerTodoFiltroAutoCompleteReferido(int idR);
        string GuardarArchivosQR(byte[] archivo, string tipo, string nombreArchivo);
        string ObtenerFechaInicioCapacitacionPortalWeb(int idMatriculaCabecera);
        string ObtenerFechaFinCapacitacionPortalWeb(int idMatriculaCabecera);
        List<CronogramaNotaDTO> ObtenerCronogramaNota(int idMatriculaCabecera);
        List<CronogramaAsistenciaDTO> ObtenerCronogramaAsistencia(int idMatriculaCabecera);
        bool ExistePorId(int idPlantilla);
        AlumnoInformacionDTO ObtenerEstadoWhatsapp(int idAlumno);
        public IEnumerable<AlumnoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoComplete(string valor);
        AlumnoDatosDocumentoDTO ObtenerDatosAlumnoDocumentoPorId(int id);
        AlumnoComprobanteDTO ObtenerDatosAlumnoPorId(int id);
        string ObtenerNumeroWhatsApp(int codigoPais, string celular);
        string EncriptarStringCorreo(string email);
        string EncriptarStringNumero(string numero);
        List<AlumnoReferidosDTO> ObtenerReferidos(int idReferido);
        AlumnoInformacionDTO ObtenerPorIdClasificacionPersona(int idClasificacionPersona);
        bool EnviarSMS(int IdMatriculaCabecera, int IdPlantilla, int IdAsesor);
        AlumnoDTO ActualizarEmailPrincipal(AlumnoActualizarEmailPrincipalDTO dto, string usuario);
        (int IdClasificacionPersona, bool EstadoReasignacion) ReasignacionOportunidadesActualizarEmail(AlumnoActualizarEmailPrincipalDTO dto, string usuario);
        AlumnoActualizarDTO ActualizarAlumno(AlumnoActualizarDTO dto, string usuario, string areaTrabajo);
        //nuevo actualizar para la nueva version agenda
        TAlumno ActualizarAlumnoAFormacion(int idAlumno,int idNuevo, string usuario);
        TAlumno ActualizarAlumnoCargo(int idAlumno, int idNuevo, string usuario);
        TAlumno ActualizarAlumnoIndustria(int idAlumno, int idNuevo, string usuario);
        TAlumno ActualizarAlumnoAreaTrabajo(int idAlumno, int idNuevo, string usuario);
        TAlumno ActualizarAlumnoEmpresa(int idAlumno, int idNuevo, string usuario);
        TAlumno ActualizarAlumnoTamanioEmpresaAgenda(int idAlumno, int idNuevo, string usuario);
        TAlumno ActualizarAlumnoExperiencia(int idAlumno, int idNuevo, string usuario);
        TAlumno ActualizarAlumnoPrincipalResponsabilidad(int idAlumno, string nuevoValor, string usuario);
        //fin nuevo actualizar para la nueva version agenda
        bool ObtenerAlumnoPorDNI(StringDTO valor);
        PruebaCFD ObtenerAlumnoPorDNIV2(StringDTO valor);
        InformacionAlumnoDTO ObtenerInformacionAlumno(int idAlumno);
        int ValidarAlumnoExiste(AlumnoAutocompleteEmailDTO filtro);
    }
}
