using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalSistemaPensionario
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Primary Key de gp.T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Primary key de gp.T_SistemaPensionario
        /// </summary>
        public int IdSistemaPensionario { get; set; }
        /// <summary>
        /// Primary key de gp.T_EntidadSistemaPensionario
        /// </summary>
        public int? IdEntidadSistemaPensionario { get; set; }
        /// <summary>
        /// Codigo Afiliado de Sistema Pensionario
        /// </summary>
        public string? CodigoAfiliado { get; set; }
        /// <summary>
        /// Identifica si el registro esta activo (en uso)
        /// </summary>
        public bool Activo { get; set; }
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
