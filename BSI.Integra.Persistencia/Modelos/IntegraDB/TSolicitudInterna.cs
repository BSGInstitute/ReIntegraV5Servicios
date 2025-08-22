using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSolicitudInterna
    {
        /// <summary>
        /// Pk de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id tabla solicitud
        /// </summary>
        public int IdSolicitud { get; set; }
        /// <summary>
        /// ID de tabla ope.T_Estado Solicitud
        /// </summary>
        public int IdEstadoSolicitud { get; set; }
        /// <summary>
        /// Id Personal solicitante
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Detalle de solicitud
        /// </summary>
        public string? DetalleSolicitud { get; set; }
        /// <summary>
        /// Tipo archivo
        /// </summary>
        public string? ContentTypeSolicitante { get; set; }
        /// <summary>
        /// Nombre archivo solicitante
        /// </summary>
        public string? NombreArchivoSolicitante { get; set; }
        /// <summary>
        /// Tipo de archivo Solucion
        /// </summary>
        public string? ContentTypeSolucion { get; set; }
        /// <summary>
        /// NombreArchivo Solucion
        /// </summary>
        public string? NombreArchivoSolucion { get; set; }
        /// <summary>
        /// Comentario de solucion solicitud
        /// </summary>
        public string? ComentarioSolucion { get; set; }
        /// <summary>
        /// Estado registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion registro automatico
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion registro automatico
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion registro automatico
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion registro automatico
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Version de registro automatica
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id tabla migrada 
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
        public virtual TSolicitud IdSolicitudNavigation { get; set; } = null!;
    }
}
