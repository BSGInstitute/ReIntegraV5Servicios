using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class ProcesoSeleccion : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// FK T_PuestoTrabajo
        /// </summary>
        public int IdPuestoTrabajo { get; set; }
        /// <summary>
        /// Codigo del proceso de selección
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Url del proceso de selección
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// Estado del proceso de selecciomn
        /// </summary>
        public bool? Activo { get; set; }
        /// <summary>
        /// Fk T_Sede
        /// </summary>
        public int? IdSede { get; set; }
        public DateTime? FechaInicioProceso { get; set; }
        /// <summary>
        /// Fecha de Fin del proceso
        /// </summary>
        public DateTime? FechaFinProceso { get; set; }
    }
}
