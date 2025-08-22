using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TExpositor
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key de la tabla T_TipoDocumento en el campo &apos;clave&apos;
        /// </summary>
        public int IdTipoDocumento { get; set; }
        /// <summary>
        /// Numero de documento de identidad
        /// </summary>
        public string? NroDocumento { get; set; }
        /// <summary>
        /// Primer nombre del docente 
        /// </summary>
        public string? PrimerNombre { get; set; }
        /// <summary>
        /// Segundo nombre del docente
        /// </summary>
        public string? SegundoNombre { get; set; }
        /// <summary>
        /// Apellido paterno del docente
        /// </summary>
        public string? ApellidoPaterno { get; set; }
        /// <summary>
        /// Apellido materno del docente
        /// </summary>
        public string? ApellidoMaterno { get; set; }
        /// <summary>
        /// Fecha de nacimiento del docente
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }
        /// <summary>
        /// Pais de procedencia (Identificador de la columna CodigoPais de la tabla T_Pais)
        /// </summary>
        public int? IdPaisProcedencia { get; set; }
        /// <summary>
        /// Es foreing key T_Ciudad
        /// </summary>
        public int? IdCiudadProcedencia { get; set; }
        /// <summary>
        /// Es Foreign Key de T_Expositor
        /// </summary>
        public int? IdReferidoPor { get; set; }
        /// <summary>
        /// Numero del telefono celular
        /// </summary>
        public string? TelfCelular1 { get; set; }
        /// <summary>
        /// Numero del telefono celular
        /// </summary>
        public string? TelfCelular2 { get; set; }
        /// <summary>
        /// Numero del telefono celular
        /// </summary>
        public string? TelfCelular3 { get; set; }
        /// <summary>
        /// Email de contacto 
        /// </summary>
        public string? Email1 { get; set; }
        /// <summary>
        /// Email de contacto 
        /// </summary>
        public string? Email2 { get; set; }
        /// <summary>
        /// Email de contacto 
        /// </summary>
        public string? Email3 { get; set; }
        /// <summary>
        /// Direccion del domicio del docente
        /// </summary>
        public string? Domicilio { get; set; }
        /// <summary>
        /// Pais del domicilio (Identificador de la columna CodigoPais de la tabla T_Pais)
        /// </summary>
        public int? IdPaisDomicilio { get; set; }
        /// <summary>
        /// Ciudad del domicilio (identificador de la tabla T_Ciudad)
        /// </summary>
        public int? IdCiudadDomicilio { get; set; }
        /// <summary>
        /// Nombre del lugar de trabajo
        /// </summary>
        public string? LugarTrabajo { get; set; }
        /// <summary>
        /// Pais del lugar del trabajo (Identificador de la columna CodigoPais de la tabla T_Pais)
        /// </summary>
        public int? IdPaisLugarTrabajo { get; set; }
        /// <summary>
        /// Ciudad del lugar de trabajo (identificador de la tabla T_Ciudad)
        /// </summary>
        public int? IdCiudadLugarTrabajo { get; set; }
        /// <summary>
        /// Nombre de su asistente
        /// </summary>
        public string? AsistenteNombre { get; set; }
        /// <summary>
        /// Telefono de su asistente
        /// </summary>
        public string? AsistenteTelefono { get; set; }
        /// <summary>
        /// Telefono celular de su asistente
        /// </summary>
        public string? AsistenteCelular { get; set; }
        /// <summary>
        /// Resumen del perfil de la hoja de vida del docente
        /// </summary>
        public string? HojaVidaResumidaPerfil { get; set; }
        /// <summary>
        /// Resumen de discurso de hoja de vida
        /// </summary>
        public string? HojaVidaResumidaSpeech { get; set; }
        /// <summary>
        /// Descripcion de la formacion academica
        /// </summary>
        public string? FormacionAcademica { get; set; }
        /// <summary>
        /// Descripcion de la experiencia profesional
        /// </summary>
        public string? ExperienciaProfesional { get; set; }
        /// <summary>
        /// Descripcion de las publicaciones hechas por el docente 
        /// </summary>
        public string? Publicaciones { get; set; }
        /// <summary>
        /// Descripcion de premios o distinciones 
        /// </summary>
        public string? PremiosDistinciones { get; set; }
        /// <summary>
        /// Descripcion de informacion adicional del docente
        /// </summary>
        public string? OtraInformacion { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
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
        /// Indica el id de la tabla migrada
        /// </summary>
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Indica si la persona es valida o no
        /// </summary>
        public bool? EsPersonaValida { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Personal
        /// </summary>
        public int? IdPersonalAsignado { get; set; }
        /// <summary>
        /// Nombre de la imagen que se cargara del docente
        /// </summary>
        public string? FotoDocente { get; set; }
        /// <summary>
        /// Url de la imagen que se cargara del docente
        /// </summary>
        public string? UrlFotoDocente { get; set; }
    }
}
