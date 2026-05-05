using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena la configuracion de cuenta de empleador en LinkedIn con nombre de la empresa, enlace del perfil publico y total de opiniones acumuladas. La captura es manual y periodica debido a que LinkedIn no expone una API publica para la extraccion de resenas de empleador
    /// </summary>
    public partial class TLinkedinConfiguracion
    {
        public TLinkedinConfiguracion()
        {
            TLinkedinResenas = new HashSet<TLinkedinResena>();
        }

        /// <summary>
        /// Identificador unico de la configuracion de LinkedIn (PK)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la empresa tal como se muestra en el perfil de LinkedIn
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// URL publica del perfil de empleador en LinkedIn
        /// </summary>
        public string? EnlacePagina { get; set; }
        /// <summary>
        /// Total de opiniones acumuladas en el perfil de empleador de LinkedIn. Se congela este campo en cada sincronización.
        /// </summary>
        public int ResenaTotal { get; set; }
        /// <summary>
        /// Estado logico del registro (1=Activo, 0=Eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modifico por ultima vez el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la ultima modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de control de versiones (timestamp automatico)
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TLinkedinResena> TLinkedinResenas { get; set; }
    }
}
