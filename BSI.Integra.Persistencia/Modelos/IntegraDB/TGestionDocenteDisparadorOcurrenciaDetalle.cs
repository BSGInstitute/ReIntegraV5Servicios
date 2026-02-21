using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla relacional que vincula disparadores con ocurrencias previas que los activan
    /// </summary>
    public partial class TGestionDocenteDisparadorOcurrenciaDetalle
    {
        public TGestionDocenteDisparadorOcurrenciaDetalle()
        {
            TGestionDocenteDisparadorOcurrenciaDetalleCongelados = new HashSet<TGestionDocenteDisparadorOcurrenciaDetalleCongelado>();
        }

        /// <summary>
        /// Identificador único de la relación
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteDisparadorDetalle
        /// </summary>
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteOcurrencia que actúa como disparador previo
        /// </summary>
        public int IdGestionDocenteOcurrenciaPrevia { get; set; }
        /// <summary>
        /// Estado del registro (1=Activo, 0=Inactivo)
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
        /// Versión de fila para control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionDocenteDisparadorDetalle IdGestionDocenteDisparadorDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrencium IdGestionDocenteOcurrenciaPreviaNavigation { get; set; } = null!;
        public virtual ICollection<TGestionDocenteDisparadorOcurrenciaDetalleCongelado> TGestionDocenteDisparadorOcurrenciaDetalleCongelados { get; set; }
    }
}
