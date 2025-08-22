using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TExamenConfiguracionFormato
    {
        /// <summary>
        /// Clave primario del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Si esta activo (en uso)
        /// </summary>
        public bool Activo { get; set; }
        /// <summary>
        /// Configuracion del titulo
        /// </summary>
        public string? ColorTextoTitulo { get; set; }
        /// <summary>
        /// Configuracion del titulo
        /// </summary>
        public int? TamanioTextoTitulo { get; set; }
        /// <summary>
        /// Configuracion del titulo
        /// </summary>
        public string? ColorFondoTitulo { get; set; }
        /// <summary>
        /// Configuracion del titulo
        /// </summary>
        public string? TipoLetraTitulo { get; set; }
        /// <summary>
        /// Configuracion del Enunciado
        /// </summary>
        public string? ColorTextoEnunciado { get; set; }
        /// <summary>
        /// Configuracion del Enunciado
        /// </summary>
        public int? TamanioTextoEnunciado { get; set; }
        /// <summary>
        /// Configuracion del Enunciado
        /// </summary>
        public string? ColorFondoEnunciado { get; set; }
        /// <summary>
        /// Configuracion del Enunciado
        /// </summary>
        public string? TipoLetraEnunciado { get; set; }
        /// <summary>
        /// Configuracion del Respuesta
        /// </summary>
        public string? ColorTextoRespuesta { get; set; }
        /// <summary>
        /// Configuracion del Respuesta
        /// </summary>
        public int? TamanioTextoRespuesta { get; set; }
        /// <summary>
        /// Configuracion del Respuesta
        /// </summary>
        public string? ColorFondoRespuesta { get; set; }
        /// <summary>
        /// Configuracion del Respuesta
        /// </summary>
        public string? TipoLetraRespuesta { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de la tabla
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
