using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TReclamo
    {
        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key de T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Descripcion del reclamo
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Es foreing key de T_EstadoReclamo
        /// </summary>
        public int IdReclamoEstado { get; set; }
        /// <summary>
        /// Es foreing key de T_Origen
        /// </summary>
        public int IdOrigen { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string? UsuarioCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string? UsuarioModificacion { get; set; }
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
        /// FK de T_TipoReclamoAlumno
        /// </summary>
        public int? IdTipoReclamoAlumno { get; set; }
        /// <summary>
        /// Numero de dias de solucion
        /// </summary>
        public int? NroDiasSolucion { get; set; }
        /// <summary>
        /// FK de T_EstadoMatricula
        /// </summary>
        public int? IdEstadoMatriculaPrevio { get; set; }
        /// <summary>
        /// guarda la fecha en que se finalizo el reclamo
        /// </summary>
        public DateTime? FechaReclamoRealizadoFin { get; set; }
        public int IdCategoriaTicket { get; set; }
        /// <summary>
        /// Comentario Solucion Agenda Atención al al cliente
        /// </summary>
        public string? ComentarioSolucion { get; set; }
        /// <summary>
        /// Fecha Autoreprogramada cuando no hay contacto
        /// </summary>
        public DateTime? AutoReprogramacion { get; set; }

        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; } = null!;
        public virtual TOrigen IdOrigenNavigation { get; set; } = null!;
        public virtual TReclamoEstado IdReclamoEstadoNavigation { get; set; } = null!;
        public virtual TTipoReclamoAlumno? IdTipoReclamoAlumnoNavigation { get; set; }
    }
}
