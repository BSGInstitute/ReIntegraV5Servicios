using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPostulanteEquipoComputo
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk de la tabla T_Postulante
        /// </summary>
        public int IdPostulante { get; set; }
        /// <summary>
        /// Tipo de equpo de computo del postulante
        /// </summary>
        public string TipoEquipo { get; set; } = null!;
        /// <summary>
        /// memoria ram del equipo del postulante
        /// </summary>
        public string MemoriaRam { get; set; } = null!;
        /// <summary>
        /// Sistema operativo del equipo del postulante
        /// </summary>
        public string SistemaOperativo { get; set; } = null!;
        /// <summary>
        /// Procesador del equipo del postulante
        /// </summary>
        public string Procesador { get; set; } = null!;
        /// <summary>
        /// true: si el postulante tiene mouse, false: si no tiene
        /// </summary>
        public bool Mouse { get; set; }
        /// <summary>
        /// true: si el postulante tiene auricular, false: si no tiene
        /// </summary>
        public bool Auricular { get; set; }
        /// <summary>
        /// true: si el postulante tiene camara, false: si no tiene
        /// </summary>
        public bool Camara { get; set; }
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
        /// Valida si es el equipo que usara para el trabajo
        /// </summary>
        public bool? EsEquipoTrabajo { get; set; }
    }
}
