using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfiguracionAccesoPersonal
    {
        /// <summary>
        /// (PK) Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key Id del Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Es Foreing Key Id del Personal Acceso Temporal
        /// </summary>
        public int IdPersonalAcceso { get; set; }
        /// <summary>
        /// Fecha de expiracion del acceso
        /// </summary>
        public DateTime? FechaExpiracion { get; set; }
        /// <summary>
        /// Id Modulo Sistema donde se aplicara el acceso
        /// </summary>
        public int IdModuloSistema { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
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
        /// Id de migracion de la tabla
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TPersonal IdPersonalAccesoNavigation { get; set; } = null!;
        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
    }
}
