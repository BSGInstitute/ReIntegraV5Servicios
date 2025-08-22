using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCentil
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_ExamenTest
        /// </summary>
        public int? IdExamenTest { get; set; }
        /// <summary>
        /// Fk T_GrupoComponenteEvaluacion
        /// </summary>
        public int? IdGrupoComponenteEvaluacion { get; set; }
        /// <summary>
        /// Fk T_Examen
        /// </summary>
        public int? IdExamen { get; set; }
        /// <summary>
        /// Fk T_Sexo
        /// </summary>
        public int? IdSexo { get; set; }
        /// <summary>
        /// Valor minimo del puntaje
        /// </summary>
        public decimal ValorMinimo { get; set; }
        /// <summary>
        /// Valor maximo del puntaje
        /// </summary>
        public decimal ValorMaximo { get; set; }
        /// <summary>
        /// Centil numerico 
        /// </summary>
        public decimal? Centil { get; set; }
        /// <summary>
        /// Centil que se expresa en letras
        /// </summary>
        public string? CentilLetra { get; set; }
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
        /// <summary>
        /// Centil numerico
        /// </summary>
        public decimal? CentilAdicional { get; set; }
        /// <summary>
        /// Indica la version del Centil
        /// </summary>
        public int? Version { get; set; }
        /// <summary>
        /// Indica si el centil es usado actualemente
        /// </summary>
        public bool? EsVigente { get; set; }
    }
}
