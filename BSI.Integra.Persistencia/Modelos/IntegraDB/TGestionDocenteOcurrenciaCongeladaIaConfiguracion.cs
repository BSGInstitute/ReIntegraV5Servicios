using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada de la configuración de Inteligencia Artificial para ocurrencias. Define el prompt y niveles de confianza para análisis automático de ocurrencias mediante IA.
    /// </summary>
    public partial class TGestionDocenteOcurrenciaCongeladaIaConfiguracion
    {
        public TGestionDocenteOcurrenciaCongeladaIaConfiguracion()
        {
            TGestionDocenteOcurrenciaIaEjemploCongelada = new HashSet<TGestionDocenteOcurrenciaIaEjemploCongeladum>();
        }

        /// <summary>
        /// Identificador único. Clave primaria. Generado automáticamente.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la ocurrencia congelada a la cual pertenece esta configuración IA. Clave foránea a T_GestionDocenteOcurrenciaCongelada.
        /// </summary>
        public int IdGestionDocenteOcurrenciaCongelada { get; set; }
        /// <summary>
        /// Identificador de la configuración IA original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteOcurrenciaIaConfiguracion { get; set; }
        /// <summary>
        /// Identificador de la ocurrencia original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteOcurrencia { get; set; }
        /// <summary>
        /// Instrucción detallada para el modelo de IA. Especifica cómo clasificar comentarios, textos o datos para determinar si la ocurrencia se marcó. Puede ser muy extenso. Ejemplo: &quot;Analiza el siguiente comentario del docente. Si contiene palabras positivas sobre asistencia, responde SI, si no, responde NO.&quot;
        /// </summary>
        public string Prompt { get; set; } = null!;
        /// <summary>
        /// Identificador del nivel de umbral de confianza para aceptar la clasificación IA. Valores: BAJO (&gt;50%), MEDIO (&gt;70%), ALTO (&gt;85%), MUY_ALTO (&gt;95%). Define cuán segura debe ser la IA para registrar automáticamente la ocurrencia.
        /// </summary>
        public int IdGestionDocenteConfianzaUmbralNivel { get; set; }
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

        public virtual TGestionDocenteConfianzaUmbralNivel IdGestionDocenteConfianzaUmbralNivelNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrenciaCongeladum IdGestionDocenteOcurrenciaCongeladaNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrenciaIaConfiguracion IdGestionDocenteOcurrenciaIaConfiguracionNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrencium IdGestionDocenteOcurrenciaNavigation { get; set; } = null!;
        public virtual ICollection<TGestionDocenteOcurrenciaIaEjemploCongeladum> TGestionDocenteOcurrenciaIaEjemploCongelada { get; set; }
    }
}
