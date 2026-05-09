using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena las resenas individuales de empleador publicadas en Glassdoor, vinculadas a la configuracion de empleador correspondiente. Incluye control de visibilidad en frontend, auditoria completa y referencias a pais y ciudad. Captura manual periodica por descontinuacion de API en 2023
    /// </summary>
    public partial class TGlassdoorResena
    {
        /// <summary>
        /// Identificador unico de la resena de Glassdoor (PK)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK hacia la configuracion de empleador de Glassdoor (mkt.T_GlassdoorConfiguracion)
        /// </summary>
        public int IdGlassdoorConfiguracion { get; set; }
        /// <summary>
        /// Titulo o encabezado de la resena publicada en Glassdoor
        /// </summary>
        public string Titulo { get; set; } = null!;
        /// <summary>
        /// Contenido de la resena publicada en Glassdoor
        /// </summary>
        public string? Contenido { get; set; }
        /// <summary>
        /// Calificacion otorgada por el autor en escala 1 a 5 estrellas
        /// </summary>
        public int Valoracion { get; set; }
        /// <summary>
        /// Cargo o puesto declarado por el autor de la resena
        /// </summary>
        public string? Cargo { get; set; }
        /// <summary>
        /// Tipo de vinculo laboral del autor al momento de publicar la resena (Actual / Anterior)
        /// </summary>
        public string? TipoEmpleado { get; set; }
        /// <summary>
        /// Aspectos positivos declarados por el autor en la resena
        /// </summary>
        public string? Ventaja { get; set; }
        /// <summary>
        /// Aspectos negativos declarados por el autor en la resena
        /// </summary>
        public string? Desventaja { get; set; }
        /// <summary>
        /// FK hacia la ciudad de origen de la resena (conf.T_Ciudad)
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// Fecha de publicacion de la resena en Glassdoor
        /// </summary>
        public DateTime? FechaResena { get; set; }
        /// <summary>
        /// Controla si la resena se muestra en el Homepage/Frontend (1=Visible, 0=Oculta)
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

        public virtual TCiudad? IdCiudadNavigation { get; set; }
        public virtual TGlassdoorConfiguracion IdGlassdoorConfiguracionNavigation { get; set; } = null!;
    }
}
