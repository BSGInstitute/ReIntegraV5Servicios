using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TRecordatorioWebinarIvr
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// PK  de  T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Maximo de  Intentos
        /// </summary>
        public int IntentoMaximo { get; set; }
        /// <summary>
        /// Número de intento
        /// </summary>
        public int Intento { get; set; }
        /// <summary>
        /// Cuando la llamada fue contestada o llegó a su máximo intento
        /// </summary>
        public bool Concluido { get; set; }
        /// <summary>
        /// Cuando un hilo de IVR tomo el registro.
        /// </summary>
        public bool Ejecutado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
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
