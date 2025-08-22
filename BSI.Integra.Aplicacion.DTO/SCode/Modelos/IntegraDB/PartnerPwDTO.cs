using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
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
    }
}
