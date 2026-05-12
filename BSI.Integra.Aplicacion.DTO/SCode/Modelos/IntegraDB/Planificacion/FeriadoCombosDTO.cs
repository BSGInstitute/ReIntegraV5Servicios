namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ComboTroncalCiudadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdTroncalPais { get; set; }
    }

    public class ComboTroncalPaisDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
