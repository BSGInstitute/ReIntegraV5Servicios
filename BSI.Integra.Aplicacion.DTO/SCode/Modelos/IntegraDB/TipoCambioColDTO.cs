using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TipoCambioColDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double PesosDolares { get; set; }
        [Required]
        public double DolaresPesos { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int IdMoneda { get; set; }
        [Required]
        public string NombreUsuario { get; set; }

    }

    public class TipoCambioColombiaDTO
    {
        public double PesosDolares { get; set; }
    }
}
