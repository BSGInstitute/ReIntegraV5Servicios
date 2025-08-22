using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TRecuperacionAutomaticoModuloSistema
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla conf.T_ModuloSistema
        /// </summary>
        public int IdModuloSistema { get; set; }
        /// <summary>
        /// Tipo de recuperacion
        /// </summary>
        public string Tipo { get; set; } = null!;
        /// <summary>
        /// Estado de la configuracion
        /// </summary>
        public bool Habilitado { get; set; }
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
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de V3
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Flag para indicar si se habilita el envio de correo
        /// </summary>
        public bool EnvioCorreo { get; set; }
    }
}
