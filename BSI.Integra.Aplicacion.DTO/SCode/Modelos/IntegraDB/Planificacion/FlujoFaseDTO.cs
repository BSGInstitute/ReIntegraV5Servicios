namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class FlujoFaseDTO
    {
        public int Id { get; set; }
        public int IdFlujo { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; } = null!;
    }
}