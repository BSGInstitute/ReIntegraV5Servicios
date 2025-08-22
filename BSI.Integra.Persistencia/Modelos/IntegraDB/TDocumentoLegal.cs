using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDocumentoLegal
    {
        /// <summary>
        /// PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del documento
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion del documento
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// FK de T_Pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// URL del documento
        /// </summary>
        public string Url { get; set; } = null!;
        /// <summary>
        /// Indicador si se visualiza en agenda
        /// </summary>
        public bool? VisualizarAgenda { get; set; }
        /// <summary>
        /// Indicador si se descarga en agenda
        /// </summary>
        public bool? DescargarAgenda { get; set; }
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
        public string? Roles { get; set; }
    }
}
