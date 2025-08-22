using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{

    public class EscalaCalificacionDetalleDTO
    {
        public int Id { get; set; }
        public int IdEscalaCalificacion { get; set; }
        public string? Nombre { get; set; }
        public decimal Valor { get; set; }
    }
}
