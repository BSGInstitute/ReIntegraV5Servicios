using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena detalles de resultados de probabilidad del modelo predictivo escalonado
    /// </summary>
    public partial class TModeloPredictivoEscalonadoClasificacion
    {
        public TModeloPredictivoEscalonadoClasificacion()
        {
            TModeloPredictivoProbabilidadEscalonados = new HashSet<TModeloPredictivoProbabilidadEscalonado>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del area a la que pertenece el modelo
        /// </summary>
        public int IdModeloPredictivoEscalonado { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_AreaCapacitacion
        /// </summary>
        public int? IdAreaCapacitacion { get; set; }
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

        public virtual TAreaCapacitacion? IdAreaCapacitacionNavigation { get; set; }
        public virtual TModeloPredictivoEscalonado IdModeloPredictivoEscalonadoNavigation { get; set; } = null!;
        public virtual ICollection<TModeloPredictivoProbabilidadEscalonado> TModeloPredictivoProbabilidadEscalonados { get; set; }
    }
}
