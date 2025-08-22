using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProgramaGeneralCertificacion
    {
        public TProgramaGeneralCertificacion()
        {
            TProgramaGeneralCertificacionArgumentos = new HashSet<TProgramaGeneralCertificacionArgumento>();
            TProgramaGeneralCertificacionModalidads = new HashSet<TProgramaGeneralCertificacionModalidad>();
        }

        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de T_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Nombre de factor de motivacion
        /// </summary>
        public string Nombre { get; set; } = null!;
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
        /// Id de migracion del registro
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual ICollection<TProgramaGeneralCertificacionArgumento> TProgramaGeneralCertificacionArgumentos { get; set; }
        public virtual ICollection<TProgramaGeneralCertificacionModalidad> TProgramaGeneralCertificacionModalidads { get; set; }
    }
}
