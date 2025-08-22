using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProveedor
    {
        public TProveedor()
        {
            TConvocatoriaPersonals = new HashSet<TConvocatoriaPersonal>();
            TEsquemaEvaluacionPgeneralDetalles = new HashSet<TEsquemaEvaluacionPgeneralDetalle>();
            TEsquemaEvaluacionPgeneralProveedors = new HashSet<TEsquemaEvaluacionPgeneralProveedor>();
            TProveedorTipoServicios = new HashSet<TProveedorTipoServicio>();
            TSolicitudCertificadoFisicos = new HashSet<TSolicitudCertificadoFisico>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public int IdTipoContribuyente { get; set; }
        /// <summary>
        /// Llave foranea con T_DocumentoIdentidad
        /// </summary>
        public int IdDocumentoIdentidad { get; set; }
        public string NroDocIdentidad { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;
        public string Nombre1 { get; set; } = null!;
        public string Nombre2 { get; set; } = null!;
        public string ApePaterno { get; set; } = null!;
        public string ApeMaterno { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? IdCiudad { get; set; }
        public string? Telefono { get; set; }
        public string Email { get; set; } = null!;
        public string? Celular1 { get; set; }
        public string? Celular2 { get; set; }
        public string? Contacto1 { get; set; }
        public string? Contacto2 { get; set; }
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
        /// Llave foranea con l atabla T_PrestacionRegistro
        /// </summary>
        public int? IdPrestacionRegistro { get; set; }
        public bool? EsPersonaValida { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_TipoImpuesto
        /// </summary>
        public int? IdTipoImpuestoPreferido { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Retencion
        /// </summary>
        public int? IdRetencionPreferido { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Detraccion
        /// </summary>
        public int? IdDetraccionPreferido { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Personal, indica la persona que tiene asignado al docente para las coordinaciones
        /// </summary>
        public int? IdPersonalAsignado { get; set; }
        /// <summary>
        /// Alias para los docentes.
        /// </summary>
        public string? Alias { get; set; }

        public virtual ICollection<TConvocatoriaPersonal> TConvocatoriaPersonals { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralDetalle> TEsquemaEvaluacionPgeneralDetalles { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralProveedor> TEsquemaEvaluacionPgeneralProveedors { get; set; }
        public virtual ICollection<TProveedorTipoServicio> TProveedorTipoServicios { get; set; }
        public virtual ICollection<TSolicitudCertificadoFisico> TSolicitudCertificadoFisicos { get; set; }
    }
}
