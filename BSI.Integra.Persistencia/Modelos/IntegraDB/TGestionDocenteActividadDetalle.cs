using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Bitácora de ejecución de actividades específicas dentro de un flujo
    /// </summary>
    public partial class TGestionDocenteActividadDetalle
    {
        public TGestionDocenteActividadDetalle()
        {
            TGestionContactoActividadDetalleSesions = new HashSet<TGestionContactoActividadDetalleSesion>();
            TGestionDocenteActividadDetalleCongelada = new HashSet<TGestionDocenteActividadDetalleCongeladum>();
            TGestionDocenteOcurrencia = new HashSet<TGestionDocenteOcurrencium>();
            TGestionDocenteOcurrenciaCongelada = new HashSet<TGestionDocenteOcurrenciaCongeladum>();
        }

        /// <summary>
        /// Identificador único de la actividad detalle
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteActividadCabecera
        /// </summary>
        public int IdGestionDocenteActividadCabecera { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteActividadDetalleTipo
        /// </summary>
        public int IdGestionDocenteActividadDetalleTipo { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_PlantillaMedioComunicacion
        /// </summary>
        public int? IdPlantillaMedioComunicacion { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteDisparadorDetalle
        /// </summary>
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        /// <summary>
        /// Nombre de la actividad detalle
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Estado de la actividad (PENDIENTE, EJECUTADO, MARCADO, CANCELADO)
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

        public virtual TGestionDocenteActividadCabecera IdGestionDocenteActividadCabeceraNavigation { get; set; } = null!;
        public virtual TGestionDocenteActividadDetalleTipo IdGestionDocenteActividadDetalleTipoNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorDetalle IdGestionDocenteDisparadorDetalleNavigation { get; set; } = null!;
        public virtual TPlantillaMedioComunicacion? IdPlantillaMedioComunicacionNavigation { get; set; }
        public virtual ICollection<TGestionContactoActividadDetalleSesion> TGestionContactoActividadDetalleSesions { get; set; }
        public virtual ICollection<TGestionDocenteActividadDetalleCongeladum> TGestionDocenteActividadDetalleCongelada { get; set; }
        public virtual ICollection<TGestionDocenteOcurrencium> TGestionDocenteOcurrencia { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladum> TGestionDocenteOcurrenciaCongelada { get; set; }
    }
}
