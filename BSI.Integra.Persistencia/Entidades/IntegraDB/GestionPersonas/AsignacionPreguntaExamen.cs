using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class AsignacionPreguntaExamen : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int? IdExamen { get; set; }
        public int  IdPregunta { get; set; }
        public int? NroOrden { get; set; }
        public int? Puntaje { get; set; }
    }
}
