using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Personal : BaseIntegraEntity
    {
        public string? Nombres { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
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
        public string? Dominio { get; set; }
        public long? IdFacebookPersonal { get; set; }
        public int? IdMigracion { get; set; }
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
        public int? DiferenciaHoraria { get; set; }
        public int? IdDominioPbx { get; set; }
        public int? CodigoPaisDiferenciaHoraria { get; set; }
        public string? Ip1 { get; set; }
        public string? Ip2 { get; set; }

    }
}
