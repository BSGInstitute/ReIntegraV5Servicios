using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TChatIntegraHistorialAsesor
    {
        /// <summary>
        /// Clave de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de relacion con la tabla TCRM_AsesoresChatsDetalles
        /// </summary>
        public int IdAsesorChatDetalle { get; set; }
        /// <summary>
        /// Id asesor de la tabla tpersonal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Fecha que se asigno el chat a un asesor
        /// </summary>
        public DateTime FechaAsignacion { get; set; }
        /// <summary>
        /// Valida estado del registro activo o no
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de  modificacion del registro
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
    }
}
