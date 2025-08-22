namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class FlujoActividadDTO
    {
        public int Id { get; set; }
        public int IdFlujoFase { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; } = null!;
    }
}