using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class BeneficioLaboralPorPeriodo : BaseIntegraEntity
    {
        public int IdAgendaTipoUsuario { get; set; }
        public int IdPeriodo { get; set; }
        public int IdBeneficioLaboralTipo { get; set; }
        public decimal Monto { get; set; }
    }
}
