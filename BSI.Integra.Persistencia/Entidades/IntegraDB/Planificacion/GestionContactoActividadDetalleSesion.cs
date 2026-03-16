using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class GestionContactoActividadDetalleSesion : BaseIntegraEntity
    {
        public int IdGestionDocenteActividadDetalle { get; set; }
        public int IdGestionDocenteSesion { get; set; }
    }
}
