namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    /// <summary>
    /// DTO para las fases del proceso de gestión de contactos
    /// </summary>
    public class FaseGestionContactoDTO
    {
        /// <summary>
        /// Identificador único de la fase
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Código único identificador de la fase (ej: LEAD, PROSP, CLI)
        /// </summary>
        public string? Codigo { get; set; }

        /// <summary>
        /// Nombre descriptivo de la fase
        /// </summary>
        public string? Nombre { get; set; }

        /// <summary>
        /// Descripción detallada de la fase
        /// </summary>
        public string? Descripcion { get; set; }
    }
}
