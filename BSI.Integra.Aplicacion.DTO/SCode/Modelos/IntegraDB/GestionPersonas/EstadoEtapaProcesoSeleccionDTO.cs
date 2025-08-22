using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class EstadoEtapaProcesoSeleccionDTO
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El valor de Id debe ser mayor o igual que 0.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }
    }
}
