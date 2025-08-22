using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TChatbotFlujoPreguntaPredefinidum
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave primaria T_ChatbotConfiguracionFlujo
        /// </summary>
        public int IdChatbotConfiguracionFlujo { get; set; }
        /// <summary>
        /// Registra si el usuario esta registrado con un booleano
        /// </summary>
        public bool UsuarioRegistrado { get; set; }
        /// <summary>
        /// Representa en que paso del flujo esta
        /// </summary>
        public int Paso { get; set; }
        /// <summary>
        /// Es la opcion seleccionada dentro del paso
        /// </summary>
        public string Caso { get; set; } = null!;
        /// <summary>
        /// Registra si el es el ultimo mensaje con un booleano
        /// </summary>
        public bool? EsMensajeFinal { get; set; }
        /// <summary>
        /// Es el mensaje a visualizar
        /// </summary>
        public string? Mensaje { get; set; }
        /// <summary>
        /// Trae SP o nombre de las funciones que trae la lista de opciones
        /// </summary>
        public string? FuncionObtenerOpcion { get; set; }
        public string? TipoOpcion { get; set; }
        public string? NombreFuncion { get; set; }
        /// <summary>
        /// Nombre de Error
        /// </summary>
        public string? MensajeErrorValidacion { get; set; }
        /// <summary>
        /// Almacena el Tipo de entrada respuesta ej numero, gmail
        /// </summary>
        public string? TipoDeEntradaRespuesta { get; set; }
        public string? MensajeFinalError { get; set; }
        /// <summary>
        /// Cuantas veces puede dar una respuesta equivocada
        /// </summary>
        public int? CantidadValidacion { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public int? IdCampoContacto { get; set; }

        public virtual TChatbotConfiguracionFlujo IdChatbotConfiguracionFlujoNavigation { get; set; } = null!;
    }
}
