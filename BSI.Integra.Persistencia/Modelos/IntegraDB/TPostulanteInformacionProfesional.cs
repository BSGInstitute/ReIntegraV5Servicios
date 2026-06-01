using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Informacion profesional del postulante. Un postulante puede tener N registros distintos en paralelo (ej: uno orientado a cada rol o puesto al que aplica), cada uno con su propio titulo, contenido, habilidades y recursos asociados. Todos los registros activos son validos simultaneamente.
    /// </summary>
    public partial class TPostulanteInformacionProfesional
    {
        public TPostulanteInformacionProfesional()
        {
            TPostulanteInformacionProfesionalHabilidads = new HashSet<TPostulanteInformacionProfesionalHabilidad>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es clave foranea de T_Postulante
        /// </summary>
        public int IdPostulante { get; set; }
        /// <summary>
        /// Titulo del Resumen Profesional ingresado por el postulante (ej: Resumen Comercial Senior)
        /// </summary>
        public string Titulo { get; set; } = null!;
        /// <summary>
        /// Texto del Resumen Profesional ingresado por el postulante desde la web. Respeta saltos de linea y parrafos
        /// </summary>
        public string Contenido { get; set; } = null!;
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
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TPostulante IdPostulanteNavigation { get; set; } = null!;
        public virtual ICollection<TPostulanteInformacionProfesionalHabilidad> TPostulanteInformacionProfesionalHabilidads { get; set; }
    }
}
