using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProcedenciaVentaCruzadum : BaseIntegraEntity
    {
        public int? IdOportunidadInicial { get; set; }
        public int IdCentroCostoInicial { get; set; }
        public int? IdOportunidadActual { get; set; }
        public int IdCentroCostoActual { get; set; }
        public int? IdOportunidadNuevo { get; set; }
        public int IdCentroCostoNuevo { get; set; }
    }
}
