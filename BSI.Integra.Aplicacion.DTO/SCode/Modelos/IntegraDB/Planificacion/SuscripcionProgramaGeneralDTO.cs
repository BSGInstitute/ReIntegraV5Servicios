
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class SuscripcionProgramaGeneralDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? OrdenBeneficio { get; set; }
    }
}
