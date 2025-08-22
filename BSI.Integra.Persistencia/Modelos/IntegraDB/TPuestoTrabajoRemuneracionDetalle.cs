using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPuestoTrabajoRemuneracionDetalle
    {
        /// <summary>
        /// PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK T_RemuneracionPuestoTrabajo
        /// </summary>
        public int IdPuestoTrabajoRemuneracion { get; set; }
        /// <summary>
        /// FK de T_RemuneracionTipo (Sueldo, Bono, Premio, Comision)
        /// </summary>
        public int IdRemuneracionTipo { get; set; }
        /// <summary>
        /// Fk de T_RemuneracionTipoCobro (Variable, Fijo)
        /// </summary>
        public int IdRemuneracionTipoCobro { get; set; }
        /// <summary>
        /// FK de T_RemuneracionFormaCobro (Monetario, Equipo)
        /// </summary>
        public int IdRemuneracionFormaCobro { get; set; }
        /// <summary>
        /// FK de T_RemuneracionPeriodoCobro (Mensual, Bimestral, Trimestral, etc)
        /// </summary>
        public int IdRemuneracionPeriodoCobro { get; set; }
        /// <summary>
        /// Indica si se evalua sobre tasa de porcentaje
        /// </summary>
        public bool EsTasa { get; set; }
        /// <summary>
        /// Monto para cantidades fijas
        /// </summary>
        public decimal? MontoFijo { get; set; }
        /// <summary>
        /// FK de T_Moneda para los montos fijos
        /// </summary>
        public int? IdMonedaMontoFijo { get; set; }
        /// <summary>
        /// Se indica el porcentaje si es que EsTasa
        /// </summary>
        public decimal? PorcentajeTasa { get; set; }
        /// <summary>
        /// Descripcion en caso el premio sea equipo
        /// </summary>
        public string? DescripcionEquipo { get; set; }
        /// <summary>
        /// Indica si el tipo de remuneracion cuenta con condiones
        /// </summary>
        public bool TieneCondicion { get; set; }
        /// <summary>
        /// FK de T_RemuneracionDescripcionMonetaria (Ventas, Cobranza)
        /// </summary>
        public int? IdRemuneracionDescripcionMonetaria { get; set; }
        /// <summary>
        /// Valor mínimo para rango
        /// </summary>
        public decimal? RangoValorMinimo { get; set; }
        /// <summary>
        /// Valos máximo para rango
        /// </summary>
        public decimal? RangoValorMaximo { get; set; }
        /// <summary>
        /// FK de T_Moneda para rango de valores
        /// </summary>
        public int? IdMonedaRangoValor { get; set; }
        /// <summary>
        /// Porcentaje de ingreso mensual para calcular premios
        /// </summary>
        public decimal? IngresoMensual { get; set; }
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

        public virtual TPuestoTrabajoRemuneracion IdPuestoTrabajoRemuneracionNavigation { get; set; } = null!;
    }
}
