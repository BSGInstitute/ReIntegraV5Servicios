using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena detalles de resultados de probabilidad del modelo predictivo escalonado
    /// </summary>
    public partial class TModeloPredictivoProbabilidadEscalonado
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_ModeloPredictivoTipo
        /// </summary>
        public int IdModeloPredictivoProbabilidad { get; set; }
        /// <summary>
        /// FK con la tabla T_ModeloPredictivoEscalonadoClasificacion
        /// </summary>
        public int IdModeloPredictivoEscalonadoClasificacion { get; set; }
        /// <summary>
        /// Probabilidad calculada basado unicamente el perfil del lead (Area formacion, trabajo, cargo e industria)
        /// </summary>
        public decimal ProbabilidadPerfil { get; set; }
        /// <summary>
        /// Probabilidad calculada basado en el perfil del lead + variables de tasa conversion
        /// </summary>
        public decimal ProbabilidadPerfilTasaConversion { get; set; }
        /// <summary>
        /// Probabilidad calculada basado en el perfil del lead + variables de tasa conversion + variables de interaccion
        /// </summary>
        public decimal? ProbabilidadPerfilTasaConversionInteraccion { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TModeloPredictivoEscalonadoClasificacion IdModeloPredictivoEscalonadoClasificacionNavigation { get; set; } = null!;
        public virtual TModeloPredictivoProbabilidad IdModeloPredictivoProbabilidadNavigation { get; set; } = null!;
    }
}
