using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProveedorCuentasDTO
    {
        [Required]
        public List<ProveedorCuentaBancoEnvioDTO> listaCuentaBanco { get; set; } = null!;
        [Required]
        public ProveedorWEnvioDTO proveedor { get; set; } = null!;

    }


}
