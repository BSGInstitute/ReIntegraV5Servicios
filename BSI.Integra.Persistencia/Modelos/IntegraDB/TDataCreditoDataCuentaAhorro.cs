using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDataCreditoDataCuentaAhorro
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public bool? Bloqueada { get; set; }
        public string? Entidad { get; set; }
        public string? Numero { get; set; }
        public DateTime? FechaApertura { get; set; }
        /// <summary>
        /// Relacion con la tabla T_DataCreditoCodigoClasificacionCuenta
        /// </summary>
        public string? Calificacion { get; set; }
        /// <summary>
        /// Relacion con la tabla T_DataCreditoCodigoSituacionTitular
        /// </summary>
        public string? SituacionTitular { get; set; }
        public string? Oficina { get; set; }
        public string? Ciudad { get; set; }
        public string? CodigoDaneCiudad { get; set; }
        public int? TipoIdentificacion { get; set; }
        public string? Identificacion { get; set; }
        public string? Sector { get; set; }
        public string? CaracteristicaClase { get; set; }
        public string? ValorMoneda { get; set; }
        public DateTime? ValorFecha { get; set; }
        public string? ValorCalificacion { get; set; }
        public string? EstadoCodigo { get; set; }
        public DateTime? EstadoFecha { get; set; }
        public string? Llave { get; set; }
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
