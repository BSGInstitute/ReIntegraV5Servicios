using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTarifarioDetalleAlterno
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK mkt.T_Tarifario
        /// </summary>
        public int IdTarifario { get; set; }
        /// <summary>
        /// Concepto Tarifario Detalle
        /// </summary>
        public string? Concepto { get; set; }
        /// <summary>
        /// FK T_Pais
        /// </summary>
        public int? IdPais { get; set; }
        /// <summary>
        /// Monto País
        /// </summary>
        public decimal? Monto { get; set; }
        /// <summary>
        /// Si aplica cuota
        /// </summary>
        public bool? AplicaCuota { get; set; }
        /// <summary>
        /// Descripcion de tarifario detalle
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Tipo de Cantidad
        /// </summary>
        public string? TipoCantidad { get; set; }
        /// <summary>
        /// Estados de matricula
        /// </summary>
        public string? Estados { get; set; }
        /// <summary>
        /// SubEstados de matricula
        /// </summary>
        public string? SubEstados { get; set; }
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
        /// <summary>
        /// FK T_Moneda
        /// </summary>
        public int? IdMoneda { get; set; }
        /// <summary>
        /// Flag para visualizar en portal web
        /// </summary>
        public bool? VisualizarPortalWeb { get; set; }
    }
}
