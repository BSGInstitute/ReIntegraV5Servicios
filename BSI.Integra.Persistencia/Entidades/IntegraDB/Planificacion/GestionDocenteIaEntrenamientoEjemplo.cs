using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class GestionDocenteIaEntrenamientoEjemplo : BaseIntegraEntity
    {
        public int IdGestionDocenteOcurrenciaIaConfiguracion { get; set; }
        public int IdGestionDocenteIaEntrenamientoClasificacionTipo { get; set; }
        public string TextoEjemplo { get; set; }
        public bool EsPositivo { get; set; }
    }
}
