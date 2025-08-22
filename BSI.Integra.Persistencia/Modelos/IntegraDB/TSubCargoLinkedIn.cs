using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena SubCargos ingresados por Linkedin que guardan relacion con CargosLinkedIn
    /// </summary>
    public partial class TSubCargoLinkedIn
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id del CargoLinkedIn
        /// </summary>
        public int IdCargoLinkedIn { get; set; }
        /// <summary>
        /// Nombre del SubCargo
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Estado del SubCargo
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del SubCargo
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion del SubCargo
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion del SubCargo
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del SubCargo
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
