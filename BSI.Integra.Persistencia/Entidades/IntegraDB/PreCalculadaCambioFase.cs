using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PreCalculadaCambioFase : BaseIntegraEntity
    {
        public int? IdPersonal { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdFaseOportunidadOrigen { get; set; }
        public int? IdFaseOportunidadDestino { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdCampania { get; set; }
        public int Contador { get; set; }
    }
}
