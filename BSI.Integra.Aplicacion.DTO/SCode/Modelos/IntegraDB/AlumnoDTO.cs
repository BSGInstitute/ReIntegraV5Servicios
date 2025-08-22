namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AlumnoDTO
    {
        public int Id { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Dni { get; set; }
        public string? Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Pais { get; set; }
        public int? Ciudad { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public string? NivelFormacion { get; set; }
        public string? Profesion { get; set; }
        public string? Empresa { get; set; }
        public string? EstadoCivil { get; set; }
        public string? TelefonoFamiliar { get; set; }
        public string? NombreFamiliar { get; set; }
        public string? Parentesco { get; set; }
        public string? TelefonoTrabajo { get; set; }
        public string? TelefonoTrabajoAnexo { get; set; }
        public string? Genero { get; set; }
        public string? Skype { get; set; }
        public string? Fax { get; set; }
        public int? IdPais { get; set; }
        public string? UbigeoPais { get; set; }
        public string? UbigeoDepartamento { get; set; }
        public string? UbigeoProvincia { get; set; }
        public string? UbigeoCiudad { get; set; }
        public string? UbigeoDistrito { get; set; }
        public string? DireccionCalle { get; set; }
        public string? DireccionAv { get; set; }
        public string? DireccionZona { get; set; }
        public string? DireccionComp { get; set; }
        public string? DireccionTorre { get; set; }
        public string? DireccionEdificio { get; set; }
        public string? DireccionDpto { get; set; }
        public string? DireccionUrb { get; set; }
        public string? DireccionMz { get; set; }
        public string? DireccionLt { get; set; }
        public string? ReferenciaDetallada { get; set; }
        public string? HoraMaxima { get; set; }
        public string? Puesto { get; set; }
        public string? AniversarioBodas { get; set; }
        public string? NroHijo { get; set; }
        public bool? ValidacionTelefonica { get; set; }
        public string? FaseContacto { get; set; }
        public int? IdCargo { get; set; }
        public string? Cargo { get; set; }
        public int? IdAformacion { get; set; }
        public string? Aformacion { get; set; }
        public int? IdAtrabajo { get; set; }
        public string? Atrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string? Industria { get; set; }
        public int? IdReferido { get; set; }
        public string? Referido { get; set; }
        public int? IdCodigoPais { get; set; }
        public string? NombrePais { get; set; }
        public int? IdCiudad { get; set; }
        public string? NombreCiudad { get; set; }
        public string? HoraContacto { get; set; }
        public string? HoraPeru { get; set; }
        public int? IdCodigoRegionCiudad { get; set; }
        public string? Telefono2 { get; set; }
        public string? Celular2 { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdOportunidadInicial { get; set; }
        public string? UsClave { get; set; }
        public Guid? IdTipoDocumento { get; set; }
        public string? NroDocumento { get; set; }
        public string? DescripcionCargo { get; set; }
        public bool? Asociado { get; set; }
        public bool? DeSuscrito { get; set; }
        public int? NroOportunidades { get; set; }
        public bool? EsPersonaValida { get; set; }
        public bool? EsEliminadoPorRegularizacion { get; set; }
        public bool? TieneOportunidad { get; set; }
        public bool? TieneMatricula { get; set; }
        public bool? EsRepetido { get; set; }
        public int? IdEstadoContactoWhatsApp { get; set; }
        public int? IdEstadoContactoMailing { get; set; }
        public string? DireccionEnvioCertificado { get; set; }
        public bool? UsarNuevaDireccionParaEnvio { get; set; }
        public string? CiudadEnvioCertificado { get; set; }
        public int? IdEstadoContactoWhatsAppSecundario { get; set; }
        public int? CodigoPortal { get; set; }
        public int? IdNumeroTipoDocumento { get; set; }
        public int? IdGenero { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string NroWhatsAppCoordinador { get; set; }
        public string NroTelefonoCoordinador { get; set; }
        public string FormaPago { get; set; }
        public string NombrePaisOrigen { get; set; }
        public string NombreCiudadOrigen { get; set; }
        public string NroCelularCompleto { get; set; }
        public string NroCelularSecundarioCompleto { get; set; }
        public string NombreCompleto { get; set; }
        public string? Comentario { get; set; }
    }
    public class AlumnoComboDTO
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
    }
    public class AlumnoCiudadPaisDTO
    {
        public int? IdCiudad { get; set; }
        public string? IdCodigoPais { get; set; }
    }
    public class AlumnoDatosDocumentoDTO
    {
        public int Id { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Direccion { get; set; }
        public string? Dni { get; set; }
        public string? Celular { get; set; }
        public string? Telefono { get; set; }
        public int? IdCiudad { get; set; }
        public string? NombreCiudad { get; set; }
        public int? IdCodigoPais { get; set; }
        public string? NombrePais { get; set; }
        public string? Correo { get; set; }
        // Campos Adicionales
        public string? Paquete { get; set; }
        public int? IdOportunidad { get; set; }
    }
    public class AlumnoInformacionDTO
    {
        public int IdClasificacionPersona { get; set; }
        public int Id { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string DNI { get; set; } = "";
        public string? Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? CelularOriginal { get; set; }
        public string? Celular { get; set; }
        public string? EmailOriginal { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public string? Genero { get; set; }
        public string? Parentesco { get; set; }
        public string? NombreFamiliar { get; set; }
        public string? TelefonoFamiliar { get; set; }
        public string? Empresa { get; set; }
        public int? IdCargo { get; set; } = 11;
        public string? Cargo { get; set; }
        public int? IdAFormacion { get; set; }
        public string? AFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public string? ATrabajo { get; set; }
        public int? IdIndustria { get; set; } = 48;
        public string? Industria { get; set; }
        public int? IdReferido { get; set; }
        public string? Referido { get; set; }
        public int? IdCodigoPais { get; set; }
        public string? NombrePais { get; set; }
        public int? IdCiudad { get; set; }
        public string? NombreCiudad { get; set; }
        public string? HoraContacto { get; set; }
        public string? HoraPeru { get; set; }
        public string? Telefono2 { get; set; }
        public string? Celular2 { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdEstadoContactoWhatsApp { get; set; }
        public int? IdEstadoContactoWhatsAppSecundario { get; set; }
        public int? IdOportunidad_Inicial { get; set; }
        public Guid? IdTipoDocumento { get; set; }
        public string? NroDocumento { get; set; }
        public string? DescripcionCargo { get; set; }
        public bool? Asociado { get; set; }
        public string? RutaBandera { get; set; }
        public string? CodigoMatricula { get; set; }
        public string? Municipio { get; set; }
        public int? IdMunicipioMexico { get; set; }
        public string? EstadoLugar { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Colonia { get; set; }
        public int? IdAsentamientoMexico { get; set; }
        public int? IdCiudadMexico { get; set; }
        public string? Curp { get; set; }
        public string? Rfc { get; set; }
        public string? PrincipalResponsabilidadProfesional { get; set; }
        public int? IdTiempoExperiencia { get; set; }
        public string? TiempoExperiencia { get; set; }
        public int? IdTamanioEmpresa { get; set; }
        public string? TamanioEmpresa { get; set; }
        public string? Modalidad { get; set; }

    }
    public class AlumnoAccesosDTO
    {
        public string id { get; set; }
        public string usuario { get; set; }
        public string contrasenia { get; set; }

    }
    public class DatosCorbranzaAlumnoDTO
    {
        public int? id { get; set; }
        public int? CuotasAtrasadas { get; set; }
        public decimal? TotalDolares { get; set; }
        public int? CompromisosVencidos { get; set; }
        public string FechaUltimoCompromiso { get; set; }
        public string ProximaFechaVencimiento { get; set; }
        public decimal? UltimoCompromisoMontoDolares { get; set; }
        public int? IdCronogramaPagoDetalleFinal { get; set; }
        public string EstadoCompromiso { get; set; }
        public int? CuotaPendiente { get; set; }


    }
    public class AvanceAonlineAlumnoDTO
    {
        public string? FechaMatricula { get; set; }
        public string? FechaPrimerAcceso { get; set; }
        public string? FechaUltimoAcceso { get; set; }
        public string? FechaFinalizacion { get; set; }
        public int IdPGeneral { get; set; }
        public string? PGeneral { get; set; }
        public string? IdPespecifico { get; set; }
        public string? PEspecifico { get; set; }
        public string? IdCentroCosto { get; set; }
        public string? CentroCosto { get; set; }
        public int? Porcentaje { get; set; }
        public int? PorcentajeProgramado { get; set; }
        public int? EsperadoSemanalHoras { get; set; }
        public decimal? RealUltimaSemanaHoras { get; set; }
        public decimal? RealUltimas2SemanasHoras { get; set; }
        public decimal? RealUltimas4SemanasHoras { get; set; }
    }
    public class AvanceOnlineAlumnoDTO
    {
        public string? FechaMatricula { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
        public int? SesionesDictadas { get; set; }
        public int? SesionesAsistidasActuales { get; set; }
        public decimal? TasaAsistencia { get; set; }
        public int? TotalClases { get; set; }
        public string? UltimaAsistencia { get; set; }
        public string? FechaUltimaSesionDictada { get; set; }
        public string? FechaProximaSesion { get; set; }

    }

    public class AlumnoPorCelularDTO
    {
        public int Id { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Celular { get; set; }
        public string? Email1 { get; set; }
    }
    public class EnvioSMSOportunidad
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
    }
    public class AlumnoEmailDTO
    {
        public int Id { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
    }
    public class AlumnoEmailPrincipalDTO
    {
        public int Id { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public bool Estado { get; set; }
        public bool EstadoCP { get; set; }
        public bool EstadoPer { get; set; }
        public int IdClasificacionPersona { get; set; }
        public int IdPersona { get; set; }
    }
    public class FlagActualizarCorreoDTO
    {
        public bool Valor { get; set; }
        public string Caso { get; set; }
        public int IdAlumno1 { get; set; }
        public int IdAlumno2 { get; set; }
    }
    public class FlagReasignacionDTO
    {
        public bool Valor { get; set; }
        public string Caso { get; set; }
        public int IdClasificacionPersonaPrincipal { get; set; }
        public int IdClasificacionPersonaSecundario { get; set; }
    }
    public class AlumnoCuponDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public string CodigoCupon { get; set; }
    }
    public class AlumnoActualizarEmailPrincipalDTO
    {
        public int IdAlumno { get; set; }
        public string EmailAPrincipal { get; set; }
    }
    public class AlumnoHoraDTO
    {
        public string HoraContacto { get; set; }
        public string HoraPeru { get; set; }
        public string Usuario { get; set; }
    }
    public class AlumnoFormularioOportunidadDTO
    {
        public int Id { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? DNI { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdReferido { get; set; }
        public int? IdCodigoPais { get; set; }
        public int? IdCodigoCiudad { get; set; }
        public string? HoraContacto { get; set; }
        public string? HoraPeru { get; set; }
        public string? Telefono2 { get; set; }
        public string? Celular2 { get; set; }
        public int? IdEmpresa { get; set; }
        public string? Comentario { get; set; }
    }
    public class AlumnoAutocompleteEmailDTO
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
    }
    public class AlumnoInformacionMessengerDTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int? IdReferido { get; set; }
        public int? IdCodigoPais { get; set; }
        public int? IdCiudad { get; set; }
        public string HoraContacto { get; set; }
        public string HoraPeru { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdEmpresa { get; set; }
        public bool? Asociado { get; set; }
    }
    public class AlumnoValidarEmailDTO
    {
        public int Id { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public int? IdCodigoPais { get; set; }
        public int? IdCodigoRegionCiudad { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdCargo { get; set; }
    }
    public class ObtenerSeguimientoAlumnoComentarioDTO
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int IdSeguimientoAlumnoCategoria { get; set; }
        public string SeguimientoAlumnoCategoria { get; set; }
        public int IdPersonal { get; set; }
        public int IdOportunidad { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
    public class ObtenerSeguimientoPagosAlumnoComentarioDTO
    {
        public DateTime? Fecha { get; set; }
        public DateTime? HoraCreacionMaxima { get; set; }
        public string ComentariosTipoPago { get; set; }
        public string ComentariosTipoAcademico { get; set; }
        public string UsuarioCreacion { get; set; }
    }
    public class ObtenerSeguimientoPagosAlumnoComentarioDosDTO
    {

        public string Comentario { get; set; }
        public string tipoCategoria { get; set; }
        public int IdTipoSeguimientoAlumnoCategoria { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
    public class AlumnoProgramaEspecificoDTO
    {
        public int IdPEspecifico { get; set; }
        public string CodigoMatricula { get; set; }
        public string PEspecifico { get; set; }
        public string NombreCompleto { get; set; }
        public int? IdAlumno { get; set; }
        public string Dni { get; set; }
        public string NombresAlumno { get; set; }
        public string ApellidosAlumno { get; set; }
        public string DireccionAlumno { get; set; }
        public string CelularAlumno { get; set; }
        public int IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public string CorreoAlumno { get; set; }
        public string FechaMatricula { get; set; }
        public string NombreCentroCosto { get; set; }
    }
    public class EmailPersonalAlumnoDTO
    {
        public string EmailPersonal { get; set; }
        public string EmailAlumno { get; set; }
    }
    public class AlumnoComprobanteDTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Pais { get; set; }
        public int? Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string NivelFormacion { get; set; }
        public string Profesion { get; set; }
        public string Empresa { get; set; }
        public string EstadoCivil { get; set; }
        public string TelefonoFamiliar { get; set; }
        public string NombreFamiliar { get; set; }
        public string Parentesco { get; set; }
        public string TelefonoTrabajo { get; set; }
        public string TelefonoTrabajoAnexo { get; set; }
        public string Genero { get; set; }
        public string Skype { get; set; }
        public string Fax { get; set; }
        public int? IdPais { get; set; }
        public string UbigeoPais { get; set; }
        public string UbigeoDepartamento { get; set; }
        public string UbigeoProvincia { get; set; }
        public string UbigeoCiudad { get; set; }
        public string UbigeoDistrito { get; set; }
        public string DireccionCalle { get; set; }
        public string DireccionAv { get; set; }
        public string DireccionZona { get; set; }
        public string DireccionComp { get; set; }
        public string DireccionTorre { get; set; }
        public string DireccionEdificio { get; set; }
        public string DireccionDpto { get; set; }
        public string DireccionUrb { get; set; }
        public string DireccionMz { get; set; }
        public string DireccionLt { get; set; }
        public string ReferenciaDetallada { get; set; }
        public string HoraMaxima { get; set; }
        public string Puesto { get; set; }
        public string AniversarioBodas { get; set; }
        public string NroHijo { get; set; }
        public bool? ValidacionTelefonica { get; set; }
        public string FaseContacto { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public int? IdAformacion { get; set; }
        public string Aformacion { get; set; }
        public int? IdAtrabajo { get; set; }
        public string Atrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string Industria { get; set; }
        public int? IdReferido { get; set; }
        public string Referido { get; set; }
        public int IdCodigoPais { get; set; }
        public string NombrePais { get; set; }
        public int? IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public string HoraContacto { get; set; }
        public string HoraPeru { get; set; }
        public int? IdCodigoRegionCiudad { get; set; }
        public string Telefono2 { get; set; }
        public string Celular2 { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdOportunidadInicial { get; set; }
        public string UsClave { get; set; }
        public Guid? IdTipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string DescripcionCargo { get; set; }
        public bool? Asociado { get; set; }
        public bool? DeSuscrito { get; set; }
    }

    public class AlumnoFiltroAutocompleteDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
    }
    public class AlumnoReferidosDTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string HoraPeru { get; set; }
    }


    public class AlumnoMatriculaDTO
    {
        public int CodigoAlumno { get; set; }
        public string CodigoProgramaEspecifico { get; set; }
        public string NombreProgramaEspecifico { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public string FechaMatricula { get; set; }
        public string Estado { get; set; }

    }

    public class AlumnoActualizarCertificadoDTO
    {
        public int Id { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }

    }
    public class NombreCompletoAlumnoDTO
    {
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
    }
    public class InformacionAlumnoDTO
    {
        public string? Tab { get; set; }
        public string? Alumno { get; set; }
        public string? FaseOportunidad { get; set; }
    }
    public class CredencialesPortalWebAlumnoDTO
    {
        public string PortalWebUsuario { get; set; }
        public string PortalWebClave { get; set; }
    }
}