using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class GestionDocenteOcurrenciaIaConfiguracion : BaseIntegraEntity
    {
        public string Prompt { get; set; }
        public int IdGestionDocenteConfianzaUmbralNivel { get; set; }
        public int IdGestionDocenteOcurrencia { get; set; }
    }
}
