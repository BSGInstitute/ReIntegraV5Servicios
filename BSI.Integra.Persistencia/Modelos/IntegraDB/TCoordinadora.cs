using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCoordinadora
    {
        /// <summary>
        /// Llave principal de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Indica el alias del correo de la coordinadora
        /// </summary>
        public string AliasCorreo { get; set; } = null!;
        /// <summary>
        /// Indica la clave de la coordinadora
        /// </summary>
        public string Clave { get; set; } = null!;
        /// <summary>
        /// Indica la firma de la coordinadora
        /// </summary>
        public string Firma { get; set; } = null!;
        /// <summary>
        /// Indica el usuario de la coordinadora
        /// </summary>
        public string Usuario { get; set; } = null!;
        /// <summary>
        /// Indica el anexo de la coordinadora
        /// </summary>
        public int Anexo { get; set; }
        /// <summary>
        /// Indica la modalidad
        /// </summary>
        public string Modalidad { get; set; } = null!;
        /// <summary>
        /// Indica el genero de la coordinadora
        /// </summary>
        public bool Genero { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Sede
        /// </summary>
        public int IdSede { get; set; }
        /// <summary>
        /// Indica el numero
        /// </summary>
        public string? HtmlNumero { get; set; }
        /// <summary>
        /// Indica el horario
        /// </summary>
        public string? HtmlHorario { get; set; }
        /// <summary>
        /// Indica las iniciales de coordinadoras
        /// </summary>
        public string Iniciales { get; set; } = null!;
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
