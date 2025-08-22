using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMoodleCategorium
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK T_MoodleCategoriaTipo, Tipo de categoria: Diplomado o curso
        /// </summary>
        public int IdMoodleCategoriaTipo { get; set; }
        /// <summary>
        /// IdCategoria de la base de datos de moodle
        /// </summary>
        public int IdCategoriaMoodle { get; set; }
        /// <summary>
        /// Nombre de la categoria registrada
        /// </summary>
        public string NombreCategoria { get; set; } = null!;
        /// <summary>
        /// Valida si la categoria requiere proyecto
        /// </summary>
        public bool AplicaProyecto { get; set; }
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

        public virtual TMoodleCategoriaTipo IdMoodleCategoriaTipoNavigation { get; set; } = null!;
    }
}
