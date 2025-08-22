using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public partial class SuscripcionProgramaGeneral : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? OrdenBeneficio { get; set; }
    }
}
