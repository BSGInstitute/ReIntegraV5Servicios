using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada del disparador. Un disparador es la condición que inicia la ejecución de una actividad detalle. Soporta cuatro tipos: FECHA_FIJA, TIEMPO_RELATIVO, OCURRENCIA_PREVIA, EVENTO_SISTEMA. Incluye fecha calculada según tipo de asignación.
    /// </summary>
    public partial class TGestionDocenteDisparadorCongelado
    {
        public TGestionDocenteDisparadorCongelado()
        {
            TGestionDocenteDisparadorCongeladoLogs = new HashSet<TGestionDocenteDisparadorCongeladoLog>();
            TGestionDocenteDisparadorEventoTipoCongelados = new HashSet<TGestionDocenteDisparadorEventoTipoCongelado>();
            TGestionDocenteDisparadorOcurrenciaDetalleCongelados = new HashSet<TGestionDocenteDisparadorOcurrenciaDetalleCongelado>();
            TGestionDocenteDisparadorReglaTiempoFijoCongelados = new HashSet<TGestionDocenteDisparadorReglaTiempoFijoCongelado>();
            TGestionDocenteDisparadorReglaTiempoRelativoCongelados = new HashSet<TGestionDocenteDisparadorReglaTiempoRelativoCongelado>();
        }

        /// <summary>
        /// Identificador único del disparador congelado. Clave primaria. Generado automáticamente por IDENTITY.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la actividad detalle congelada a la cual pertenece este disparador. Clave foránea a T_GestionDocenteActividadDetalleCongelada. Establece relación jerárquica.
        /// </summary>
        public int IdGestionDocenteActividadDetalleCongelada { get; set; }
        /// <summary>
        /// Identificador del disparador detalle original (sin congelar). Referencia el maestro para propósitos de auditoría.
        /// </summary>
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        /// <summary>
        /// Identificador del tipo de disparador en el flujo. Referencia a T_GestionDocenteDisparadorFlujoTipo. Clasifica la naturaleza del disparador.
        /// </summary>
        public int IdGestionDocenteDisparadorFlujoTipo { get; set; }
        /// <summary>
        /// Identificador de la ocurrencia congelada previa. Usado cuando TipoDisparador = OCURRENCIA_PREVIA para indicar qué ocurrencia debe marcarse primero. Referencia a T_GestionDocenteOcurrenciaCongelada. Puede ser nulo.
        /// </summary>
        public int? IdGestionDocenteOcurrenciaCongelada { get; set; }
        /// <summary>
        /// Identificador del estado de ejecución del disparador (por ejecutar, en progreso, ejecutado, fallido, cancelado). Referencia a T_GestionDocenteEstadoEjecucion.
        /// </summary>
        public int IdGestionDocenteEstadoEjecucion { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo. 1 = Activo, 0 = Inactivo. Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que realizó el congelamiento. Máximo 50 caracteres. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó el registro congelado. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en UTC-5 del congelamiento. Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría obligatorio.
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

        public virtual TGestionDocenteActividadDetalleCongeladum IdGestionDocenteActividadDetalleCongeladaNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorDetalle IdGestionDocenteDisparadorDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorFlujoTipo IdGestionDocenteDisparadorFlujoTipoNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrenciaCongeladum? IdGestionDocenteOcurrenciaCongeladaNavigation { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorCongeladoLog> TGestionDocenteDisparadorCongeladoLogs { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorEventoTipoCongelado> TGestionDocenteDisparadorEventoTipoCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorOcurrenciaDetalleCongelado> TGestionDocenteDisparadorOcurrenciaDetalleCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoFijoCongelado> TGestionDocenteDisparadorReglaTiempoFijoCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoRelativoCongelado> TGestionDocenteDisparadorReglaTiempoRelativoCongelados { get; set; }
    }
}
