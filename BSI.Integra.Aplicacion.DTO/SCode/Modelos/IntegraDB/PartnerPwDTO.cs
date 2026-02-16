using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>
    /// DTO para respuesta y datos generales de Partner
    /// </summary>
    public class PartnerPwDTO
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        [StringLength(150)]
        public string? ImgPrincipal { get; set; }
        [StringLength(150)]
        public string? ImgPrincipalAlf { get; set; }
        [StringLength(150)]
        public string? ImgSecundaria { get; set; }
        [StringLength(150)]
        public string? ImgSecundariaAlf { get; set; }
        public string? Descripcion { get; set; }
        [StringLength(500)]
        public string? DescripcionCorta { get; set; }
        public string? Preguntas { get; set; }
        public int Posicion { get; set; }
        public short? IdPartner { get; set; }
        public string? EncabezadoCorreoPartner { get; set; }
        public List<PartnerBeneficioPwDTO> Beneficios { get; set; }
        public List<PartnerContactoPwDTO> Contactos { get; set; }

        // Nuevos campos
        [StringLength(500)]
        public string? PaginaLink { get; set; }
        [StringLength(150)]
        public string? CertificadoLogo { get; set; }
        [StringLength(150)]
        public string? CertificadoBSG { get; set; }

        // URLs completas construidas (solo para respuesta/lectura)
        public string? UrlCertificadoLogo { get; set; }
        public string? UrlCertificadoBSG { get; set; }
    }

    /// <summary>
    /// DTO para entrada de datos con archivos (usado con [FromForm])
    /// </summary>
    public class PartnerPwEntradaDTO
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        [StringLength(150)]
        public string? ImgPrincipal { get; set; }
        [StringLength(150)]
        public string? ImgPrincipalAlf { get; set; }
        [StringLength(150)]
        public string? ImgSecundaria { get; set; }
        [StringLength(150)]
        public string? ImgSecundariaAlf { get; set; }
        public string? Descripcion { get; set; }
        [StringLength(500)]
        public string? DescripcionCorta { get; set; }
        public string? Preguntas { get; set; }
        public int Posicion { get; set; }
        public short? IdPartner { get; set; }
        public string? EncabezadoCorreoPartner { get; set; }

        // Nuevos campos
        [StringLength(500)]
        public string? PaginaLink { get; set; }

        // Archivos para subir al blob storage
        public IFormFile? ArchivoCertificadoLogo { get; set; }
        public IFormFile? ArchivoCertificadoBSG { get; set; }

        // Beneficios y Contactos como JSON string (se deserializará en el controller)
        public string? BeneficiosJson { get; set; }
        public string? ContactosJson { get; set; }
    }
}
