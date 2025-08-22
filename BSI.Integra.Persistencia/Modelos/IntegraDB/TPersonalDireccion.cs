using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalDireccion
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Fk T_Pais
        /// </summary>
        public int? IdPais { get; set; }
        /// <summary>
        /// Fk T_Ciudad
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// Distrito del personal
        /// </summary>
        public string? Distrito { get; set; }
        /// <summary>
        /// Tipo de via que registra el personal
        /// </summary>
        public string? TipoVia { get; set; }
        /// <summary>
        /// Nombre de via
        /// </summary>
        public string? NombreVia { get; set; }
        /// <summary>
        /// Manzana - Parte de direccion del personal
        /// </summary>
        public string? Manzana { get; set; }
        /// <summary>
        /// Lote - Parte de direccion del personal
        /// </summary>
        public int? Lote { get; set; }
        /// <summary>
        /// Tipo de zona urbana - direccion del personal
        /// </summary>
        public string? TipoZonaUrbana { get; set; }
        /// <summary>
        /// Nombre de zona urbana - Direccion del personal
        /// </summary>
        public string? NombreZonaUrbana { get; set; }
        /// <summary>
        /// Activo/Inactivo direccion vigente
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
