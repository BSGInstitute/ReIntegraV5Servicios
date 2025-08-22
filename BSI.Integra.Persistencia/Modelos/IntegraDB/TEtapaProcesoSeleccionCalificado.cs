using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TEtapaProcesoSeleccionCalificado
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Pk de la tabla gp.T_ProcesoSeleccionEtapa
        /// </summary>
        public int IdProcesoSeleccionEtapa { get; set; }
        /// <summary>
        /// Pk de la tabla gp.T_Postulante
        /// </summary>
        public int IdPostulante { get; set; }
        /// <summary>
        /// Verifica si la etapa ha sido aprobada o desaprobada
        /// </summary>
        public bool EsEtapaAprobada { get; set; }
        /// <summary>
        /// Representa la nota calculada de la Etapa del Proceso de Seleccion
        /// </summary>
        public decimal? NotaCalculada { get; set; }
        /// <summary>
        /// Primary ked de gp.T_EstadoEtapaProcesoSeleccion
        /// </summary>
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
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
        /// Define si es la Etapa actual del Postulante
        /// </summary>
        public bool? EsEtapaActual { get; set; }
        /// <summary>
        /// Define si el postulante a sido contactado en esta Etapa
        /// </summary>
        public bool? EsContactado { get; set; }

        public virtual TEstadoEtapaProcesoSeleccion? IdEstadoEtapaProcesoSeleccionNavigation { get; set; }
        public virtual TPostulante IdPostulanteNavigation { get; set; } = null!;
    }
}
