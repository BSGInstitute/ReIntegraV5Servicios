using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla contiene informacion acerca de envios de correos
    /// </summary>
    public partial class TMandrilEnvioCorreoGestion
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK a T_GestionContacto
        /// </summary>
        public int? IdGestionContacto { get; set; }
        /// <summary>
        /// FK a T_CentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Fk a T_Personal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// FK a T_ClasificacionPersona
        /// </summary>
        public int? IdClasificacionPersona { get; set; }
        /// <summary>
        /// Fk a T_MandrilTipoAsignacion
        /// </summary>
        public int IdMandrilTipoAsignacion { get; set; }
        /// <summary>
        /// Registro del estado de envio de correos en la base de datos
        /// </summary>
        public int? EstadoEnvio { get; set; }
        /// <summary>
        /// FK a T_MandrilTipoEnvio
        /// </summary>
        public int IdMandrilTipoEnvio { get; set; }
        /// <summary>
        /// Contenido breve que describe el motivo del correo electronico enviado
        /// </summary>
        public string? Asunto { get; set; }
        /// <summary>
        /// Registro de fechas en las que se enviaron correos electronicos
        /// </summary>
        public DateTime? FechaEnvio { get; set; }
        /// <summary>
        /// FK asignada por Mandril
        /// </summary>
        public string? FkMandril { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Indica si el correo es parte de un envio masivo
        /// </summary>
        public bool EsEnvioMasivo { get; set; }

        public virtual TCentroCosto? IdCentroCostoNavigation { get; set; }
        public virtual TClasificacionPersona? IdClasificacionPersonaNavigation { get; set; }
        public virtual TGestionContacto? IdGestionContactoNavigation { get; set; }
        public virtual TMandrilTipoAsignacion IdMandrilTipoAsignacionNavigation { get; set; } = null!;
        public virtual TMandrilTipoEnvio IdMandrilTipoEnvioNavigation { get; set; } = null!;
        public virtual TPersonal? IdPersonalNavigation { get; set; }
    }
}
