using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFacebookFormularioLeadgen
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// IdLeadgen
        /// </summary>
        public string IdLeadgenFacebook { get; set; } = null!;
        /// <summary>
        /// FechaCreacionLeadgen
        /// </summary>
        public DateTime? FechaCreacionFacebook { get; set; }
        /// <summary>
        /// IdCampaniaFacebook
        /// </summary>
        public string? IdCampanhaFacebook { get; set; }
        /// <summary>
        /// NombreCampaniaFacebook
        /// </summary>
        public string? NombreCampaniaFacebook { get; set; }
        /// <summary>
        /// CorreoElectronico, correo electronico del usuario de Facebook
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// NombreCompleto
        /// </summary>
        public string? NombreCompleto { get; set; }
        /// <summary>
        /// AreaFormacion, Area de formacion elegida
        /// </summary>
        public string? AreaFormacion { get; set; }
        /// <summary>
        /// Cargo, cargo del usuario
        /// </summary>
        public string? Cargo { get; set; }
        /// <summary>
        /// AreaTrabajo
        /// </summary>
        public string? AreaTrabajo { get; set; }
        /// <summary>
        /// Ciudad
        /// </summary>
        public string? Ciudad { get; set; }
        /// <summary>
        /// Telefono
        /// </summary>
        public string? Telefono { get; set; }
        /// <summary>
        /// Procesado, Estado del proceso de la creacion de una oportunidad
        /// </summary>
        public bool? EsProcesado { get; set; }
        /// <summary>
        /// Industria, industria indicada por el usuario
        /// </summary>
        public string? Industria { get; set; }
        /// <summary>
        /// InicioCapacitacion, Inicio de capacitacion indicada por el usuario
        /// </summary>
        public string? InicioCapacitacion { get; set; }
        /// <summary>
        /// Excepcion, Mensaje de excepcion cuando no se pudo procesar una asignacion automatica
        /// </summary>
        public string? Excepcion { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
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
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Id original de Facebook del anuncio
        /// </summary>
        public string? FacebookAnuncioId { get; set; }
        /// <summary>
        /// Id original de Facebook del anuncio
        /// </summary>
        public string? FacebookAnuncioNombre { get; set; }
        /// <summary>
        /// Plataforma de origen del lead (Facebook, Instagram, etc.)
        /// </summary>
        public string? Plataforma { get; set; }
    }
}
