using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TComprobantePago
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave secundaria de la tabla T_SunatDocumento
        /// </summary>
        public int IdSunatDocumento { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Serie del comprobante
        /// </summary>
        public string SerieComprobante { get; set; } = null!;
        /// <summary>
        /// Numero del comprobante
        /// </summary>
        public string NumeroComprobante { get; set; } = null!;
        /// <summary>
        /// Llave foranea de la tabla T_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Monto bruto del comprobante, es el monto bruto original mas el ajuste del monto bruto
        /// </summary>
        public decimal MontoBruto { get; set; }
        /// <summary>
        /// Fecha emision del comprobante
        /// </summary>
        public DateTime FechaEmision { get; set; }
        /// <summary>
        /// Fecha programacion del comprobante
        /// </summary>
        public DateTime FechaProgramacion { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_TipoImpuesto
        /// </summary>
        public int? IdTipoImpuesto { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Retencion
        /// </summary>
        public int? IdRetencion { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Detraccion
        /// </summary>
        public int? IdDetraccion { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Proveedor
        /// </summary>
        public int? IdProveedor { get; set; }
        /// <summary>
        /// Estado de eliminado o no
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id origen de la tabla al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Monto Bruto del comrobante
        /// </summary>
        public decimal MontoNeto { get; set; }
        /// <summary>
        /// Valor a añadido al monto bruto para su ajuste
        /// </summary>
        public decimal AjusteMontoBruto { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_ComprobantePagoEstado
        /// </summary>
        public int? IdComprobantePagoEstado { get; set; }
        /// <summary>
        /// Fecha de vencimiento de la reprograqmacion
        /// </summary>
        public DateTime? FechaVencimientoReprogramacion { get; set; }
        /// <summary>
        /// Monto inafecto
        /// </summary>
        public decimal? MontoInafecto { get; set; }
        /// <summary>
        /// Monto de IGV
        /// </summary>
        public decimal? MontoIgv { get; set; }
        /// <summary>
        /// Porcentaje de IGV
        /// </summary>
        public decimal? PorcentajeIgv { get; set; }
        /// <summary>
        /// Otra taza de contribuicion
        /// </summary>
        public decimal? OtraTazaContribucion { get; set; }
        /// <summary>
        /// Identificador de empresa
        /// </summary>
        public int? IdEmpresa { get; set; }
        /// <summary>
        /// Identificador de Ciudad
        /// </summary>
        public int? IdCiudad { get; set; }

        public virtual TGestionPago TGestionPago { get; set; } = null!;
    }
}
