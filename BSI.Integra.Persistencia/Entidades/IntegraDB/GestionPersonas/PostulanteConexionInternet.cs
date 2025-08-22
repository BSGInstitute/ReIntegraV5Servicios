using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PostulanteConexionInternet : BaseIntegraEntity
    {
        public int IdPostulante { get; set; }
        public string TipoConexion { get; set; } = null!;
        public string MedioConexion { get; set; } = null!;
        public string VelocidadInternet { get; set; } = null!;
        public string ProveedorInternet { get; set; } = null!;
        public decimal CostoInternet { get; set; }
        public string ConexionCompartida { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
