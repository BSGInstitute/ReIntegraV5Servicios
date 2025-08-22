using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class ProcesoSeleccionEtapa : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public int IdProcesoSeleccion { get; set; }
        public int? NroOrden { get; set; }
        public int? IdMigracion { get; set; }
    }

}
