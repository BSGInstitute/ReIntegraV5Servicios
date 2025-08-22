using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSolicitudCertificadoFisico
    {
        public TSolicitudCertificadoFisico()
        {
            TRegistroCertificadoFisicoGenerados = new HashSet<TRegistroCertificadoFisicoGenerado>();
        }

        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// clave foranea de la tabla fin.T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// clave foranea de la tabla gp.T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// clave foranea de la tabla fin.T_Fur
        /// </summary>
        public int? IdFur { get; set; }
        /// <summary>
        /// clave foranea de la tabla fin.T_Proveedor
        /// </summary>
        public int? IdProveedor { get; set; }
        /// <summary>
        /// Fecha de Solicitud del certificado
        /// </summary>
        public DateTime FechaSolicitud { get; set; }
        /// <summary>
        /// Fecha en la que se estima se entregara el certificado
        /// </summary>
        public DateTime? FechaEntregaEstimada { get; set; }
        /// <summary>
        /// Fecha de cuando se realizo la entrega del certificado
        /// </summary>
        public DateTime? FechaEntregaReal { get; set; }
        /// <summary>
        /// Codigo de seguimiento proporcionado por el proveedor
        /// </summary>
        public string? CodigoSeguimientoEnvio { get; set; }
        /// <summary>
        /// clave foranea de la tabla mkt.T_EstadoEntregaCertificadoFisico
        /// </summary>
        public int IdEstadoCertificadoFisico { get; set; }
        /// <summary>
        /// Clave Foranea de la tabla pla.T_CertificadoGeneradoAutomatico
        /// </summary>
        public int IdCertificadoGeneradoAutomatico { get; set; }
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
        /// clave foranea de la tabla pla.T_Courier
        /// </summary>
        public int? IdCourier { get; set; }
        /// <summary>
        /// Fecha de entrega courier
        /// </summary>
        public DateTime? FechaEntregaCourier { get; set; }
        /// <summary>
        /// Estado de la entrega al servicio courier
        /// </summary>
        public string? EstadoCourier { get; set; }
        /// <summary>
        /// clave foranea de la tabla TPais
        /// </summary>
        public int? IdPaisConsignado { get; set; }
        /// <summary>
        /// clave foranea de la tabla TCiudad
        /// </summary>
        public int? IdCiudadConsignada { get; set; }
        /// <summary>
        /// Codigo de seguimiento
        /// </summary>
        public string? CodigoSeguimiento { get; set; }

        public virtual TEstadoCertificadoFisico IdEstadoCertificadoFisicoNavigation { get; set; } = null!;
        public virtual TFur? IdFurNavigation { get; set; }
        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; } = null!;
        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
        public virtual TProveedor? IdProveedorNavigation { get; set; }
        public virtual ICollection<TRegistroCertificadoFisicoGenerado> TRegistroCertificadoFisicoGenerados { get; set; }
    }
}
