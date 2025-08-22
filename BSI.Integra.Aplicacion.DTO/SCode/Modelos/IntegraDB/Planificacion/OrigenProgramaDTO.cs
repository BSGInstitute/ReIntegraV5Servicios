using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class OrigenProgramaDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; }
    }
}
