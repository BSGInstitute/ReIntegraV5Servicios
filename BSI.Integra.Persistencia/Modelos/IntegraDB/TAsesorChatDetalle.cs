using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAsesorChatDetalle
    {
        /// <summary>
        /// Clave primeria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk de AsesorChat de la tabla TCRM_AsesoresChats
        /// </summary>
        public int? IdAsesorChat { get; set; }
        /// <summary>
        /// Id de Pais de la tabla TCRM_Pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Id del Programa General de la tabla TPLA_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Estado , validad si esta activo o no
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario que modifico el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
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
