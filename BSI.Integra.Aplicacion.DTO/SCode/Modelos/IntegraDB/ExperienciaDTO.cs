namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class ExperienciaComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdAreaTrabajo { get; set; }
    }
    public class ExperienciaRecibidoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdAreaTrabajo { get; set; }
    }
}
