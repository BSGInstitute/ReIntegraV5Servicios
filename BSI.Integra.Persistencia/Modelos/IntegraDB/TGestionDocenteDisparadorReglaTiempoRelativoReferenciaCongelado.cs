using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada de la referencia de tiempo para reglas relativas. Define el punto de referencia desde el cual se calcula el desplazamiento relativo.
    /// </summary>
    public partial class TGestionDocenteDisparadorReglaTiempoRelativoReferenciaCongelado
    {
        /// <summary>
        /// Identificador único. Clave primaria. Generado automáticamente.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la regla de tiempo relativo congelada padre. Clave foránea.
        /// </summary>
        public int IdGestionDocenteDisparadorReglaTiempoRelativoCongelado { get; set; }
        /// <summary>
        /// Identificador de la referencia de tiempo relativo original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteDisparadorReglaTiempoRelativoReferencia { get; set; }
        /// <summary>
        /// Identificador de la regla de tiempo relativo original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteDisparadorReglaTiempoRelativo { get; set; }
        /// <summary>
        /// Identificador de la referencia de tiempo. Valores: INICIO_SESION, FIN_SESION, FECHA_MATRICULA, FECHA_ACTIVIDAD, FECHA_EVALUACION, etc. Define el punto de referencia desde donde se calcula el desplazamiento.
        /// </summary>
        public int IdGestionDocenteReferenciaTiempo { get; set; }
        /// <summary>
        /// Identificador del estado de ejecución. Referencia a T_GestionDocenteEstadoEjecucion.
        /// </summary>
        public int IdGestionDocenteEstadoEjecucion { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo. Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que realizó el congelamiento. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en UTC-5 del congelamiento. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora en UTC-5 de la última modificación. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia optimista. Campo de auditoría obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionDocenteDisparadorReglaTiempoRelativoCongelado IdGestionDocenteDisparadorReglaTiempoRelativoCongeladoNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorReglaTiempoRelativo IdGestionDocenteDisparadorReglaTiempoRelativoNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorReglaTiempoRelativoReferencium IdGestionDocenteDisparadorReglaTiempoRelativoReferenciaNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
        public virtual TGestionDocenteReferenciaTiempo IdGestionDocenteReferenciaTiempoNavigation { get; set; } = null!;
    }
}
