using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class EstadoPespecificoDTO
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Nombre { get; set; }
    }
}
