using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PostulanteComparacion : BaseIntegraEntity
    {
        public int? IdPostulante { get; set; }
        public int? IdGrupoComparacionProcesoSeleccion { get; set; }
    }
}
