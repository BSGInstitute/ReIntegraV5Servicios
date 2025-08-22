using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class SedeTrabajoGrupoComparacion : BaseIntegraEntity
    {
        public int IdSedeTrabajo { get; set; }
        public int IdGrupoComparacionProcesoSeleccion { get; set; }
    }
}
