using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class WhatsAppUsuario : BaseIntegraEntity
    {
        public int? IdPersonal { get; set; }
        public string? RolUser { get; set; }
        public string? UserUsername { get; set; }
        public string? UserPassword { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
    }
}