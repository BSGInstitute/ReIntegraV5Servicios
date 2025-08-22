using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMontoPago
    {
        public TMontoPago()
        {
            TMontoPagoPlataformas = new HashSet<TMontoPagoPlataforma>();
            TMontoPagoSuscripcions = new HashSet<TMontoPagoSuscripcion>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Precio en numeros designado sin decimales
        /// </summary>
        public decimal Precio { get; set; }
        /// <summary>
        /// El precio en numero representado en letras con la moneda al final
        /// </summary>
        public string PrecioLetras { get; set; } = null!;
        /// <summary>
        /// Es Foreing Key TPW_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Monto de la matricula si es en cuotas
        /// </summary>
        public decimal? Matricula { get; set; }
        /// <summary>
        /// Monto de la cuota sea 1 o mas
        /// </summary>
        public decimal? Cuotas { get; set; }
        /// <summary>
        /// Numero de cuotas designadas
        /// </summary>
        public int? NroCuotas { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_TipoDescuento
        /// </summary>
        public int? IdTipoDescuento { get; set; }
        /// <summary>
        /// Es Foreing Key tPLA_PGeneral
        /// </summary>
        public int? IdPrograma { get; set; }
        public int? IdTipoPago { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Pais
        /// </summary>
        public int? IdPais { get; set; }
        /// <summary>
        /// Dia en el que vence el pago
        /// </summary>
        public string? Vencimiento { get; set; }
        /// <summary>
        /// Mes de la primera cuota
        /// </summary>
        public string? PrimeraCuota { get; set; }
        /// <summary>
        /// Si la cuota es doble
        /// </summary>
        public bool? CuotaDoble { get; set; }
        public string? Descripcion { get; set; }
        public bool? VisibleWeb { get; set; }
        public int? Paquete { get; set; }
        public bool? PorDefecto { get; set; }
        public decimal? MontoDescontado { get; set; }
        /// <summary>
        /// Creado o eliminado
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
        /// Sistema Automatico Fecha creacion
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

        public virtual ICollection<TMontoPagoPlataforma> TMontoPagoPlataformas { get; set; }
        public virtual ICollection<TMontoPagoSuscripcion> TMontoPagoSuscripcions { get; set; }
    }
}
