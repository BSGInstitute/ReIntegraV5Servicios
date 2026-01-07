using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPai
    {
        public TPai()
        {
            TMontoPagoLogIdPaisModificadoNavigations = new HashSet<TMontoPagoLog>();
            TMontoPagoLogIdPaisOriginalNavigations = new HashSet<TMontoPagoLog>();
            TPaqueteTutorVirtualPais = new HashSet<TPaqueteTutorVirtualPai>();
            TWhatsAppMensajeRecibidos = new HashSet<TWhatsAppMensajeRecibido>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Codigo Pais
        /// </summary>
        public int CodigoPais { get; set; }
        /// <summary>
        /// Codigo Abreviado
        /// </summary>
        public string CodigoIso { get; set; } = null!;
        /// <summary>
        /// Nombre del pais
        /// </summary>
        public string NombrePais { get; set; } = null!;
        /// <summary>
        /// Moneda del pais
        /// </summary>
        public string Moneda { get; set; } = null!;
        /// <summary>
        /// Zona horia del pais
        /// </summary>
        public decimal ZonaHoraria { get; set; }
        /// <summary>
        /// Permite visualizar en Integra y/o Portal
        /// </summary>
        public int EstadoPublicacion { get; set; }
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
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Codigo geográfico del Pais en Google
        /// </summary>
        public int? CodigoGoogleId { get; set; }
        /// <summary>
        /// Codigo pais de dos digitos que se usara para la creacion de usuarios en moodle
        /// </summary>
        public string? CodigoPaisMoodle { get; set; }
        /// <summary>
        /// Ruta del repositorio contenedor de la bandera
        /// </summary>
        public string? RutaBandera { get; set; }
        /// <summary>
        /// Ruta del repositorio contenedor del icono de la bandera
        /// </summary>
        public string? RutaIcono { get; set; }
        /// <summary>
        /// Permite vizualizar combo de pais dentro del modulo Personal
        /// </summary>
        public int? EstadoVisualizacion { get; set; }

        public virtual ICollection<TMontoPagoLog> TMontoPagoLogIdPaisModificadoNavigations { get; set; }
        public virtual ICollection<TMontoPagoLog> TMontoPagoLogIdPaisOriginalNavigations { get; set; }
        public virtual ICollection<TPaqueteTutorVirtualPai> TPaqueteTutorVirtualPais { get; set; }
        public virtual ICollection<TWhatsAppMensajeRecibido> TWhatsAppMensajeRecibidos { get; set; }
    }
}
