
using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ProgramaGeneralMaterialEstudioAdicionalEspecifico: BaseIntegraEntity
    {
        public int MaterialEstudioAdicionalPorPgeneralId { get; set; }
        public int? IdPespecifico { get; set; }
    }
}
