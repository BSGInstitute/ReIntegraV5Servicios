using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMaterialAsociacionCriterioVerificacion
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea a la tabla ope.T_MaterialTipo
        /// </summary>
        public int IdMaterialTipo { get; set; }
        /// <summary>
        /// Llave foranea a la tabla ope.T_MaterialCriterioVerificacion
        /// </summary>
        public int IdMaterialCriterioVerificacion { get; set; }
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
        /// Usuario de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }

        public virtual TMaterialCriterioVerificacion IdMaterialCriterioVerificacionNavigation { get; set; } = null!;
        public virtual TMaterialTipo IdMaterialTipoNavigation { get; set; } = null!;
    }
}
