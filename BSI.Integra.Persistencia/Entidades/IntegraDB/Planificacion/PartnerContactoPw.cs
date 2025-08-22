using BSI.Integra.Aplicacion.Base;
namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PartnerContactoPw : BaseIntegraEntity
    {
        public int IdPartner { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Email1 { get; set; } = null!;
        public string? Email2 { get; set; }
        public string Telefono1 { get; set; } = null!;
        public string? Telefono2 { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
