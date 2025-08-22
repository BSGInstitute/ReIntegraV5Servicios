using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class GradoEstudioDTO
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public string Nombre { get; set; } = null!;
    }
}
