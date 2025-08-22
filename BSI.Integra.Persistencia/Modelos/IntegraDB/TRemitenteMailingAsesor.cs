using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TRemitenteMailingAsesor
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla tpla_RemitenteMailing
        /// </summary>
        public int IdRemitenteMailing { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        public string NombreCompleto { get; set; } = null!;
        /// <summary>
        /// Direccion de Correo Electronica
        /// </summary>
        public string CorreoElectronico { get; set; } = null!;
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
        /// Sistema Automatico Fecha creacion
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
        /// Id de sender asociado a SendinBlue
        /// </summary>
        public int? IdSenderSendinBlue { get; set; }
    }
}
