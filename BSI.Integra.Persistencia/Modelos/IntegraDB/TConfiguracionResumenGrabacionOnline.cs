using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la configuracion de resumenes de grabaciones online por sesiones
    /// </summary>
    public partial class TConfiguracionResumenGrabacionOnline
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es fk T_PEspecificoSesion
        /// </summary>
        public int IdPespecificoSesion { get; set; }
        /// <summary>
        /// Es fk T_ResumenGrabacionOnline
        /// </summary>
        public int IdResumenGrabacionOnline { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TResumenGrabacionOnline IdResumenGrabacionOnlineNavigation { get; set; } = null!;
    }
}
