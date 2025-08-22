using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PostulanteCursoPortalNotasHistorico : BaseIntegraEntity
    {
        public int IdPostulanteProcesoSeleccion { get; set; }
        public int IdPgeneral { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public int? OrdenFilaSesion { get; set; }
        public string? GrupoPregunta { get; set; }
        public decimal? Calificacion { get; set; }
        public string? IdUsuario { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPespecifico { get; set; }
        public bool? AccesoPrueba { get; set; }
        public int? IdMigracion { get; set; }
    }
}
