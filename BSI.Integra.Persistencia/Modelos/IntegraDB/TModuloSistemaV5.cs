using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TModuloSistemaV5
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del modulo
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Nombre del grupo
        /// </summary>
        public string IdModuloSistemaGrupo { get; set; } = null!;
        /// <summary>
        /// URL del modulo
        /// </summary>
        public string Url { get; set; } = null!;
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
        /// <summary>
        /// Llave foranea cn la tabla T_ModuloSistemaSubGrupo
        /// </summary>
        public int? IdModuloSistemaSubGrupo { get; set; }
        /// <summary>
        /// Orden de Visualización en Menú Principal Integra
        /// </summary>
        public int? OrdenMenuPrincipal { get; set; }
    }
}
