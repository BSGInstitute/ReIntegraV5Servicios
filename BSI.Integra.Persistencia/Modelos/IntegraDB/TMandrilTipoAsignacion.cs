using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla clasifica los tipos de asignacion de mandriles
    /// </summary>
    public partial class TMandrilTipoAsignacion
    {
        public TMandrilTipoAsignacion()
        {
            TMandrilEnvioCorreoGestions = new HashSet<TMandrilEnvioCorreoGestion>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Registro del usuario para identificacion en la plataforma
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Usuario de creacion del registro
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

        public virtual ICollection<TMandrilEnvioCorreoGestion> TMandrilEnvioCorreoGestions { get; set; }
    }
}
