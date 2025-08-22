namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class FeriadoDTO
    {
        public int Id { get; set; }
        public int? Tipo { get; set; }
        public DateTime Dia { get; set; }
        public string Motivo { get; set; } = null!;
        public int Frecuencia { get; set; }
        public int IdTroncalCiudad { get; set; }
    }
}