using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using Org.BouncyCastle.Asn1.Crmf;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PersonalDTO
    {
        public int? Id { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Rol { get; set; }
        public string? TipoPersonal { get; set; }
        public string? Email { get; set; }
        public string? AreaAbrev { get; set; }
        public string? Anexo { get; set; }
        public int? IdJefe { get; set; }
        public string? Central { get; set; }
        public bool? Activo { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int? IdSexo { get; set; }
        public int? IdEstadocivil { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdRegion { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? AutogeneradoEssalud { get; set; }
        public int? IdTipoSangre { get; set; }
        public string? UrlFirmaCorreos { get; set; }
        public int? IdGrupoProgramasCriticos { get; set; }
        public int? IdCerrador { get; set; }
        public bool? EsCerrador { get; set; }
        public int? IdPaisDireccion { get; set; }
        public int? IdRegionDireccion { get; set; }
        public string? CiudadDireccion { get; set; }
        public string? NombreDireccion { get; set; }
        public string? FijoReferencia { get; set; }
        public string? MovilReferencia { get; set; }
        public string? EmailReferencia { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string? NombreCuspp { get; set; }
        public string? DistritoDireccion { get; set; }
        public bool? ConEssalud { get; set; }
        public int? IdBusqueda { get; set; }
        public string? AliasEmailAsesor { get; set; }
        public string? Anexo3Cx { get; set; }
        public string? Id3Cx { get; set; }
        public string? Password3Cx { get; set; }
        public int? IdDominioPbx { get; set; }
        public string? Dominio { get; set; }
        public long? IdFacebookPersonal { get; set; }
        public bool? Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool? EsPersonaValida { get; set; }
        public string? UrlFoto { get; set; }
        public bool? AplicaFirmaHtml { get; set; }
        public string? FirmaHtml { get; set; }
        public string? CargoFirmaHtml { get; set; }
        public int? IdPostulante { get; set; }
        public int? UsuarioAsterisk { get; set; }
        public string? ContrasenaAsterisk { get; set; }
        public int? IdTableroComercialCategoriaAsesor { get; set; }
        public int? IdPuestoTrabajoNivel { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdPersonalArchivo { get; set; }
        public int? IdRolUsuarioTicket { get; set; }
        public bool? DiscadorActivo { get; set; }
        public decimal? DiferenciaHoraria { get; set; }
        public int?  CodigoPaisDiferenciaHoraria { get; set; }
    }
    public class PersonalComboDTO
    {
        public int Id { get; set; }
        public string? Nombres { get; set; }
    }
    
    public class PersonalComboAreaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
    
    public class PersonalAsignadoDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public string Usuario { get; set; }
        public string TipoPersonal { get; set; }
        public string NivelVisualizacionAgenda { get; set; }
        public string AreaAbrev { get; set; }
    }

    public class ReportePersonalDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public bool? Activo { get; set; }
        public bool? Estado { get; set; }
        public int? IdJefe { get; set; }
        public string TipoPersonal { get; set; }
    }
    public class PersonalAutocompleteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class PersonalDatosAgendaDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public string TipoPersonal { get; set; }
        public string Email { get; set; }
        public string AreaAbrev { get; set; }
        public string Anexo { get; set; }
        public int? IdJefe { get; set; }
        public string Central { get; set; }
        public string Anexo3Cx { get; set; }
        public string Id3Cx { get; set; }
        public string Password3Cx { get; set; }
        public string Dominio { get; set; }
        public Nullable<int> UsuarioAsterisk { get; set; }
        public string ContrasenaAsterisk { get; set; }
        public int IdAsterisk { get; set; }

        //nuevosvalores avatar
        public string Top { get; set; }
        public string Accessories { get; set; }
        public string HairColor { get; set; }
        public string FacialHair { get; set; }
        public string FacialHairColor { get; set; }
        public string Clothes { get; set; }
        public string ClothesColor { get; set; }
        public string Eyes { get; set; }
        public string Eyesbrow { get; set; }
        public string Mouth { get; set; }
        public string Skin { get; set; }
        public int IdSexo { get; set; }
        public int? IdAvatar { get; set; }

    }
    public class DatosPersonalAsesorPorGrupoIdDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Gmail { get; set; }
        public string NombreCompleto { get; set; }
        public bool asignado { get; set; }
        public int? IdAsesor { get; set; }
        public string Rol { get; set; }
        public int? IdGrupo { get; set; }
        public bool Estado { get; set; }
    }
    public class DatoCompletoPersonalDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Anexo3Cx { get; set; }
        public string Central { get; set; }
        public string Email { get; set; }
        public string MovilReferencia { get; set; }
        public string PrimerNombreApellidoPaterno { get; set; }
        public string Nombre1 { get; set; }
        public int? CodigoPaisDiferenciaHoraria { get; set; }
        public int? DiferenciaHoraria { get; set; }

    }
    public class PersonalConfiguracionOpenVoxDTO
    {
        public int IdPais { get; set; }
        public string Prefijo { get; set; }
        public string Anexo { get; set; }
    }
    public class PersonalAlumnoDTO
    {
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
    }
    public class PersonalActivoEmailDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
    }
    public class AsesorNombreFiltroDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public bool? Activo { get; set; }
        public bool? Estado { get; set; }
        public int? IdJefe { get; set; }
    }
    public class PersonalAsignadoReportePendienteDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public string Usuario { get; set; }
        public string TipoPersonal { get; set; }
    }
    public class TipoPersonalDTO
    {
        public int Id { get; set; }
        public string TipoPersonal { get; set; }
    }
    public class PersonalComboAprobadoDTO
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
    }
    public class PersonalNuevaContraseniaDTO
    {
        public string Usuario { get; set; }
        public string NuevaContrasena { get; set; }
    }

    public class RegistrosMarcacionPersonal
    {
        public string? dni { get; set; }
        public DateTime? fechaMarcacion { get; set; }
        public string? m1 { get; set; }
        public string? m2 { get; set; }
        public string? m3 { get; set; }
        public string? m4 { get; set; }
        public string? m5 { get; set; }
        public string? m6 { get; set; }
    }

    public class PersonalFichaDatosDTO
    {
        public int Id { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Rol { get; set; }
        public string? Email { get; set; }
        public bool? Activo { get; set; }
    }

    public class ComboFichaDatosPersonalDTO
    {
        public IEnumerable<CiudadDatosDTO> listaCiudad { get; set; }
        public IEnumerable<PaisMonedaComboDTO> listaPais { get; set; }
        public IEnumerable<EstadoCivilDTO> listaEstadoCivil { get; set; }
        public IEnumerable<ComboDTO> listaSexo { get; set; }
        public IEnumerable<SistemaPensionarioDTO> listaSistemaPensionario { get; set; }
        public IEnumerable<EntidadSistemaPensionarioDTO> listaEntidad { get; set; }
        public IEnumerable<TipoDocumentoPersonalDTO> listaTipoDocumento { get; set; }
        public IEnumerable<MotivoCeseDTO> listaMotivoCese { get; set; }

        public IEnumerable<EntidadSeguroSaludDTO> listaEntidadSeguroSalud { get; set; }

        public IEnumerable<CentroEstudioDTO> listaCentroEstudio { get; set; }
        public IEnumerable<TipoEstudioDTO> listaTipoEstudio { get; set; }
        public IEnumerable<ComboDTO> listaAreaFormacion { get; set; }

        public IEnumerable<GradoEstudioDTO> listaEstadoEstudio { get; set; }
        public IEnumerable<NivelEstudioDTO> listaNivelEstudio { get; set; }
        public IEnumerable<ComboDTO> listaIdioma { get; set; }
        public IEnumerable<NivelIdiomaDTO> listaNivelIdioma { get; set; }
        public IEnumerable<ComboDTO> listaEmpresa { get; set; }

        public IEnumerable<ComboDTO> listaAreaTrabajo { get; set; }
        public IEnumerable<ComboDTO> listaCargo { get; set; }
        public IEnumerable<ParentescoPersonalDTO> listaParentesco { get; set; }
        public IEnumerable<TipoSangreDTO> listaTipoSangre { get; set; }
        public IEnumerable<ComboDTO> listaPuestoTrabajo { get; set; }
        public IEnumerable<ComboDTO> listaSedeTrabajo { get; set; }
        public IEnumerable<PersonalAreaTrabajoDTO> listaPersonalAreaTrabajo { get; set; }
        public IEnumerable<ComboDTO> listaPersonal { get; set; }
        public IEnumerable<ComboDTO> listaPersonalAsesorAsociado { get; set; }
        public IEnumerable<TipoPagoRemuneracionDTO> listaTipoPagoRemuneracion { get; set; }
        public IEnumerable<EntidadFinancieraDTO> listaEntidadFinanciera { get; set; }
        public IEnumerable<ContratoEstadoDTO> listaContratoEstado { get; set; }
        public IEnumerable<ComboDTO> listaMotivoInactividad { get; set; }

        public IEnumerable<ComboDTO> listaPuestoTrabajoNivel { get; set; }

        public IEnumerable<TableroComercialCategoriaAsesorComboDTO> listaCategoriaAsesor { get; set; }
        public IEnumerable<NivelCompetenciaTecnicaDTO> listaNivelCompetenciaTecnica { get; set; }
    }

    public class PersonalWhatsAppDTO
    {
        public int Id { get; set; }
        public string? Rol { get; set; }
        public string? Email { get; set; }
        public string? AreaAbrev { get; set; }
        public string? Anexo { get; set; }
    }

    public class InduccionPersonalDTO
    {
        public DateTime FechaIncoorporacion { get; set; }
        public DateTime FechaRealizado { get; set; }
        public int IdSede { get; set; }
        public string NombreSede { get; set; }
        public int IdArea { get; set; }
        public string NombreArea { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string NombrePuestoTrabajo { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string NroDocumento { get; set; }
        public int IdPostulante { get; set; }
        public string NombrePostulante { get; set; }
        public int OrdenFilaSesion { get; set; }
        public double Calificacion { get; set; }

    }

    public class FiltroInduccionPersonalDTO
    {
        public List<int?> IdArea { get; set; }
        public List<int?> IdSede { get; set; }
        public List<int?> IdProceso { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

    }

    public class InduccionPersonalCalificacionAgrupadaDTO
    {
        public DateTime FechaIncoorporacion { get; set; }
        public DateTime FechaRealizado { get; set; }
        public int IdSede { get; set; }
        public string NombreSede { get; set; }
        public int IdArea { get; set; }
        public string NombreArea { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string NombrePuestoTrabajo { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string NroDocumento { get; set; }
        public int IdPostulante { get; set; }
        public string NombrePostulante { get; set; }
        public List<CursoCalificacion> IdCursoCalificacion { get; set; }
        public double PromedioGeneral { get; set; }
    }

    public class CursoCalificacion
    {
        public int OrdenFilaSesion { get; set; }
        public double Calificacion { get; set; }
    }


    public class MaestroPersonalPuestoSedeDTO
    {
        public int Id { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public string FijoReferencia { get; set; }
        public string MovilReferencia { get; set; }
        public string EmailReferencia { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdCiudad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPaisDireccion { get; set; }
        public int? IdRegionDireccion { get; set; }
        public string DistritoDireccion { get; set; }
        public string NombreDireccion { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdSexo { get; set; }
        public int? IdPuestoTrabajo { get; set; }
        public int? IdSedeTrabajo { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string CodigoAfiliado { get; set; }
        public int? IdEntidadSeguroSalud { get; set; }
        public string TipoPersonal { get; set; }
        //=======================================
        public string Email { get; set; }
        public int? IdJefe { get; set; }
        public string Central { get; set; }
        public string Anexo3CX { get; set; }
        public string UrlFirmaCorreos { get; set; }
        public bool Activo { get; set; }

        public int? IdTipoSangre { get; set; }
        public bool? EsCerrador { get; set; }
        public int? IdCerrador { get; set; }
        public int? IdPuestoTrabajoNivel { get; set; }
        public int? IdPersonalArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public string RutaArchivoHtml { get; set; }
        public bool? EsImagen { get; set; }
        public int? IdTableroComercialCategoriaAsesor { get; set; }
    }






    public class FiltroPersonalJefaturaFiltroDTO
    {
        public string? PersonalAreaTrabajo { get; set; }
        public string? Personal { get; set; }
        public string? PersonalPuestoTrabajo { get; set; }
        public int? PersonasACargo { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaInicioPuesto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaCese { get; set; }
        public string? JefeInmediato { get; set; }
        public string? PuestoJefeInmediato { get; set; }
    }

    public class FiltroPersonalJefaturaDTO
    {
        public List<int>? ListaPersonal { get; set; }
        public int? ListaAreaTrabajo { get; set; }
        public int? Estado { get; set; }
    }


    public class PersonalJefaturaIteradorDTO
    {
        public int? IdPersonal { get; set; }
        public string? Personal { get; set; }
        public string? PuestoTrabajo { get; set; }
        public List<PersonalJefaturaIteradorDTO>? PersonalACargo { get; set; }
    }
    public class PersonalJefaturaAgrupadoDTO
    {
        public int? IdJefeInmediato { get; set; }
        public List<PersonalJefaturaAsociadoDTO> PersonalACargo { get; set; }
    }
    public class PersonalJefaturaAsociadoDTO
    {
        public int? IdPersonal { get; set; }
        public string Personal { get; set; }
        public string PuestoTrabajo { get; set; }
    }
    public class PersonalJefaturaDTO
    {
        public int IdPersonal { get; set; }
        public string Personal { get; set; }
        public string PuestoTrabajo { get; set; }
        public int? IdJefeInmediato { get; set; }
    }
    //public class PersonalJefeInmediatoDTO
    //{
    //    public int Id { get; set; }
    //    public int IdJefe { get; set; }
    //    public string DatosJefe { get; set; }
    //    public DateTime? FechaInicio { get; set; }
    //    public DateTime? FechaFin { get; set; }
    //}
    //public class PersonalTipoAsesorDTO
    //{
    //    public int Id { get; set; }
    //    public int? IdCerrador { get; set; }
    //    public string AsesorAsociado { get; set; }
    //    public bool? EsCerrador { get; set; }
    //    public DateTime? FechaInicio { get; set; }
    //    public DateTime? FechaFin { get; set; }
    //}

    public class PersonalLogDTO
    {
        public int IdPersonal { get; set; }
        public string Rol { get; set; }
        public string TipoPersonal { get; set; }
        public int? IdJefe { get; set; }
        public bool EstadoRol { get; set; }
        public bool EstadoTipoPersonal { get; set; }
        public bool EstadoIdJefe { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCerrador { get; set; }
        public bool? EsCerrador { get; set; }
        public bool? EstadoCerrador { get; set; }
        public int? IdPuestoTrabajoNivel { get; set; }
    }
    public class EnvioCorreoValidacionAccesoDTO
    {
        public string Usuario { get; set; }
        public string EmailRemitente { get; set; }
        public string PersonalRemitente { get; set; }
        public string PasswordCorreo { get; set; }
    }

    public class PersonalDetalleDTO
    {
        public int? Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Area { get; set; }
        public string? AsesorCoordinador { get; set; }
        public string Email { get; set; }
        public string Anexo { get; set; }
        public string Central { get; set; }
        public string? Jefe { get; set; }
        public string AreaAbrev { get; set; }
        public int IdCentral { get; set; }
        public int IdJefe { get; set; }
        public int IdArea { get; set; }
        public bool? Estado { get; set; }
        public bool? Activo { get; set; }
        public string? Id3CX { get; set; }
        public string? Password3CX { get; set; }
        public string Dominio { get; set; }
        public int? IdDominioPbx { get; set; }
        public int? CodigoPaisDiferenciaHoraria { get; set; }
        public int? IdPais { get; set; }
        public int? IdGmailCliente { get; set; }
        public string? PasswordCorreo { get; set; }
        public int? UsuarioAsterisk { get; set; }
        public string? ContrasenaAsterisk { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string? Ip1 { get; set; }
        public string? Ip2 { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public PersonalHorarioDTO PersonalHorario { get; set; } = new PersonalHorarioDTO();
    }
    public class MacDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
    }

    public class ResultadoDTOv2
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
    }
}
