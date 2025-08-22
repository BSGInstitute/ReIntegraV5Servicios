namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion
{
    public class ConfiguracionIntegraDTO
    {
        public int Id { get; set; }
        public string Clave1 { get; set; }
        public string? Clave2 { get; set; }
        public string Valor1 { get; set; }
        public string? Valor2 { get; set; }
        public string? Descripcion { get; set; }
        public string? Tipo { get; set; }
        public string? SubTipo { get; set; }
        public string? ValorJson { get; set; }
        public DateTime? FechaExpira { get; set; }
        public int OrdenPrioridad { get; set; }
        public bool Activo { get; set; }
    }
}