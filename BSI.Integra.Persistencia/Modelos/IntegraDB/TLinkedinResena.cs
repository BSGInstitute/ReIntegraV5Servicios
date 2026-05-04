using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena las reseñas y recomendaciones de LinkedIn, con control de visibilidad en el frontend y auditoría completa
    /// </summary>
    public partial class TLinkedinResena
    {
        /// <summary>
        /// Identificador único de la reseña de LinkedIn (PK)
        /// </summary>
        public int Id { get; set; }
        public int IdLinkedinConfiguracion { get; set; }
        /// <summary>
        /// Nombre completo del autor de la reseña en LinkedIn
        /// </summary>
        public string NombreAutor { get; set; } = null!;
        /// <summary>
        /// Cargo o posición profesional del autor en el momento de la reseña
        /// </summary>
        public string? Cargo { get; set; }
        /// <summary>
        /// Empresa o institución donde trabaja el autor al momento de la reseña
        /// </summary>
        public string? Empresa { get; set; }
        /// <summary>
        /// URI de la foto de perfil del autor en LinkedIn
        /// </summary>
        public string? FotoAutor { get; set; }
        /// <summary>
        /// URL de la publicación o recomendación original en LinkedIn
        /// </summary>
        public string? UrlPublicacion { get; set; }
        /// <summary>
        /// Texto completo de la reseña o recomendación escrita por el autor
        /// </summary>
        public string? TextoResena { get; set; }
        /// <summary>
        /// Nombre del certificado o curso de BSG Institute que el autor reseña o recomienda
        /// </summary>
        public string? Certificacion { get; set; }
        /// <summary>
        /// FK hacia la tabla de países (referencia al país del autor)
        /// </summary>
        public int? IdPais { get; set; }
        /// <summary>
        /// FK hacia la tabla de ciudades (referencia a la ciudad del autor)
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// Fecha en que el autor publicó la reseña o recomendación en LinkedIn
        /// </summary>
        public DateTime? FechaResena { get; set; }
        /// <summary>
        /// Controla si la reseña se muestra en el Homepage/Frontend (1=Visible, 0=Oculta)
        /// </summary>
        public bool Mostrar { get; set; }
        /// <summary>
        /// Estado lógico del registro (1=Activo, 0=Eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó por última vez el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de control de versiones (timestamp automático)
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TLinkedinConfiguracion IdLinkedinConfiguracionNavigation { get; set; } = null!;
    }
}
