using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMontoPagoLog
    {
        /// <summary>
        /// (PK) Primary Key del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// (FK) que referencia a T_MontoPago
        /// </summary>
        public int IdMontoPago { get; set; }
        /// <summary>
        /// Almacena el precio original
        /// </summary>
        public decimal PrecioOriginal { get; set; }
        /// <summary>
        /// Registro del precio modificado
        /// </summary>
        public decimal? PrecioModificado { get; set; }
        /// <summary>
        /// Precio original expresado en letras
        /// </summary>
        public string PrecioLetrasOriginal { get; set; } = null!;
        /// <summary>
        /// Precio modificado expresado en letras
        /// </summary>
        public string? PrecioLetrasModificado { get; set; }
        /// <summary>
        /// (FK) Moneda original
        /// </summary>
        public int IdMonedaOriginal { get; set; }
        /// <summary>
        /// (FK) Moneda modificada
        /// </summary>
        public int? IdMonedaModificado { get; set; }
        /// <summary>
        /// Matrícula original
        /// </summary>
        public decimal? MatriculaOriginal { get; set; }
        /// <summary>
        /// Matrícula modificada
        /// </summary>
        public decimal? MatriculaModificado { get; set; }
        /// <summary>
        /// Monto de la cuota original
        /// </summary>
        public decimal? CuotasOriginal { get; set; }
        /// <summary>
        /// Monto de la cuota modificado
        /// </summary>
        public decimal? CuotasModificado { get; set; }
        /// <summary>
        /// Número de cuotas original
        /// </summary>
        public int? NroCuotasOriginal { get; set; }
        /// <summary>
        /// Número de cuotas modificado
        /// </summary>
        public int? NroCuotasModificado { get; set; }
        /// <summary>
        /// Id tipo descuento original
        /// </summary>
        public int? IdTipoDescuentoOriginal { get; set; }
        /// <summary>
        /// Id tipo descuento modificado
        /// </summary>
        public int? IdTipoDescuentoModificado { get; set; }
        /// <summary>
        /// Id programa original
        /// </summary>
        public int? IdPgeneralOriginal { get; set; }
        /// <summary>
        /// Id programa modificado
        /// </summary>
        public int? IdPgeneralModificado { get; set; }
        /// <summary>
        /// Id tipo pago original
        /// </summary>
        public int? IdTipoPagoOriginal { get; set; }
        /// <summary>
        /// Id tipo pago modificado
        /// </summary>
        public int? IdTipoPagoModificado { get; set; }
        /// <summary>
        /// Id país original
        /// </summary>
        public int? IdPaisOriginal { get; set; }
        /// <summary>
        /// Id país modificado
        /// </summary>
        public int? IdPaisModificado { get; set; }
        /// <summary>
        /// Vencimiento original
        /// </summary>
        public string? VencimientoOriginal { get; set; }
        /// <summary>
        /// Vencimiento modificado
        /// </summary>
        public string? VencimientoModificado { get; set; }
        /// <summary>
        /// Primera cuota original
        /// </summary>
        public string? PrimeraCuotaOriginal { get; set; }
        /// <summary>
        /// Primera cuota modificada
        /// </summary>
        public string? PrimeraCuotaModificado { get; set; }
        /// <summary>
        /// Cuota doble original
        /// </summary>
        public bool? CuotaDobleOriginal { get; set; }
        /// <summary>
        /// Cuota doble modificada
        /// </summary>
        public bool? CuotaDobleModificado { get; set; }
        /// <summary>
        /// Descripción original
        /// </summary>
        public string? DescripcionOriginal { get; set; }
        /// <summary>
        /// Descripción modificada
        /// </summary>
        public string? DescripcionModificado { get; set; }
        /// <summary>
        /// Visible web original
        /// </summary>
        public bool? VisibleWebOriginal { get; set; }
        /// <summary>
        /// Visible web modificado
        /// </summary>
        public bool? VisibleWebModificado { get; set; }
        /// <summary>
        /// Paquete original
        /// </summary>
        public int? PaqueteOriginal { get; set; }
        /// <summary>
        /// Paquete modificado
        /// </summary>
        public int? PaqueteModificado { get; set; }
        /// <summary>
        /// Por defecto original
        /// </summary>
        public bool? PorDefectoOriginal { get; set; }
        /// <summary>
        /// Por defecto modificado
        /// </summary>
        public bool? PorDefectoModificado { get; set; }
        /// <summary>
        /// Monto descontado original
        /// </summary>
        public decimal? MontoDescontadoOriginal { get; set; }
        /// <summary>
        /// Monto descontado modificado
        /// </summary>
        public decimal? MontoDescontadoModificado { get; set; }
        /// <summary>
        /// Acción realizada por el usuario
        /// </summary>
        public string? Accion { get; set; }
        /// <summary>
        /// Estado (eliminación lógica)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creación
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificación
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creación
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificación
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// RowVersion (concurrencia)
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TMonedum? IdMonedaModificadoNavigation { get; set; }
        public virtual TMonedum IdMonedaOriginalNavigation { get; set; } = null!;
        public virtual TPai? IdPaisModificadoNavigation { get; set; }
        public virtual TPai? IdPaisOriginalNavigation { get; set; }
        public virtual TPgeneral? IdPgeneralModificadoNavigation { get; set; }
        public virtual TPgeneral? IdPgeneralOriginalNavigation { get; set; }
        public virtual TTipoDescuento? IdTipoDescuentoModificadoNavigation { get; set; }
        public virtual TTipoDescuento? IdTipoDescuentoOriginalNavigation { get; set; }
        public virtual TTipoPago? IdTipoPagoModificadoNavigation { get; set; }
        public virtual TTipoPago? IdTipoPagoOriginalNavigation { get; set; }
    }
}
