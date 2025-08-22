using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDataCreditoDataNaturalNacional
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string NroDocumento { get; set; } = null!;
        public string? Nombres { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? NombreCompleto { get; set; }
        public bool? Validada { get; set; }
        public bool? Rut { get; set; }
        public string? Genero { get; set; }
        public string? IdentificacionEstado { get; set; }
        public DateTime? IdentificacionFechaExpedicion { get; set; }
        public string? IdentificacionCiudad { get; set; }
        public string? IdentificacionDepartamento { get; set; }
        public string? IdentificacionNumero { get; set; }
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }
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
