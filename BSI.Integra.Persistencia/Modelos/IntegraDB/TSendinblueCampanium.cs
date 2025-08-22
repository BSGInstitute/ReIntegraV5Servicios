using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSendinblueCampanium
    {
        /// <summary>
        /// Identificador unico de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la campaña
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Email de remitente
        /// </summary>
        public string? ContenidoHtml { get; set; }
        /// <summary>
        /// Id de la plantilla en sendinblue
        /// </summary>
        public int? IdPlantilla { get; set; }
        /// <summary>
        /// Indica el estado del remitente en sending blue
        /// </summary>
        public string? HoraProgramada { get; set; }
        /// <summary>
        /// Asunto de la campaña
        /// </summary>
        public string? Asunto { get; set; }
        /// <summary>
        /// Lista de Contactos
        /// </summary>
        public string? Receptor { get; set; }
        /// <summary>
        /// Hace referencia al campo ToField de Sendinblue
        /// </summary>
        public string? Campo { get; set; }
        /// <summary>
        /// Id de remitente de sendingblue
        /// </summary>
        public int? IdSendinblueRemitente { get; set; }
        /// <summary>
        /// Tipo de envio de campaña
        /// </summary>
        public bool PruebaAb { get; set; }
        /// <summary>
        /// Asunto del primer correo
        /// </summary>
        public string AsuntoA { get; set; } = null!;
        /// <summary>
        /// Asunto del segundo correo
        /// </summary>
        public string AsuntoB { get; set; } = null!;
        /// <summary>
        /// Porcentaje de envios
        /// </summary>
        public int ReglaDivision { get; set; }
        /// <summary>
        /// Critero de los correos ganadores
        /// </summary>
        public string GanadorCriterio { get; set; } = null!;
        /// <summary>
        /// Duracion del test por horas
        /// </summary>
        public int GanadorTiempoAtraso { get; set; }
        /// <summary>
        /// Respuesta de Sendinblue
        /// </summary>
        public string Respuesta { get; set; } = null!;
        /// <summary>
        /// Saber si se guardo o no en Sendinblue
        /// </summary>
        public bool EstadoGuardado { get; set; }
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
        public int? IdMigracion { get; set; }
    }
}
