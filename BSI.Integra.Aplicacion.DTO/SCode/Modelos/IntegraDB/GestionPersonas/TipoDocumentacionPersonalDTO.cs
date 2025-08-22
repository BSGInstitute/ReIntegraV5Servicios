
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class TipoDocumentacionPersonalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
