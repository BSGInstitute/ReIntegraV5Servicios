using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMatriculaCabeceraBeneficio
    {
        /// <summary>
        /// Clave Primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave Foranea de la tabla fin.T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// decripcion del beneficio
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// clave foranea de la tabla pla.T_SuscripcionProgramaGeneral
        /// </summary>
        public int IdSuscripcionProgramaGeneral { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
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
        /// id de la tabla original
        /// </summary>
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// clave foranea de la tabla pla.T_ConfiguracionBeneficioProgramaGeneral
        /// </summary>
        public int? IdConfiguracionBeneficioProgramaGeneral { get; set; }
        /// <summary>
        /// Clave foranea de la tabla com.T_EstadoMatriculaCabeceraBeneficio
        /// </summary>
        public int? IdEstadoMatriculaCabeceraBeneficio { get; set; }
        /// <summary>
        /// Fecha en la que se solicita el beneficio
        /// </summary>
        public DateTime? FechaSolicitud { get; set; }
        /// <summary>
        /// Es foreign key de la tabla pla.T_EstadoSolicitudBeneficio
        /// </summary>
        public int? IdEstadoSolicitudBeneficio { get; set; }
        /// <summary>
        /// Duracion de programa matriculado
        /// </summary>
        public int? Duracion { get; set; }
        /// <summary>
        /// Fecha de entrega del beneficio (cuando se le dio clic al boton de entregar)
        /// </summary>
        public DateTime? FechaEntrega { get; set; }
        /// <summary>
        /// Fecha en la que se debe de entregar el beneficio (FechaAprobacion + 30 dias)
        /// </summary>
        public DateTime? FechaProgramada { get; set; }
        /// <summary>
        /// Fecha en la que la coordinadora aprueba el beneficio
        /// </summary>
        public DateTime? FechaAprobacion { get; set; }
        /// <summary>
        /// Almacena el nombre del usuario
        /// </summary>
        public string? UsuarioAprobacion { get; set; }
        /// <summary>
        /// es el usuario que entregóbeneficio
        /// </summary>
        public string? UsuarioEntregoBeneficio { get; set; }
    }
}
