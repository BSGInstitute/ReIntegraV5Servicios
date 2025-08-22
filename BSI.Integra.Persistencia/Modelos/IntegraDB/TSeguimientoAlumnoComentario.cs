using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSeguimientoAlumnoComentario
    {
        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave Foranea de la Tabla fin.T_matriculaCabecera
        /// </summary>
        public int? IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Numero de cuota de pago
        /// </summary>
        public int NroCuota { get; set; }
        /// <summary>
        /// Numero de sub cuota de pago
        /// </summary>
        public int NroSubCuota { get; set; }
        /// <summary>
        /// clave foranea de la tabla ope.T_SeguimientoAlumnoCategoria
        /// </summary>
        public int IdSeguimientoAlumnoCategoria { get; set; }
        /// <summary>
        /// clave foranea de la tabla gp.t_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// clave foranea de la tabla com.T_Oportunidad
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// Comentario de seguimiento 
        /// </summary>
        public string? Comentario { get; set; }
        /// <summary>
        /// Fecha Compromiso proxima Actividad
        /// </summary>
        public DateTime? FechaCompromiso { get; set; }
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
    }
}
