using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PuestoTrabajo : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public int? IdMigracion { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public ICollection<GrupoComparacionProcesoSeleccion> GrupoComparacionProcesoSeleccions { get; set; }
    }
}
