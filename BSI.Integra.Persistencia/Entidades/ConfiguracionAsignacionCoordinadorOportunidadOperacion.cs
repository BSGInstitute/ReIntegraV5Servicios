using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades
{
    public class ConfiguracionAsignacionCoordinadorOportunidadOperacion : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdCentroCosto { get; set; }
        public int? IdCentroCostoHijo { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public int? IdMigracion { get; set; }
    }
}
