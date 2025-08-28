
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class AsignacionPreguntaExamenDTO
    {
        public int Id { get; set; }
        public int IdExamen { get; set; }
        public int IdPregunta { get; set; }
        public int? NroOrden { get; set; }
        public int? Puntaje { get; set; }
    }
}
