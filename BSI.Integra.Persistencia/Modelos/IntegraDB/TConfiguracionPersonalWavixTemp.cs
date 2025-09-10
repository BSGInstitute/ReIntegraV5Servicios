using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena los numeros asignados por asesor de wavix
    /// </summary>
    public partial class TConfiguracionPersonalWavixTemp
    {
        /// <summary>
        /// Identificador único de la tabla (autoincremental)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea que referencia al personal Wavix
        /// </summary>
        public int IdPersonalWavix { get; set; }
        /// <summary>
        /// Identificador del país asociado al número
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Número telefónico asignado al asesor
        /// </summary>
        public string Numero { get; set; } = null!;
        /// <summary>
        /// Indica si el número es el predeterminado (1 = Sí, 0 = No)
        /// </summary>
        public bool Predeterminado { get; set; }
        /// <summary>
        /// Estado del registro (1 = Activo, 0 = Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de la última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo utilizado para control de concurrencia (versión de fila)
        /// </summary>
        public byte[]? RowVersion { get; set; }

        public virtual TPersonalWavixTemp IdPersonalWavixNavigation { get; set; } = null!;
    }
}
