namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class FeriadoConPaisDTO
    {
        public int Id { get; set; }
        public int? Tipo { get; set; }
        public DateTime Dia { get; set; }
        public string Motivo { get; set; } = null!;
        public int Frecuencia { get; set; }
        public int IdTroncalCiudad { get; set; }
        public int IdTroncalPais { get; set; }
    }
}
