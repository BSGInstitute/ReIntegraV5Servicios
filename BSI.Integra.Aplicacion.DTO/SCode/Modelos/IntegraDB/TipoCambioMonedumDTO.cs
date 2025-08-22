using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TipoCambioMonedumDTO
    {

        public int Id { get; set; }
        public string NombreMoneda { get; set; }
        public int IdMoneda { get; set; }
        public double DolarAMoneda { get; set; }
        public double MonedaADolar { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class TipoCambioMonedumActualDTO
    {
        public int Id { get; set; }
        public double DolarAMoneda { get; set; }
        public double MonedaADolar { get; set; }
    }

    public class FiltroTipoCambioMonedaDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double MonedaAdolar { get; set; }
        [Required]
        public double DolarAmoneda { get; set; }
        [Required]
        public DateTime? Fecha { get; set; }
        [Required]
        public int IdMoneda { get; set; }
        [Required]
        public int IdPeriodo { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
    }
}

    
