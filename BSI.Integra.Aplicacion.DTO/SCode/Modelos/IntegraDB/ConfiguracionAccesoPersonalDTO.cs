namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionAccesoPersonalDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdPersonalAcceso { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public int IdModuloSistema { get; set; }
    }
}
