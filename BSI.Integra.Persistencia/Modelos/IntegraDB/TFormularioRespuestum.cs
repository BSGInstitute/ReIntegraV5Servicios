using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFormularioRespuestum
    {
        /// <summary>
        /// Clave Primaria de La Tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre formulario respuesta
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Codigo formulario respuesta
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Clave Foranea de la Tabla TPGeneral o IdPGeneral de la Tabla TPLA_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Nombre programa general
        /// </summary>
        public string ProgramaGeneral { get; set; } = null!;
        /// <summary>
        /// Resumen del programa general
        /// </summary>
        public string? ResumenProgramaGeneral { get; set; }
        /// <summary>
        /// Color texto titulo
        /// </summary>
        public string? ColorTextoPgeneral { get; set; }
        /// <summary>
        /// Color texto descripcion
        /// </summary>
        public string? ColorTextoDescripcionPgeneral { get; set; }
        /// <summary>
        /// Color texto brochure
        /// </summary>
        public string? ColorTextoInvitacionBrochure { get; set; }
        /// <summary>
        /// Texto del boton brochure
        /// </summary>
        public string? TextoBotonBrochure { get; set; }
        /// <summary>
        /// Color fondo boton brochure
        /// </summary>
        public string? ColorFondoBotonBrochure { get; set; }
        /// <summary>
        /// Color text boton brochure
        /// </summary>
        public string? ColorTextoBotonBrochure { get; set; }
        /// <summary>
        /// Color borde inferior brochure
        /// </summary>
        public string? ColorBordeInferiorBotonBrochure { get; set; }
        /// <summary>
        /// Color text boton invitacion
        /// </summary>
        public string? ColorTextoBotonInvitacion { get; set; }
        /// <summary>
        /// Color fondo boton invitacion
        /// </summary>
        public string? ColorFondoBotonInvitacion { get; set; }
        /// <summary>
        /// Fondo Boton lado invitacion
        /// </summary>
        public string? FondoBotonLadoInvitacion { get; set; }
        /// <summary>
        /// Url de la imagen invitacion
        /// </summary>
        public string? UrlImagenLadoInvitacion { get; set; }
        /// <summary>
        /// Texto boton inivitacion pagina
        /// </summary>
        public string? TextoBotonInvitacionPagina { get; set; }
        /// <summary>
        /// Url boton invitacion pagina
        /// </summary>
        public string? UrlBotonInvitacionPagina { get; set; }
        /// <summary>
        /// Texto boton inivitacion area
        /// </summary>
        public string? TextoBotonInvitacionArea { get; set; }
        /// <summary>
        /// Url boton invitacion area
        /// </summary>
        public string? UrlBotonInvitacionArea { get; set; }
        /// <summary>
        /// Seccion telefono
        /// </summary>
        public string? ContenidoSeccionTelefonos { get; set; }
        /// <summary>
        /// Clave Foranea de la tabla T_FormularioRespuestaPlantilla
        /// </summary>
        public int? IdFormularioRespuestaPlantilla { get; set; }
        /// <summary>
        /// Url del brochure
        /// </summary>
        public string? Urlbrochure { get; set; }
        /// <summary>
        /// Url del logotipo
        /// </summary>
        public string? Urllogotipo { get; set; }
        /// <summary>
        /// Texto invitacion del brochure
        /// </summary>
        public string? TextoInvitacionBrochure { get; set; }
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
    }
}
