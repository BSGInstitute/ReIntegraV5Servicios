using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class GestionDocenteDisparadorReglaTiempoRelativoReferencia : BaseIntegraEntity
    {
        public int IdGestionDocenteDisparadorReglaTiempoRelativo { get; set; }
        public int IdGestionDocenteReferenciaTiempo { get; set; }
    }
}
