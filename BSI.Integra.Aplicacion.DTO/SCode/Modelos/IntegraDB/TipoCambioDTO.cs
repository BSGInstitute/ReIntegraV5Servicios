using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TipoCambioDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double SolesDolares { get; set; }
        [Required]
        public double DolaresSoles { get; set; }
        [Required]
        public DateTime? Fecha { get; set; }
        [Required]
        public int IdPeriodo { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
    }
    public class TipoCambioFiltroDTO
    {
        public int IdMoneda { get; set; }
        public DateTime? Fecha { get; set; }
    }
    public class TipoCambioObtenerDTO
    {
        public int Id { get; set; }
        public double SolesDolares { get; set; }
        public double DolaresSoles { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
