using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class GrupoComparacionProcesoSeleccion
 : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public int? IdPuestoTrabajo { get; set; }
        public int? IdSedeTrabajo { get; set; }
    }
}
