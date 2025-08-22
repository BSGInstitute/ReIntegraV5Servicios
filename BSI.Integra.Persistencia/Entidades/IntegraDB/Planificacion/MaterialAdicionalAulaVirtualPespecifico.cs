
using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialAdicionalAulaVirtualPespecifico : BaseIntegraEntity
    {
        public int IdPespecifico { get; set; }
        public int? IdMaterialAdicionalAulaVirtual { get; set; }
    }
}
