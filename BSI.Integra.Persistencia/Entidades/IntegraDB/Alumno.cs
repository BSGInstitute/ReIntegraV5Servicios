using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Alumno : BaseIntegraEntity
    {
        [StringLength(50)]
        public string? Nombre1 { get; set; }
        [StringLength(50)]
        public string? Nombre2 { get; set; }
        [StringLength(50)]
        public string? ApellidoPaterno { get; set; }
        [StringLength(50)]
        public string? ApellidoMaterno { get; set; }
        [StringLength(20)]
        public string? Dni { get; set; }
        [StringLength(1000)]
        public string? Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        [StringLength(50)]
        public string? Pais { get; set; }
        public int? Ciudad { get; set; }
        [StringLength(50)]
        public string? Telefono { get; set; }
        [StringLength(50)]
        public string? Celular { get; set; }
        [StringLength(500)]
        public string? Email1 { get; set; }
        [StringLength(500)]
        public string? Email2 { get; set; }
        [StringLength(500)]
        public string? NivelFormacion { get; set; }
        [StringLength(500)]
        public string? Profesion { get; set; }
        [StringLength(500)]
        public string? Empresa { get; set; }
        [StringLength(50)]
        public string? EstadoCivil { get; set; }
        [StringLength(30)]
        public string? TelefonoFamiliar { get; set; }
        [StringLength(100)]
        public string? NombreFamiliar { get; set; }
        [StringLength(200)]
        public string? Parentesco { get; set; }
        [StringLength(30)]
        public string? TelefonoTrabajo { get; set; }
        [StringLength(20)]
        public string? TelefonoTrabajoAnexo { get; set; }
        [StringLength(1)]
        public string? Genero { get; set; }
        [StringLength(100)]
        public string? Skype { get; set; }
        [StringLength(100)]
        public string? Fax { get; set; }
        public int? IdPais { get; set; }
        [StringLength(200)]
        public string? UbigeoPais { get; set; }
        [StringLength(200)]
        public string? UbigeoDepartamento { get; set; }
        [StringLength(200)]
        public string? UbigeoProvincia { get; set; }
        [StringLength(200)]
        public string? UbigeoCiudad { get; set; }
        [StringLength(200)]
        public string? UbigeoDistrito { get; set; }
        [StringLength(50)]
        public string? DireccionCalle { get; set; }
        [StringLength(50)]
        public string? DireccionAv { get; set; }
        [StringLength(50)]
        public string? DireccionZona { get; set; }
        [StringLength(50)]
        public string? DireccionComp { get; set; }
        [StringLength(50)]
        public string? DireccionTorre { get; set; }
        [StringLength(50)]
        public string? DireccionEdificio { get; set; }
        [StringLength(50)]
        public string? DireccionDpto { get; set; }
        [StringLength(50)]
        public string? DireccionUrb { get; set; }
        [StringLength(50)]
        public string? DireccionMz { get; set; }
        [StringLength(50)]
        public string? DireccionLt { get; set; }
        [StringLength(1000)]
        public string? ReferenciaDetallada { get; set; }
        [StringLength(20)]
        public string? HoraMaxima { get; set; }
        [StringLength(500)]
        public string? Puesto { get; set; }
        [StringLength(50)]
        public string? AniversarioBodas { get; set; }
        [StringLength(5)]
        public string? NroHijo { get; set; }
        public bool? ValidacionTelefonica { get; set; }
        [StringLength(1)]
        public string? FaseContacto { get; set; }
        public int? IdCargo { get; set; }
        [StringLength(50)]
        public string? Cargo { get; set; }
        public int? IdAformacion { get; set; }
        [StringLength(50)]
        public string? Aformacion { get; set; }
        public int? IdAtrabajo { get; set; }
        [StringLength(50)]
        public string? Atrabajo { get; set; }
        public int? IdIndustria { get; set; }
        [StringLength(50)]
        public string? Industria { get; set; }
        public int? IdReferido { get; set; }
        [StringLength(150)]
        public string? Referido { get; set; }
        public int? IdCodigoPais { get; set; }
        [StringLength(50)]
        public string? NombrePais { get; set; }
        public int? IdCiudad { get; set; }
        [StringLength(50)]
        public string? NombreCiudad { get; set; }
        [StringLength(20)]
        public string? HoraContacto { get; set; }
        [StringLength(20)]
        public string? HoraPeru { get; set; }
        public int? IdCodigoRegionCiudad { get; set; }
        [StringLength(50)]
        public string? Telefono2 { get; set; }
        [StringLength(50)]
        public string? Celular2 { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdOportunidadInicial { get; set; }
        [StringLength(100)]
        public string? UsClave { get; set; }
        public Guid? IdTipoDocumento { get; set; }
        [StringLength(25)]
        public string? NroDocumento { get; set; }
        [StringLength(250)]
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
        [StringLength(1000)]
        public string? DireccionEnvioCertificado { get; set; }
        public bool? UsarNuevaDireccionParaEnvio { get; set; }
        [StringLength(50)]
        public string? CiudadEnvioCertificado { get; set; }
        public int? IdEstadoContactoWhatsAppSecundario { get; set; }
        public int? CodigoPortal { get; set; }
        public int? IdNumeroTipoDocumento { get; set; }
        public int? IdGenero { get; set; }
        [StringLength(100)]
        public string? Comentario { get; set; }

        public string? Municipio { get; set; }
        public int? IdMunicipioMexico { get; set; }
        public string? EstadoLugar { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Colonia { get; set; }
        public int? IdAsentamientoMexico { get; set; }
        public int? IdCiudadMexico { get; set; }
        public string? Rfc { get; set; }
        public string? Curp { get; set; }
        public string? PrincipalResponsabilidadProfesional { get; set; }
        public int? IdExperiencia { get; set; }
        public int? IdTamanioEmpresaAgenda { get; set; }


    }
}
