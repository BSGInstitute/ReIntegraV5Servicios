using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProcesoSeleccionEtapa
    {
        /// <summary>
        /// Clave de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la Etapa del Proceso de Seleccion
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Primary key de gp.T_ProcesoSeleccion
        /// </summary>
        public int IdProcesoSeleccion { get; set; }
        /// <summary>
        /// Nro de Orden en que se mostrara la Etapa
        /// </summary>
        public int? NroOrden { get; set; }
        /// <summary>
        /// Estado ,valida si esta activo o no
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario que modifico el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Primary key de tabla en V3
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
