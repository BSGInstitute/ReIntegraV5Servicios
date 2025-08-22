using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDataCreditoDataInfMicroPerfilGeneral
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string Tipo { get; set; } = null!;
        public string? SectorFinanciero { get; set; }
        public string? SectorCooperativo { get; set; }
        public string? SectorReal { get; set; }
        public string? SectorTelcos { get; set; }
        public string? TotalComoPrincipal { get; set; }
        public string? TotalComoCodeudorYotros { get; set; }
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

        public virtual TDataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
