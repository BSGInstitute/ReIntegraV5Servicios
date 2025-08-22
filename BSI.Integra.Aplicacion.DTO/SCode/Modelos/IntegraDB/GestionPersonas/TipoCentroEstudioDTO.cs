using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class TipoCentroEstudioDTO
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public string Nombre { get; set; } = null!;
    }
}
