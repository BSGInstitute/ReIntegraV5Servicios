using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla registra los leads generados por formularios de Google
    /// </summary>
    public partial class TGoogleFormularioLeadgen
    {
        public TGoogleFormularioLeadgen()
        {
            TGoogleAdsConversionQueues = new HashSet<TGoogleAdsConversionQueue>();
            TOportunidadGoogleLeads = new HashSet<TOportunidadGoogleLead>();
        }

        /// <summary>
        /// Id del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id del lead de google
        /// </summary>
        public string? Lead { get; set; }
        /// <summary>
        /// Celular de la persona
        /// </summary>
        public string? Celular { get; set; }
        /// <summary>
        /// Email de la persona
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Pais de donde proviene el registro
        /// </summary>
        public string? Pais { get; set; }
        /// <summary>
        /// Nombre de la persona
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Apellidos de la persona
        /// </summary>
        public string? Apellidos { get; set; }
        /// <summary>
        /// Cargo de la persona
        /// </summary>
        public string? Cargo { get; set; }
        /// <summary>
        /// Area de Trabajo de la persona
        /// </summary>
        public string? AreaTrabajo { get; set; }
        /// <summary>
        /// Informacion sobre el area de formacion del usuario registrada
        /// </summary>
        public string? AreaFormacion { get; set; }
        /// <summary>
        /// Industria de la persona
        /// </summary>
        public string? Industria { get; set; }
        /// <summary>
        /// Version del api actual de google
        /// </summary>
        public string? VersionApi { get; set; }
        /// <summary>
        /// Id del formulario del que proviene el lead
        /// </summary>
        public string? FormularioGoogle { get; set; }
        /// <summary>
        /// Id de la campaña de donde proviene el lead
        /// </summary>
        public string? CampaniaGoogle { get; set; }
        /// <summary>
        /// Clave del hook por formulario
        /// </summary>
        public string? KeyGoogle { get; set; }
        /// <summary>
        /// Registra si el usuario realizo la prueba de conocimientos
        /// </summary>
        public bool EsTest { get; set; }
        /// <summary>
        /// Gcl de adwprds
        /// </summary>
        public string? Gcl { get; set; }
        /// <summary>
        /// Id del grupo de ads
        /// </summary>
        public string? GrupoAds { get; set; }
        /// <summary>
        /// Id de ads creativo
        /// </summary>
        public string? CreativoAds { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Campo de auditoria Usuario Creacion del registro
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

        public virtual ICollection<TGoogleAdsConversionQueue> TGoogleAdsConversionQueues { get; set; }
        public virtual ICollection<TOportunidadGoogleLead> TOportunidadGoogleLeads { get; set; }
    }
}
