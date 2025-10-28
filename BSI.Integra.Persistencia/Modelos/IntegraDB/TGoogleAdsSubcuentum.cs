using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de subcuentas de Google Ads. Permite gestionar múltiples subcuentas (Búsqueda, Remarketing, Display, etc.) bajo un mismo Manager Account, cada una con sus propias acciones de conversión configuradas.
    /// </summary>
    public partial class TGoogleAdsSubcuentum
    {
        public TGoogleAdsSubcuentum()
        {
            TGoogleAdsConversionQueues = new HashSet<TGoogleAdsConversionQueue>();
            TGoogleFormularioLeadgens = new HashSet<TGoogleFormularioLeadgen>();
        }

        /// <summary>
        /// Identificador único de la subcuenta
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Customer ID de Google Ads (sin guiones). Ejemplo: 5743207825 o 6421853601
        /// </summary>
        public string CustomerId { get; set; } = null!;
        /// <summary>
        /// Nombre descriptivo de la subcuenta. Ejemplos: Búsqueda, Remarketing, Display
        /// </summary>
        public string NombreSubcuenta { get; set; } = null!;
        /// <summary>
        /// Descripción detallada del propósito y uso de esta subcuenta
        /// </summary>
        public string? DescripcionSubcuenta { get; set; }
        /// <summary>
        /// ID de la acción de conversión para Interesado por Trabajar - IT (Fase 13). Formato: customers/{CustomerId}/conversionActions/{ActionId}
        /// </summary>
        public string? ConversionActionIdIt { get; set; }
        /// <summary>
        /// ID de la acción de conversión para Inscripción Proceso Pago Final - IP, PF (Fase 8, 22). Formato: customers/{CustomerId}/conversionActions/{ActionId}
        /// </summary>
        public string? ConversionActionIdIppf { get; set; }
        /// <summary>
        /// ID de la acción de conversión para Inscrito/Matriculado - IC, IS y M (Fase 5, 12, 23). Formato: customers/{CustomerId}/conversionActions/{ActionId}
        /// </summary>
        public string? ConversionActionIdIcism { get; set; }
        /// <summary>
        /// Referencia a la credencial OAuth compartida en T_AdworkCredencialApi
        /// </summary>
        public int IdAdworkCredencialApi { get; set; }
        /// <summary>
        /// Indica si la subcuenta está activa para procesamiento de conversiones
        /// </summary>
        public bool? Activo { get; set; }
        /// <summary>
        /// Estado del registro (activo o eliminado)
        /// </summary>
        public bool? Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de auditoría - RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TAdworkCredencialApi IdAdworkCredencialApiNavigation { get; set; } = null!;
        public virtual ICollection<TGoogleAdsConversionQueue> TGoogleAdsConversionQueues { get; set; }
        public virtual ICollection<TGoogleFormularioLeadgen> TGoogleFormularioLeadgens { get; set; }
    }
}
