using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPreguntaFrecuente
    {
        public TPreguntaFrecuente()
        {
            TPreguntaFrecuenteAreas = new HashSet<TPreguntaFrecuenteArea>();
            TPreguntaFrecuentePgenerals = new HashSet<TPreguntaFrecuentePgeneral>();
            TPreguntaFrecuenteSubAreas = new HashSet<TPreguntaFrecuenteSubArea>();
            TPreguntaFrecuenteTipos = new HashSet<TPreguntaFrecuenteTipo>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de T_Secccion_Pregunta
        /// </summary>
        public int IdSeccionPreguntaFrecuente { get; set; }
        /// <summary>
        /// Campo Pregunta
        /// </summary>
        public string Pregunta { get; set; } = null!;
        /// <summary>
        /// Campo Respuesta
        /// </summary>
        public string Respuesta { get; set; } = null!;
        public int Tipo { get; set; }
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
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TPreguntaFrecuenteArea> TPreguntaFrecuenteAreas { get; set; }
        public virtual ICollection<TPreguntaFrecuentePgeneral> TPreguntaFrecuentePgenerals { get; set; }
        public virtual ICollection<TPreguntaFrecuenteSubArea> TPreguntaFrecuenteSubAreas { get; set; }
        public virtual ICollection<TPreguntaFrecuenteTipo> TPreguntaFrecuenteTipos { get; set; }
    }
}
