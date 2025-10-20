using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la vinculación entre oportunidades activas y formularios de Google Lead Generation, permitiendo rastrear qué oportunidades comerciales provienen de campañas de Google Ads
    /// </summary>
    public partial class TOportunidadGoogleLead
    {
        /// <summary>
        /// Id del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de la oportunidad asociada
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// Id del formulario de Google del que proviene el lead
        /// </summary>
        public int IdGoogleFormularioLeadgen { get; set; }
        /// <summary>
        /// Id del alumno asociado
        /// </summary>
        public int IdAlumno { get; set; }
        /// <summary>
        /// Fecha en que se realizó la vinculación entre oportunidad y formulario
        /// </summary>
        public DateTime FechaVinculacion { get; set; }
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
        /// Campo de auditoria Rowversion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TAlumno IdAlumnoNavigation { get; set; } = null!;
        public virtual TGoogleFormularioLeadgen IdGoogleFormularioLeadgenNavigation { get; set; } = null!;
        public virtual TOportunidad IdOportunidadNavigation { get; set; } = null!;
    }
}
