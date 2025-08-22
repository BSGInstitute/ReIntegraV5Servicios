using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena el estado de los LeadsSubidos si fue correcto o no
    /// </summary>
    public partial class TRegistroLeadSubido
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Indica él estado de si se subio correctamente
        /// </summary>
        public bool? OportunidnadSubidaCompleta { get; set; }
        /// <summary>
        /// Fecha de Registro Subido
        /// </summary>
        public DateTime? FechaRevisada { get; set; }
        /// <summary>
        /// Estado del SubCargo
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del Registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion del Registro Subido
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion del Registro Subido
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del Registro Subido
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
