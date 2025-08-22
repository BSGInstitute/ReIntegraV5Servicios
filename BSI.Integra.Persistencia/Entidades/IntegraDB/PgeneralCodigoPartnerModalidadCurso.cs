using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public partial class PgeneralCodigoPartnerModalidadCurso : BaseIntegraEntity
    {

        public int IdPgeneralCodigoPartner { get; set; }

        public int IdModalidadCurso { get; set; } 

        public virtual TModalidadCurso IdModalidadCursoNavigation { get; set; } = null!;
        public virtual TPgeneralCodigoPartner IdPgeneralCodigoPartnerNavigation { get; set; } = null!;
    }
}
