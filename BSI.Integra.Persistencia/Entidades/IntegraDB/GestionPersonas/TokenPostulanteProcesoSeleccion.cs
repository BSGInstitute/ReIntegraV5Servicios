using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class TokenPostulanteProcesoSeleccion : BaseIntegraEntity
    {
        public int IdPostulanteProcesoSeleccion { get; set; }
        public string Token { get; set; } = null!;
        public string TokenHash { get; set; } = null!;
        public Guid GuidAccess { get; set; }
        public bool Activo { get; set; }
        public int? IdMigracion { get; set; }
        public DateTime? FechaEnvioAccesos { get; set; }
    }
}
