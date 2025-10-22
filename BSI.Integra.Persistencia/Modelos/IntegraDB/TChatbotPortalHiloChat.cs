using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena informacion de un chat con el alumno.
    /// </summary>
    public partial class TChatbotPortalHiloChat
    {
        public TChatbotPortalHiloChat()
        {
            TFormularioAplicadoChatbots = new HashSet<TFormularioAplicadoChatbot>();
        }

        /// <summary>
        /// Identificador unico, es Primary Key.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del alumno asociado.
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Identificador del contacto portal segmento.
        /// </summary>
        public string? IdContactoPortalSegmento { get; set; }
        /// <summary>
        /// Identificador del área de derivación.
        /// </summary>
        public int? CodigoAreaDerivacion { get; set; }
        /// <summary>
        /// Nombre del navegador utilizado por el usuario.
        /// </summary>
        public string Navegador { get; set; } = null!;
        /// <summary>
        /// Direccion IPv4 del usuario.
        /// </summary>
        public string? DireccionIpv4 { get; set; }
        /// <summary>
        /// Dirección IPv6 del usuario.
        /// </summary>
        public string? DireccionIpv6 { get; set; }
        /// <summary>
        /// Indica si el chat esta cerrado (1 = cerrado, 0 = abierto).
        /// </summary>
        public bool Cerrado { get; set; }
        /// <summary>
        /// Indica si el chat ha sido derivado (1 = derivado, 0 = no derivado).
        /// </summary>
        public bool Derivado { get; set; }
        /// <summary>
        /// Indica si el chat ha sido derivado y está cerrado.
        /// </summary>
        public bool? DerivacionCerrado { get; set; }
        /// <summary>
        /// Indica el estado creado/eliminado del registro.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Nombre del usuario que creo el registro.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Nombre del usuario que modifico el registro por ultima vez.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en que se creo el registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la ultima modificacion del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Numero que indica la version del registro.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Identificador para migraciones.
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Valoración del chatbot del portal web
        /// </summary>
        public int? Valoracion { get; set; }

        public virtual TAlumno? IdAlumnoNavigation { get; set; }
        public virtual ICollection<TFormularioAplicadoChatbot> TFormularioAplicadoChatbots { get; set; }
    }
}
