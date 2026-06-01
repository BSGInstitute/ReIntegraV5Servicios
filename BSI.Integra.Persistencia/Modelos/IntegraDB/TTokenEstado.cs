using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catalogo de estados del ciclo de vida de tokens OAuth de Webex (Activo, Renovado, Revocado, Expirado).
    /// </summary>
    public partial class TTokenEstado
    {
        public TTokenEstado()
        {
            TWebexTokens = new HashSet<TWebexToken>();
        }

        /// <summary>
        /// Identificador unico del estado de token.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre descriptivo del estado (Activo, Renovado, Revocado, Expirado).
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción del estado del token.
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Estado logico del registro. 1=Activo, 0=Inactivo.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario que modifico el registro.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creacion del registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la ultima modificacion del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia optimista. Se actualiza automaticamente en cada modificacion.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TWebexToken> TWebexTokens { get; set; }
    }
}
