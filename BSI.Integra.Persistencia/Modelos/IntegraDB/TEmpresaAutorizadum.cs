using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TEmpresaAutorizadum
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Razon social
        /// </summary>
        public string RazonSocial { get; set; } = null!;
        /// <summary>
        /// Numero de RUC
        /// </summary>
        public string Ruc { get; set; } = null!;
        /// <summary>
        /// Direccion de la empresa
        /// </summary>
        public string Direccion { get; set; } = null!;
        /// <summary>
        /// Numero de telefono de la central de llamadas
        /// </summary>
        public string Central { get; set; } = null!;
        /// <summary>
        /// Flag de Activo
        /// </summary>
        public bool Activo { get; set; }
        /// <summary>
        /// Es foreing key T_Pais
        /// </summary>
        public int IdPais { get; set; }
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
        /// Nombre Comercial de la Empresa
        /// </summary>
        public string? NombreComercial { get; set; }
    }
}
