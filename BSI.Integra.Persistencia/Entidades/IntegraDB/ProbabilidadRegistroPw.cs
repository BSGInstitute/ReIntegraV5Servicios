using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProbabilidadRegistroPw : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public Guid IdCodigo { get; set; }
        public int Codigo { get; set; }
    }
}
