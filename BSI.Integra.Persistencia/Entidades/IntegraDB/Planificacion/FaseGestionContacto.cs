using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    /// <summary>
    /// Entidad que representa las fases del proceso de gestión de contactos
    /// </summary>
    public class FaseGestionContacto : BaseIntegraEntity
    {
        /// <summary>
        /// Código único identificador de la fase (ej: LEAD, PROSP, CLI)
        /// </summary>
        public string? Codigo { get; set; }

        /// <summary>
        /// Nombre descriptivo de la fase del proceso de gestión de contactos
        /// </summary>
        public string? Nombre { get; set; }

        /// <summary>
        /// Descripción detallada de la fase y sus características
        /// </summary>
        public string? Descripcion { get; set; }
    }
}
