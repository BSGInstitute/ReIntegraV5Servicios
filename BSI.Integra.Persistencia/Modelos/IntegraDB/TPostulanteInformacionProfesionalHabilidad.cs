using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla puente entre la informacion profesional del postulante y el catalogo de habilidades. Cada fila representa una habilidad declarada por el postulante con su nivel y anios de experiencia. Una misma habilidad no puede repetirse dentro del mismo registro de informacion profesional.
    /// </summary>
    public partial class TPostulanteInformacionProfesionalHabilidad
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es clave foranea de T_PostulanteInformacionProfesional
        /// </summary>
        public int IdPostulanteInformacionProfesional { get; set; }
        /// <summary>
        /// Es clave foranea de T_Habilidad (catalogo)
        /// </summary>
        public int IdHabilidad { get; set; }
        /// <summary>
        /// Es clave foranea de T_HabilidadNivel (nivel de dominio)
        /// </summary>
        public int? IdHabilidadNivel { get; set; }
        /// <summary>
        /// Anios de experiencia del postulante en la habilidad
        /// </summary>
        public int? AnioExperiencia { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool? Estado { get; set; }
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
        /// Id de la tabla original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual THabilidad IdHabilidadNavigation { get; set; } = null!;
        public virtual THabilidadNivel? IdHabilidadNivelNavigation { get; set; }
        public virtual TPostulanteInformacionProfesional IdPostulanteInformacionProfesionalNavigation { get; set; } = null!;
    }
}
