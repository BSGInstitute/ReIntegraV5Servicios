using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TLogEnvioHelperWhatsApp
    {
        /// <summary>
        /// Identificador unico de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Numero Celular al que fue enviado wpp
        /// </summary>
        public string Numero { get; set; } = null!;
        /// <summary>
        /// Identificador unico que hace referncia a la tabla MKT.T_ALUMNO
        /// </summary>
        public int IdAlumno { get; set; }
        /// <summary>
        /// Fecha en la que fue enviado el wpp
        /// </summary>
        public DateTime FechaEnvioWpp { get; set; }
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
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
