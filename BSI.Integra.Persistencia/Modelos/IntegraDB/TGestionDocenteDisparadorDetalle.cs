using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena la configuración base de disparadores de actividades
    /// </summary>
    public partial class TGestionDocenteDisparadorDetalle
    {
        public TGestionDocenteDisparadorDetalle()
        {
            TGestionDocenteActividadDetalleCongelada = new HashSet<TGestionDocenteActividadDetalleCongeladum>();
            TGestionDocenteActividadDetalles = new HashSet<TGestionDocenteActividadDetalle>();
            TGestionDocenteDisparadorCongelados = new HashSet<TGestionDocenteDisparadorCongelado>();
            TGestionDocenteDisparadorEventoTipoCongelados = new HashSet<TGestionDocenteDisparadorEventoTipoCongelado>();
            TGestionDocenteDisparadorEventoTipos = new HashSet<TGestionDocenteDisparadorEventoTipo>();
            TGestionDocenteDisparadorOcurrenciaDetalleCongelados = new HashSet<TGestionDocenteDisparadorOcurrenciaDetalleCongelado>();
            TGestionDocenteDisparadorOcurrenciaDetalles = new HashSet<TGestionDocenteDisparadorOcurrenciaDetalle>();
            TGestionDocenteDisparadorReglaTiempoFijoCongelados = new HashSet<TGestionDocenteDisparadorReglaTiempoFijoCongelado>();
            TGestionDocenteDisparadorReglaTiempoFijos = new HashSet<TGestionDocenteDisparadorReglaTiempoFijo>();
            TGestionDocenteDisparadorReglaTiempoRelativoCongelados = new HashSet<TGestionDocenteDisparadorReglaTiempoRelativoCongelado>();
            TGestionDocenteDisparadorReglaTiempoRelativos = new HashSet<TGestionDocenteDisparadorReglaTiempoRelativo>();
        }

        /// <summary>
        /// Identificador único del disparador
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteTipoDisparadorFlujo
        /// </summary>
        public int IdGestionDocenteDisparadorFlujoTipo { get; set; }
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

        public virtual TGestionDocenteDisparadorFlujoTipo IdGestionDocenteDisparadorFlujoTipoNavigation { get; set; } = null!;
        public virtual ICollection<TGestionDocenteActividadDetalleCongeladum> TGestionDocenteActividadDetalleCongelada { get; set; }
        public virtual ICollection<TGestionDocenteActividadDetalle> TGestionDocenteActividadDetalles { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorCongelado> TGestionDocenteDisparadorCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorEventoTipoCongelado> TGestionDocenteDisparadorEventoTipoCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorEventoTipo> TGestionDocenteDisparadorEventoTipos { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorOcurrenciaDetalleCongelado> TGestionDocenteDisparadorOcurrenciaDetalleCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorOcurrenciaDetalle> TGestionDocenteDisparadorOcurrenciaDetalles { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoFijoCongelado> TGestionDocenteDisparadorReglaTiempoFijoCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoFijo> TGestionDocenteDisparadorReglaTiempoFijos { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoRelativoCongelado> TGestionDocenteDisparadorReglaTiempoRelativoCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoRelativo> TGestionDocenteDisparadorReglaTiempoRelativos { get; set; }
    }
}
