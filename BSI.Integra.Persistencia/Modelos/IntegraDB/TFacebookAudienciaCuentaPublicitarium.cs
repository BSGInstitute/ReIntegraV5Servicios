using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFacebookAudienciaCuentaPublicitarium
    {
        /// <summary>
        /// Es clave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreign key de la tabla T_FacebookAudiencia
        /// </summary>
        public int IdFacebookAudiencia { get; set; }
        /// <summary>
        /// Es foreign key de la tabla T_FacebookCuentaPublicitaria
        /// </summary>
        public int IdFacebookCuentaPublicitaria { get; set; }
        /// <summary>
        /// Subtipo de la audiencia
        /// </summary>
        public string Subtipo { get; set; } = null!;
        /// <summary>
        /// Origen de la audiencia
        /// </summary>
        public string Origen { get; set; } = null!;
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
        public Guid? IdMigracion { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
    }
}
