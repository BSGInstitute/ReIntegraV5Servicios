using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPostulanteLog
    {
        /// <summary>
        /// PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK T_Postulante
        /// </summary>
        public int IdPostulante { get; set; }
        /// <summary>
        /// Campo modificado
        /// </summary>
        public string Clave { get; set; } = null!;
        /// <summary>
        /// Valor del campo modificado
        /// </summary>
        public string Valor { get; set; } = null!;
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion de registro
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
    }
}
