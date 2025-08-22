using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMatriculaCabeceraDatosCertificadoMensaje
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la tabla TMatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Identificador de la tabla TPersonal del remitente del mensaje
        /// </summary>
        public int IdPersonalRemitente { get; set; }
        /// <summary>
        /// Identificador de la tabla TPersonal del receptor del mensaje
        /// </summary>
        public int IdPersonalReceptor { get; set; }
        /// <summary>
        /// mensaje
        /// </summary>
        public string Mensaje { get; set; } = null!;
        /// <summary>
        /// Valor anterior del registro modificado
        /// </summary>
        public string ValorAntiguo { get; set; } = null!;
        /// <summary>
        /// Valor nuevo del registro modificado
        /// </summary>
        public string ValorNuevo { get; set; } = null!;
        /// <summary>
        /// Estado de lectura del mensaje(leido o pendiente)
        /// </summary>
        public bool EstadoMensaje { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
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
