using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoGrupoComparacion : BaseIntegraEntity
    {
        public int IdPuestoTrabajo { get; set; }
        public int IdGrupoComparacionProcesoSeleccion { get; set; }
    }
}
