using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProveedorRucRazonSocialDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NroDocIdentidad { get; set; }
        [Required]
        public string RazonSocial { get; set; }
        [Required]
        public int? IdTipoImpuesto { get; set; }
        [Required]
        public int? IdDetraccion { get; set; }
        [Required]
        public int? IdRetencion { get; set; }
        [Required]
        public int? IdPais { get; set; }

    }


}
