using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFormularioLandingPage
    {
        public TFormularioLandingPage()
        {
            TFormularioPlantillas = new HashSet<TFormularioPlantilla>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_FORMULARIO_SOLICITUD
        /// </summary>
        public int IdFormularioSolicitud { get; set; }
        /// <summary>
        /// Nombre landing page
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Codigo landing page
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Valor en header
        /// </summary>
        public int Header { get; set; }
        /// <summary>
        /// Valor en footer
        /// </summary>
        public int Footer { get; set; }
        /// <summary>
        /// Es Foreing Key TMK_FORMULARIOLANDINGPAGE
        /// </summary>
        public int IdPlantillaLandingPage { get; set; }
        /// <summary>
        /// Mensaje landing page
        /// </summary>
        public string Mensaje { get; set; } = null!;
        /// <summary>
        /// Texto pop up
        /// </summary>
        public string? TextoPopup { get; set; }
        /// <summary>
        /// Titulo pop up
        /// </summary>
        public string? TituloPopup { get; set; }
        /// <summary>
        /// Color pop up
        /// </summary>
        public string ColorPopup { get; set; } = null!;
        /// <summary>
        /// Color titulo
        /// </summary>
        public string ColorTitulo { get; set; } = null!;
        /// <summary>
        /// Color T boton
        /// </summary>
        public string ColorTextoBoton { get; set; } = null!;
        /// <summary>
        /// Color F boton
        /// </summary>
        public string ColorFondoBoton { get; set; } = null!;
        /// <summary>
        /// Color descripcion
        /// </summary>
        public string ColorDescripcion { get; set; } = null!;
        /// <summary>
        /// Estado pop up
        /// </summary>
        public bool EstadoPopup { get; set; }
        /// <summary>
        /// Es Foreign Key tPEspecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Color formulario header
        /// </summary>
        public string ColorFondoHeader { get; set; } = null!;
        /// <summary>
        /// Tipo
        /// </summary>
        public string Tipo { get; set; } = null!;
        /// <summary>
        /// Pregunta 1
        /// </summary>
        public string Cita1Texto { get; set; } = null!;
        /// <summary>
        /// Color cita 1
        /// </summary>
        public string Cita1Color { get; set; } = null!;
        /// <summary>
        /// Pregunta 2
        /// </summary>
        public string Cita3Texto { get; set; } = null!;
        /// <summary>
        /// Color cita 3
        /// </summary>
        public string Cita3Color { get; set; } = null!;
        /// <summary>
        /// Pregunta 3
        /// </summary>
        public string Cita4Texto { get; set; } = null!;
        /// <summary>
        /// Color cita 4
        /// </summary>
        public string Cita4Color { get; set; } = null!;
        /// <summary>
        /// Signo de pregunta
        /// </summary>
        public string? Cita1Despues { get; set; }
        /// <summary>
        /// Estado muestra programa
        /// </summary>
        public bool MuestraPrograma { get; set; }
        /// <summary>
        /// Url de la imagen principal
        /// </summary>
        public string? UrlImagenPrincipal { get; set; }
        /// <summary>
        /// Color holder
        /// </summary>
        public string ColorPlaceHolder { get; set; } = null!;
        /// <summary>
        /// Id del remitente (foreign key de TCRM_GMAICLIENTE)
        /// </summary>
        public int? IdGmailClienteRemitente { get; set; }
        /// <summary>
        /// Id del receptor (foreign key de TCRM_GMAICLIENTE)
        /// </summary>
        public int? IdGmailClienteReceptor { get; set; }
        /// <summary>
        /// Id plantilla correo (foreign key de TCRM_PLANTILLA)
        /// </summary>
        public int? IdPlantilla { get; set; }
        public bool? TesteoAb { get; set; }
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
        /// Valida si tiene titulo del Programa Automático
        /// </summary>
        public bool? TituloProgramaAutomatico { get; set; }
        /// <summary>
        /// Valida si tiene descripción Web Automático
        /// </summary>
        public bool? DescripcionWebAutomatico { get; set; }

        public virtual ICollection<TFormularioPlantilla> TFormularioPlantillas { get; set; }
    }
}
