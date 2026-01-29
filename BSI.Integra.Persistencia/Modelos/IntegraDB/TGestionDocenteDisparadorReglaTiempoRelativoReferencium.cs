using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Define disparadores relativos a eventos (X horas antes o después de una sesión)
    /// </summary>
    public partial class TGestionDocenteDisparadorReglaTiempoRelativoReferencium
    {
        public TGestionDocenteDisparadorReglaTiempoRelativoReferencium()
        {
            InverseIdGestionDocenteDisparadorReglaTiempoRelativoNavigation = new HashSet<TGestionDocenteDisparadorReglaTiempoRelativoReferencium>();
        }

        /// <summary>
        /// Identificador único de la regla de tiempo relativo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteDisparadorReglaTiempo
        /// </summary>
        public int IdGestionDocenteDisparadorReglaTiempoRelativo { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteDetalleDisparador
        /// </summary>
        public int IdGestionDocenteReferenciaTiempo { get; set; }
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

        public virtual TGestionDocenteDisparadorReglaTiempoRelativoReferencium IdGestionDocenteDisparadorReglaTiempoRelativoNavigation { get; set; } = null!;
        public virtual TGestionDocenteReferenciaTiempo IdGestionDocenteReferenciaTiempoNavigation { get; set; } = null!;
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoRelativoReferencium> InverseIdGestionDocenteDisparadorReglaTiempoRelativoNavigation { get; set; }
    }
}
