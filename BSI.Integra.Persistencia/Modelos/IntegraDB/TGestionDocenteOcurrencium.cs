using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Registra ocurrencias o eventos resultantes de actividades (ej: Confirmó asistencia, Rechazó)
    /// </summary>
    public partial class TGestionDocenteOcurrencium
    {
        public TGestionDocenteOcurrencium()
        {
            TGestionDocenteDisparadorOcurrenciaDetalles = new HashSet<TGestionDocenteDisparadorOcurrenciaDetalle>();
            TGestionDocenteOcurrenciaCongelada = new HashSet<TGestionDocenteOcurrenciaCongeladum>();
            TGestionDocenteOcurrenciaCongeladaIaConfiguracions = new HashSet<TGestionDocenteOcurrenciaCongeladaIaConfiguracion>();
            TGestionDocenteOcurrenciaIaConfiguracions = new HashSet<TGestionDocenteOcurrenciaIaConfiguracion>();
        }

        /// <summary>
        /// Identificador único de la ocurrencia
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la ocurrencia
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción de la ocurrencia
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteOcurrenciaTipo
        /// </summary>
        public int IdGestionDocenteOcurrenciaTipo { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteActividadDetalle que originó esta ocurrencia
        /// </summary>
        public int IdGestionDocenteActividadDetalle { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteModoMarcado
        /// </summary>
        public int IdGestionDocenteModoMarcado { get; set; }
        /// <summary>
        /// Indica si la ocurrencia requiere comentario adicional
        /// </summary>
        public bool RequiereComentario { get; set; }
        /// <summary>
        /// Indica si la ocurrencia requiere fecha y hora específica
        /// </summary>
        public bool RequiereFechaHora { get; set; }
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

        public virtual TGestionDocenteActividadDetalle IdGestionDocenteActividadDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteModoMarcado IdGestionDocenteModoMarcadoNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrenciaTipo IdGestionDocenteOcurrenciaTipoNavigation { get; set; } = null!;
        public virtual ICollection<TGestionDocenteDisparadorOcurrenciaDetalle> TGestionDocenteDisparadorOcurrenciaDetalles { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladum> TGestionDocenteOcurrenciaCongelada { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladaIaConfiguracion> TGestionDocenteOcurrenciaCongeladaIaConfiguracions { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaIaConfiguracion> TGestionDocenteOcurrenciaIaConfiguracions { get; set; }
    }
}
