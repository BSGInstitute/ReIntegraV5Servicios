using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TLlamadaWebphoneAsterisk
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fecha Inicial de la llamada
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha Final de la llamada
        /// </summary>
        public DateTime FechaFin { get; set; }
        /// <summary>
        /// Anexo de Personal
        /// </summary>
        public string Anexo { get; set; } = null!;
        /// <summary>
        /// Telefono a quien se realizo la llamada
        /// </summary>
        public string? TelefonoDestino { get; set; }
        /// <summary>
        /// clave foranea de la tabla com.T_ActividadDetalle
        /// </summary>
        public int IdActividadDetalle { get; set; }
        /// <summary>
        /// clave foranea de la tabla  com.T_LlamadaWebphoneTipo
        /// </summary>
        public int IdLlamadaWebphoneTipo { get; set; }
        /// <summary>
        /// Id cdr Asterisk
        /// </summary>
        public int CdrId { get; set; }
        /// <summary>
        /// Duracion de timbrado de la llamada
        /// </summary>
        public int DuracionTimbrado { get; set; }
        /// <summary>
        /// duracion contestado de la llamada
        /// </summary>
        public int DuracionContesto { get; set; }
        /// <summary>
        /// Nombre del archivo de audio de la llamada
        /// </summary>
        public string NombreGrabacion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
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
        /// <summary>
        /// Llave foranea con la tabla T_ProveedorNube
        /// </summary>
        public int? IdProveedorNube { get; set; }
        /// <summary>
        /// Url del archivo respaldado
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// Indica si el Archivo se elimino del disco duro original
        /// </summary>
        public bool? EsEliminado { get; set; }
        /// <summary>
        /// Indica el numero de bytes del archivo
        /// </summary>
        public int? NroBytes { get; set; }
        /// <summary>
        /// Indica la fecha de subida del archivo al respaldo
        /// </summary>
        public DateTime? FechaSubida { get; set; }
        /// <summary>
        /// Indica la fecha en la que se elimino el archivo del disco
        /// </summary>
        public DateTime? FechaEliminacion { get; set; }
        public bool? GrabacionContrato { get; set; }
        public int? IdServidorAsterisk { get; set; }
    }
}
