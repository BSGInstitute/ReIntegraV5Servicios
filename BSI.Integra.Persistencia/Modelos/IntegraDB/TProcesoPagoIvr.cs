using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProcesoPagoIvr
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// (FK) a T_Alumno
        /// </summary>
        public int IdAlumno { get; set; }
        /// <summary>
        /// (FK) a integraDB_PortalWeb.dbo.T_TransaccionAuditoriaPago
        /// </summary>
        public int? IdTransaccionAuditoriaPago { get; set; }
        /// <summary>
        /// (FK) a T_Personal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Numero de celular del alumno en llamada
        /// </summary>
        public string? Celular { get; set; }
        /// <summary>
        /// Anexo del personal en llamada
        /// </summary>
        public string? Anexo { get; set; }
        /// <summary>
        /// Campo auditoria Estado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Campo auditoria UsuarioCreacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Campo auditoria UsuarioModificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo auditoria FechaCreacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Campo auditoria FechaModificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo auditoria RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
