using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la configuracion de formularios progresivos
    /// </summary>
    public partial class TFormularioProgresivo
    {
        public TFormularioProgresivo()
        {
            TFormularioProgresivoConfiguracionBotons = new HashSet<TFormularioProgresivoConfiguracionBoton>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de formulario
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion de formulario
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Tipo de formulario
        /// </summary>
        public int Tipo { get; set; }
        /// <summary>
        /// Estado para activar y desactivar el formulario
        /// </summary>
        public bool? Activado { get; set; }
        /// <summary>
        /// Referencia al id de formulario progresivo inicial, solo para formulario tipo 2
        /// </summary>
        public int? IdFormularioProgresivoInicial { get; set; }
        /// <summary>
        /// Condición para mostrar formulario, solo para formulario tipo 1
        /// </summary>
        public int? CondicionMostrar { get; set; }
        /// <summary>
        /// Tiempo para mostrar formulario cuando está en programas, por publicidad, solo para formulario tipo 1
        /// </summary>
        public int? TiempoProgramasPublicidad { get; set; }
        /// <summary>
        /// Condicion para mostrar titulo
        /// </summary>
        public bool? Titulo { get; set; }
        /// <summary>
        /// Texto para titulo
        /// </summary>
        public string? TituloTexto { get; set; }
        /// <summary>
        /// Condicion para mostrar mensaje superior de cabecera
        /// </summary>
        public bool? CabeceraMensajeSup { get; set; }
        /// <summary>
        /// Texto para mensaje superior de cabecera
        /// </summary>
        public string? CabeceraMensajeSupTexto { get; set; }
        /// <summary>
        /// Condicion para mostrar mensaje de cabecera
        /// </summary>
        public bool? CabeceraMensaje { get; set; }
        /// <summary>
        /// Condicion para dividir mensaje de cabecera en index y curso
        /// </summary>
        public bool? CabeceraMensajeIndexCurso { get; set; }
        /// <summary>
        /// Texto para mensaje de cabecera caso index de ser el caso
        /// </summary>
        public string? CabeceraMensajeTexto { get; set; }
        /// <summary>
        /// Texto para mensaje de cabecera caso curso
        /// </summary>
        public string? CabeceraMensajeTextoCurso { get; set; }
        /// <summary>
        /// Condicion para mostrar bordes en mensaje de cabecera, solo para formulario tipo 2
        /// </summary>
        public bool? CabeceraMensajeBordes { get; set; }
        /// <summary>
        /// Condicion para mostrar mensaje inferior de cabecera
        /// </summary>
        public bool? CabeceraMensajeInf { get; set; }
        /// <summary>
        /// Condicion para dividir mensaje inferior de cabecera en index y curso
        /// </summary>
        public bool? CabeceraMensajeInfIndexCurso { get; set; }
        /// <summary>
        /// Texto para mensaje inferior de cabecera caso index de ser el caso
        /// </summary>
        public string? CabeceraMensajeInfTexto { get; set; }
        /// <summary>
        /// Texto para mensaje inferior de cabecera caso curso
        /// </summary>
        public string? CabeceraMensajeInfTextoCurso { get; set; }
        /// <summary>
        /// Condicion para mostrar boton de cabecera
        /// </summary>
        public bool? CabeceraBoton { get; set; }
        /// <summary>
        /// Texto para boton de cabecera
        /// </summary>
        public string? CabeceraBotonTexto { get; set; }
        /// <summary>
        /// Accion para boton de cabecera
        /// </summary>
        public int? CabeceraBotonAccion { get; set; }
        /// <summary>
        /// Condicion para mostrar mensaje superior de cuerpo
        /// </summary>
        public bool? CuerpoMensajeSup { get; set; }
        /// <summary>
        /// Texto para mensaje superior de cuerpo
        /// </summary>
        public string? CuerpoMensajeSupTexto { get; set; }
        /// <summary>
        /// Condicion para mostrar correo de cuerpo
        /// </summary>
        public bool? CuerpoCorreo { get; set; }
        /// <summary>
        /// Nro Orden para mostrar correo de cuerpo
        /// </summary>
        public int? CuerpoCorreoOrden { get; set; }
        /// <summary>
        /// Condicion para hacer obligatorio correo de cuerpo
        /// </summary>
        public bool? CuerpoCorreoObl { get; set; }
        /// <summary>
        /// Condicion para mostrar nombres de cuerpo
        /// </summary>
        public bool? CuerpoNombres { get; set; }
        /// <summary>
        /// Nro Orden para mostrar nombres de cuerpo
        /// </summary>
        public int? CuerpoNombresOrden { get; set; }
        /// <summary>
        /// Condicion para hacer obligatorio nombres de cuerpo
        /// </summary>
        public bool? CuerpoNombresObl { get; set; }
        /// <summary>
        /// Condicion para mostrar apellidos de cuerpo
        /// </summary>
        public bool? CuerpoApellidos { get; set; }
        /// <summary>
        /// Nro Orden para mostrar apellidos de cuerpo
        /// </summary>
        public int? CuerpoApellidosOrden { get; set; }
        /// <summary>
        /// Condicion para hacer obligatorio apellidos de cuerpo
        /// </summary>
        public bool? CuerpoApellidosObl { get; set; }
        /// <summary>
        /// Condicion para mostrar pais de cuerpo
        /// </summary>
        public bool? CuerpoPais { get; set; }
        /// <summary>
        /// Nro Orden para mostrar pais de cuerpo
        /// </summary>
        public int? CuerpoPaisOrden { get; set; }
        /// <summary>
        /// Condicion para hacer obligatorio pais de cuerpo
        /// </summary>
        public bool? CuerpoPaisObl { get; set; }
        /// <summary>
        /// Condicion para mostrar telefono de cuerpo
        /// </summary>
        public bool? CuerpoTelefono { get; set; }
        /// <summary>
        /// Nro Orden para mostrar telefono de cuerpo
        /// </summary>
        public int? CuerpoTelefonoOrden { get; set; }
        /// <summary>
        /// Condicion para hacer obligatorio telefono de cuerpo
        /// </summary>
        public bool? CuerpoTelefonoObl { get; set; }
        /// <summary>
        /// Condicion para mostrar cargo de cuerpo
        /// </summary>
        public bool? CuerpoCargo { get; set; }
        /// <summary>
        /// Nro Orden para mostrar cargo de cuerpo
        /// </summary>
        public int? CuerpoCargoOrden { get; set; }
        /// <summary>
        /// Condicion para hacer obligatorio cargo de cuerpo
        /// </summary>
        public bool? CuerpoCargoObl { get; set; }
        /// <summary>
        /// Condicion para mostrar area formacion de cuerpo
        /// </summary>
        public bool? CuerpoAreaFormacion { get; set; }
        /// <summary>
        /// Nro Orden para mostrar area formacion de cuerpo
        /// </summary>
        public int? CuerpoAreaFormacionOrden { get; set; }
        /// <summary>
        /// Condicion para hacer obligatorio area formacion de cuerpo
        /// </summary>
        public bool? CuerpoAreaFormacionObl { get; set; }
        /// <summary>
        /// Condicion para mostrar area trabajo de cuerpo
        /// </summary>
        public bool? CuerpoAreaTrabajo { get; set; }
        /// <summary>
        /// Nro Orden para mostrar area trabajo de cuerpo
        /// </summary>
        public int? CuerpoAreaTrabajoOrden { get; set; }
        /// <summary>
        /// Condicion para hacer obligatorio area trabajo de cuerpo
        /// </summary>
        public bool? CuerpoAreaTrabajoObl { get; set; }
        /// <summary>
        /// Condicion para mostrar industria de cuerpo
        /// </summary>
        public bool? CuerpoIndustria { get; set; }
        /// <summary>
        /// Nro Orden para mostrar industria de cuerpo
        /// </summary>
        public int? CuerpoIndustriaOrden { get; set; }
        /// <summary>
        /// Condicion para hacer obligatorio industria de cuerpo
        /// </summary>
        public bool? CuerpoIndustriaObl { get; set; }
        /// <summary>
        /// Condicion para mostrar mensaje inferior de cuerpo
        /// </summary>
        public bool? CuerpoMensajeInf { get; set; }
        /// <summary>
        /// Texto para mensaje inferior de cuerpo
        /// </summary>
        public string? CuerpoMensajeInfTexto { get; set; }
        /// <summary>
        /// Condicion para mostrar boton de cuerpo
        /// </summary>
        public bool? CuerpoBoton { get; set; }
        /// <summary>
        /// Texto para boton de cuerpo
        /// </summary>
        public string? CuerpoBotonTexto { get; set; }
        /// <summary>
        /// Accion para boton de cuerpo
        /// </summary>
        public int? CuerpoBotonAccion { get; set; }
        /// <summary>
        /// Condicion para mostrar pie
        /// </summary>
        public bool? Pie { get; set; }
        /// <summary>
        /// Texto para pie
        /// </summary>
        public string? PieTexto { get; set; }
        /// <summary>
        /// Condicion para mostrar boton
        /// </summary>
        public bool? Boton { get; set; }
        /// <summary>
        /// Texto para boton
        /// </summary>
        public string? BotonTexto { get; set; }
        /// <summary>
        /// Accion para boton
        /// </summary>
        public int? BotonAccion { get; set; }
        /// <summary>
        /// Estado del formulario
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion del formulario
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificacion del formulario
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion del formulario
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion del formulario
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Tiempo para mostrar formulario cuando está en programas, organico, solo para formulario tipo 1
        /// </summary>
        public int? TiempoProgramasOrganico { get; set; }
        /// <summary>
        /// Tiempo para mostrar formulario cuando está en blogs y whitepapers, solo para formulario tipo 1
        /// </summary>
        public int? TiempoBlogsWhite { get; set; }
        /// <summary>
        /// Tiempo para mostrar formulario cuando está en index y tags, solo para formulario tipo 1
        /// </summary>
        public int? TiempoIndexTags { get; set; }
        /// <summary>
        /// Texto para mensaje de cabecera caso whitepaper
        /// </summary>
        public string? CabeceraMensajeTextoWhitepaper { get; set; }
        /// <summary>
        /// Texto para mensaje inferior de cabecera caso whitepaper
        /// </summary>
        public string? CabeceraMensajeInfTextoWhitepaper { get; set; }
        /// <summary>
        /// Condicion para dividir mensaje superior de cabecera en index y curso
        /// </summary>
        public bool? CabeceraMensajeSupIndexCurso { get; set; }
        /// <summary>
        /// Texto para mensaje superior de cabecera caso curso
        /// </summary>
        public string? CabeceraMensajeSupTextoCurso { get; set; }
        /// <summary>
        /// Texto para mensaje superior de cabecera caso whitepaper
        /// </summary>
        public string? CabeceraMensajeSupTextoWhitepaper { get; set; }
        /// <summary>
        /// Condicion para dividir mensaje superior de cuerpo en index y curso
        /// </summary>
        public bool? CuerpoMensajeSupIndexCurso { get; set; }
        /// <summary>
        /// Texto para mensaje superior de cuerpo caso curso
        /// </summary>
        public string? CuerpoMensajeSupTextoCurso { get; set; }
        /// <summary>
        /// Texto para mensaje superior de cuerpo caso whitepaper
        /// </summary>
        public string? CuerpoMensajeSupTextoWhitepaper { get; set; }

        public virtual ICollection<TFormularioProgresivoConfiguracionBoton> TFormularioProgresivoConfiguracionBotons { get; set; }
    }
}
