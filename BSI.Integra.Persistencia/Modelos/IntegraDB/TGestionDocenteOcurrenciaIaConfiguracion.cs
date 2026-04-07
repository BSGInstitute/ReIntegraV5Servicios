using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Configuración de inteligencia artificial para clasificación automática de ocurrencias
    /// </summary>
    public partial class TGestionDocenteOcurrenciaIaConfiguracion
    {
        public TGestionDocenteOcurrenciaIaConfiguracion()
        {
            TGestionDocenteIaEntrenamientoEjemplos = new HashSet<TGestionDocenteIaEntrenamientoEjemplo>();
            TGestionDocenteOcurrenciaCongeladaIaConfiguracions = new HashSet<TGestionDocenteOcurrenciaCongeladaIaConfiguracion>();
            TGestionDocenteOcurrenciaIaEjemploCongelada = new HashSet<TGestionDocenteOcurrenciaIaEjemploCongeladum>();
        }

        /// <summary>
        /// Identificador único de la configuración de IA
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Prompt o instrucción para el modelo de IA
        /// </summary>
        public string? Prompt { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteConfianzaUmbralNivel
        /// </summary>
        public int IdGestionDocenteConfianzaUmbralNivel { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteOcurrencia
        /// </summary>
        public int IdGestionDocenteOcurrencia { get; set; }
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

        public virtual TGestionDocenteConfianzaUmbralNivel IdGestionDocenteConfianzaUmbralNivelNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrencium IdGestionDocenteOcurrenciaNavigation { get; set; } = null!;
        public virtual ICollection<TGestionDocenteIaEntrenamientoEjemplo> TGestionDocenteIaEntrenamientoEjemplos { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladaIaConfiguracion> TGestionDocenteOcurrenciaCongeladaIaConfiguracions { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaIaEjemploCongeladum> TGestionDocenteOcurrenciaIaEjemploCongelada { get; set; }
    }
}
