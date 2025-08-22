using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ReprogramacionCabecera : BaseIntegraEntity
    {
        public int IdActividadCabecera { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int MaxReproPorDia { get; set; }
        public int IntervaloSigProgramacionMin { get; set; }
    }
}
