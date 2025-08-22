namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PartnerContactoPwDTO
    {
        public int Id { get; set; }
        public int IdPartner { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Email1 { get; set; } = null!;
        public string? Email2 { get; set; }
        public string Telefono1 { get; set; } = null!;
        public string? Telefono2 { get; set; }
    }
}