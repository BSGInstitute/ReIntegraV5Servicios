using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class GestionDocenteActividadCabeceraFlujo : BaseIntegraEntity
    {
        public int IdGestionDocenteFlujo { get; set; }
        public int IdGestionDocenteActividadCabecera { get; set; }
    }
}
