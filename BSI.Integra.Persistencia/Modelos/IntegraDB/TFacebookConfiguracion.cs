using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena la configuracion de las paginas de Facebook de BSG Institute. Incluye identificador de pagina, nombre, token de acceso, total de opiniones, valoracion promedio y control de
    ///   visibilidad en frontend
    /// </summary>
    public partial class TFacebookConfiguracion
    {
        public TFacebookConfiguracion()
        {
            TFacebookResenas = new HashSet<TFacebookResena>();
        }

        /// <summary>
        /// Identificador unico de la configuracion de pagina de Facebook (PK)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador unico de la pagina de Facebook (Page ID)
        /// </summary>
        public string IdentificadorPagina { get; set; } = null!;
        /// <summary>
        /// Nombre de la pagina de Facebook
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Token de acceso de la pagina de Facebook para consumir la Graph API
        /// </summary>
        public string? TokenAccesoPagina { get; set; }
        /// <summary>
        /// Total de opiniones recibidas en la pagina de Facebook. Se congela este campo en cada sincronización.
        /// </summary>
        public int ResenaTotal { get; set; }
        /// <summary>
        /// Valoracion promedio de la pagina en escala de 1.00 a 5.00
        /// </summary>
        public decimal Valoracion { get; set; }
        /// <summary>
        /// Controla si la pagina se muestra en el Homepage/Frontend (1=Visible, 0=Oculta)
        /// </summary>
        public bool Mostrar { get; set; }
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

        public virtual ICollection<TFacebookResena> TFacebookResenas { get; set; }
    }
}
