using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFur
    {
        public TFur()
        {
            TMaterialPespecificoDetalles = new HashSet<TMaterialPespecificoDetalle>();
            TSolicitudCertificadoFisicos = new HashSet<TSolicitudCertificadoFisico>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Codigo t fur
        /// </summary>
        public string? Codigo { get; set; }
        /// <summary>
        /// Es Foreing Key T_PEspecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Identificador de Area
        /// </summary>
        public int? IdPersonalAreaTrabajo { get; set; }
        /// <summary>
        /// POR DEFINIR
        /// </summary>
        public int IdCiudad { get; set; }
        /// <summary>
        /// Numero fur
        /// </summary>
        public string? NumeroFur { get; set; }
        /// <summary>
        /// Numero de semana
        /// </summary>
        public int? NumeroSemana { get; set; }
        /// <summary>
        /// Usuario que solicita
        /// </summary>
        public string? UsuarioSolicitud { get; set; }
        /// <summary>
        /// Usuario que pide
        /// </summary>
        public string? UsuarioAutoriza { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string? Observaciones { get; set; }
        /// <summary>
        /// Es Foreing Key tProveedor
        /// </summary>
        public int? IdProveedor { get; set; }
        /// <summary>
        /// Es Foreing Key tProducto
        /// </summary>
        public int? IdProducto { get; set; }
        /// <summary>
        /// Cantidad
        /// </summary>
        public decimal Cantidad { get; set; }
        /// <summary>
        /// Monto
        /// </summary>
        public decimal? Monto { get; set; }
        /// <summary>
        /// Nombre unidad
        /// </summary>
        public int? IdProductoPresentacion { get; set; }
        /// <summary>
        /// Es Foreing Key tCentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Llave Foranea con T_Moneda
        /// </summary>
        public int? IdMonedaProveedor { get; set; }
        /// <summary>
        /// Numero de cuenta
        /// </summary>
        public string NumeroCuenta { get; set; } = null!;
        /// <summary>
        /// Numero de recibo
        /// </summary>
        public string? NumeroRecibo { get; set; }
        /// <summary>
        /// Pago Moneda Origen
        /// </summary>
        public decimal? PagoMonedaOrigen { get; set; }
        /// <summary>
        /// Pago en dolares
        /// </summary>
        public decimal? PagoDolares { get; set; }
        /// <summary>
        /// Fecha cobro a bancos
        /// </summary>
        public DateTime? FechaCobroBanco { get; set; }
        /// <summary>
        /// Responsable del cobro
        /// </summary>
        public string? ResponsableCobro { get; set; }
        /// <summary>
        /// Fecha de pago
        /// </summary>
        public DateTime? FechaPago { get; set; }
        /// <summary>
        /// Cuenta contable
        /// </summary>
        public string Cuenta { get; set; } = null!;
        /// <summary>
        /// Descripcion
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Precio Unitario en Moneda Origen
        /// </summary>
        public decimal PrecioUnitarioMonedaOrigen { get; set; }
        /// <summary>
        /// Precio unitario dolares
        /// </summary>
        public decimal PrecioUnitarioDolares { get; set; }
        /// <summary>
        /// Precio Total en Moneda Origen
        /// </summary>
        public decimal PrecioTotalMonedaOrigen { get; set; }
        /// <summary>
        /// Precio total dolares
        /// </summary>
        public decimal PrecioTotalDolares { get; set; }
        /// <summary>
        /// Aprobados primera fase
        /// </summary>
        public int IdFurFaseAprobacion1 { get; set; }
        /// <summary>
        /// Aprobados segunda fase
        /// </summary>
        public int? AprobadoFase2 { get; set; }
        /// <summary>
        /// Fecha limite
        /// </summary>
        public DateTime? FechaLimite { get; set; }
        /// <summary>
        /// Id tipo pedido
        /// </summary>
        public int IdFurTipoPedido { get; set; }
        /// <summary>
        /// Estado cancelado
        /// </summary>
        public bool Cancelado { get; set; }
        /// <summary>
        /// Estado antiguo
        /// </summary>
        public int? Antiguo { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Moneda, Moneda que se slecciona al crear el Fur
        /// </summary>
        public int? IdMonedaPagoReal { get; set; }
        /// <summary>
        /// Estado ocupado solicitud
        /// </summary>
        public bool? OcupadoSolicitud { get; set; }
        /// <summary>
        /// Estado ocupado rendicion
        /// </summary>
        public bool? OcupadoRendicion { get; set; }
        /// <summary>
        /// 1 cuando el fur esta aprobado y 0 cuando esta observado
        /// </summary>
        public bool EstadoAprobadoObservado { get; set; }
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
        /// Llave foranea con la tabla T_TipoPago
        /// </summary>
        public int? IdMonedaPagoRealizado { get; set; }
        /// <summary>
        /// Fecha de Probacion por el Jefe de Finanzas o el jefe final
        /// </summary>
        public DateTime? FechaAprobacionProcesoCulminado { get; set; }
        /// <summary>
        /// Monto proyectado del registro
        /// </summary>
        public decimal? MontoProyectado { get; set; }
        /// <summary>
        /// Indica que el pago del Fur sera diferido o no
        /// </summary>
        public bool? EsDiferido { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_FurSubFaseAprobacion
        /// </summary>
        public int? IdFurSubFaseAprobacion { get; set; }
        /// <summary>
        /// Es la fecha de reprogramada en caso no se cumpla con la fecha limite
        /// </summary>
        public DateTime? FechaLimiteReprogramacion { get; set; }
        public int? IdEmpresa { get; set; }

        public virtual TEmpresa? IdEmpresaNavigation { get; set; }
        public virtual ICollection<TMaterialPespecificoDetalle> TMaterialPespecificoDetalles { get; set; }
        public virtual ICollection<TSolicitudCertificadoFisico> TSolicitudCertificadoFisicos { get; set; }
    }
}
