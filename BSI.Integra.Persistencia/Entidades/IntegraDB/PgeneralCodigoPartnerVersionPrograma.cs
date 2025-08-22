using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public partial class PgeneralCodigoPartnerVersionPrograma : BaseIntegraEntity
    {
        public int IdPgeneralCodigoPartner { get; set; }
        public int? IdVersionPrograma { get; set; }
        public int IdMigracion { get; set; }
    }
}
