using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProgramaGeneralModeloCertificado
    {
        public TProgramaGeneralModeloCertificado()
        {
            TProgramaGeneralModeloCertificadoModalidads = new HashSet<TProgramaGeneralModeloCertificadoModalidad>();
        }

        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk de T_Pgeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Nombre del modelo de certificado
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Url del documento
        /// </summary>
        public string Url { get; set; } = null!;
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// RowVersion del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// IdMigracion del registro
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual ICollection<TProgramaGeneralModeloCertificadoModalidad> TProgramaGeneralModeloCertificadoModalidads { get; set; }
    }
}
