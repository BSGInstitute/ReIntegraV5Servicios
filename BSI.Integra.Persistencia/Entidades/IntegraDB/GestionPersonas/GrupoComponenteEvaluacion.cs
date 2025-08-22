using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class GrupoComponenteEvaluacion : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string? NombreAbreviado { get; set; }
        public int? IdFormulaPuntaje { get; set; }
        public bool RequiereCentil { get; set; }
        public int? IdMigracion { get; set; }
        public decimal? Factor { get; set; }
    }
}
