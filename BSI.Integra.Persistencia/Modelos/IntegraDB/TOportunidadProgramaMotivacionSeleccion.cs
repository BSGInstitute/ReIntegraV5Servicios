using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena las selecciones del asesor respecto a la motivacion de la oportunidad
    /// </summary>
    public partial class TOportunidadProgramaMotivacionSeleccion
    {
        /// <summary>
        /// Identificador único de la selección
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Oportunidad asociada
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// Programa de motivación seleccionado
        /// </summary>
        public int IdProgramaMotivacion { get; set; }
        /// <summary>
        /// Estado lógico (1: activo, 0: inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo para control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Indica la prioridad de la motivacion.
        /// </summary>
        public int? Prioridad { get; set; }

        public virtual TOportunidad IdOportunidadNavigation { get; set; } = null!;
        public virtual TProgramaMotivacion IdProgramaMotivacionNavigation { get; set; } = null!;
    }
}
