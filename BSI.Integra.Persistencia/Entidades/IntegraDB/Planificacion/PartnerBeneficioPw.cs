using BSI.Integra.Aplicacion.Base;
namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PartnerBeneficioPw : BaseIntegraEntity
    {
        public int IdPartner { get; set; }
        public string Descripcion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
    }
}
