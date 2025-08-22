using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSolicitudAlumno
    {
        /// <summary>
        /// Id de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// id de tabla T_EstadoSolicitud
        /// </summary>
        public int IdEstadoSolicitud { get; set; }
        /// <summary>
        /// Id de solicitante table T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// ID de tabla T_Solicitud
        /// </summary>
        public int IdSolicitud { get; set; }
        /// <summary>
        /// Id de tabla T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Id de tabla T_PEspecificod
        /// </summary>
        public int IdPespescifico { get; set; }
        /// <summary>
        /// Detalle de la solicitud
        /// </summary>
        public string? DetalleSolicitud { get; set; }
        /// <summary>
        /// Tipo de archivo solicitante
        /// </summary>
        public string? ContentTypeSolicitante { get; set; }
        /// <summary>
        /// Nombre d earchivo solicitante
        /// </summary>
        public string? NombreArchivoSolicitante { get; set; }
        /// <summary>
        /// Tipo de archivo Solucion
        /// </summary>
        public string? ContentTypeSolucion { get; set; }
        /// <summary>
        /// Nombre de archivo SOlucion
        /// </summary>
        public string? NombreArchivoSolucion { get; set; }
        /// <summary>
        /// Comentario de solucion de solicitud
        /// </summary>
        public string? ComentarioSolucion { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion de Registro Automatico
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion de registro automatico
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion de  registro automatico
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion de registro automatico
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Version de registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de tabla migrada
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// ID de tabla ope.T_ControlSolicitudOrigen
        /// </summary>
        public int? IdControlSolicitudOrigen { get; set; }

        public virtual TEstadoSolicitud IdEstadoSolicitudNavigation { get; set; } = null!;
        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; } = null!;
        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
        public virtual TPespecifico IdPespescificoNavigation { get; set; } = null!;
        public virtual TSolicitud IdSolicitudNavigation { get; set; } = null!;
    }
}
