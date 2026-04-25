using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada de la regla de tiempo relativo para disparadores. Define un desplazamiento de tiempo desde una referencia (ej: 3 días antes del inicio de sesión).
    /// </summary>
    public partial class TGestionDocenteDisparadorReglaTiempoRelativoCongelado
    {
        public TGestionDocenteDisparadorReglaTiempoRelativoCongelado()
        {
            TGestionDocenteDisparadorReglaTiempoRelativoReferenciaCongelados = new HashSet<TGestionDocenteDisparadorReglaTiempoRelativoReferenciaCongelado>();
        }

        /// <summary>
        /// Identificador único de la regla de tiempo relativo congelada. Clave primaria. Generado automáticamente.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del disparador congelado. Clave foránea a T_GestionDocenteDisparadorCongelado.
        /// </summary>
        public int IdGestionDocenteDisparadorCongelado { get; set; }
        /// <summary>
        /// Identificador de la regla de tiempo relativo original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteDisparadorReglaTiempoRelativo { get; set; }
        /// <summary>
        /// Identificador de la regla de tiempo genérica. Referencia la configuración base.
        /// </summary>
        public int IdGestionDocenteDisparadorReglaTiempo { get; set; }
        /// <summary>
        /// Identificador del disparador detalle original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        /// <summary>
        /// Número de unidades de tiempo para el desplazamiento. Ejemplo: 3 (para 3 días, 3 horas, etc. según IdGestionDocenteUnidadTiempo). Puede ser negativo para desplazamientos previos. Rango: -999 a 999.
        /// </summary>
        public int Cantidad { get; set; }
        /// <summary>
        /// Identificador de la unidad de tiempo (DIAS, HORAS, MINUTOS, SEMANAS, MESES). Referencia a catálogo T_GestionDocenteUnidadTiempo. Define la escala de tiempo.
        /// </summary>
        public int IdGestionDocenteUnidadTiempo { get; set; }
        /// <summary>
        /// Identificador del estado de ejecución. Referencia a T_GestionDocenteEstadoEjecucion.
        /// </summary>
        public int IdGestionDocenteEstadoEjecucion { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo. 1 = Activo, 0 = Inactivo. Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que realizó el congelamiento. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó el registro. Campo de auditoría obligatorio.
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
        /// Control de concurrencia optimista. Generado automáticamente. Campo de auditoría obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionDocenteDisparadorCongelado IdGestionDocenteDisparadorCongeladoNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorDetalle IdGestionDocenteDisparadorDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorReglaTiempo IdGestionDocenteDisparadorReglaTiempoNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorReglaTiempoRelativo IdGestionDocenteDisparadorReglaTiempoRelativoNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
        public virtual TGestionDocenteUnidadTiempo IdGestionDocenteUnidadTiempoNavigation { get; set; } = null!;
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoRelativoReferenciaCongelado> TGestionDocenteDisparadorReglaTiempoRelativoReferenciaCongelados { get; set; }
    }
}
