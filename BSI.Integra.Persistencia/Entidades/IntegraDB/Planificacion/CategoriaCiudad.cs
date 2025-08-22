using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class CategoriaCiudad : BaseIntegraEntity
    {
        public int IdCategoriaPrograma { get; set; }
        public int? IdCiudad { get; set; }
        public string TroncalCompleto { get; set; } = null!;
        public int IdRegionCiudad { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
