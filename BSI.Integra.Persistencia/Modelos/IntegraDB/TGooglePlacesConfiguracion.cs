using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena la configuración de Google Places por sede de BSG Institute, incluyendo el PlaceId de Google Maps necesario para consultar reseñas vía Places API
    /// </summary>
    public partial class TGooglePlacesConfiguracion
    {
        public TGooglePlacesConfiguracion()
        {
            TGoogleResenas = new HashSet<TGoogleResena>();
        }

        /// <summary>
        /// Identificador único de la configuración de sede (PK)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre descriptivo de la sede de BSG Institute (ej: Miraflores, Arequipa, México, Colombia)
        /// </summary>
        public string NombreSede { get; set; } = null!;
        /// <summary>
        /// Google Place ID único de la sede en Google Maps, usado para consultar reseñas vía Places API (ej: ChIJrwMeWYbIBRER...)
        /// </summary>
        public string IdentificadorCuenta { get; set; } = null!;
        /// <summary>
        /// Valoracion promedio de la pagina en escala de 1.00 a 5.00. Se congela este campo en cada sincronización.
        /// </summary>
        public decimal Valoracion { get; set; }
        /// <summary>
        /// Total de reseñas acumuladas en el perfil de empleador de Google Places. Se congela este campo en cada sincronización.
        /// </summary>
        public int ResenaTotal { get; set; }
        /// <summary>
        /// Controla si la cuenta se muestra en el Homepage/Frontend (1=Visible, 0=Oculta)
        /// </summary>
        public bool Mostrar { get; set; }
        /// <summary>
        /// Estado lógico del registro (1=Activo, 0=Inactivo)
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

        public virtual ICollection<TGoogleResena> TGoogleResenas { get; set; }
    }
}
