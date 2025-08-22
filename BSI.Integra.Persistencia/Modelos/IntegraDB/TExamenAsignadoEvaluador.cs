using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TExamenAsignadoEvaluador
    {
        public TExamenAsignadoEvaluador()
        {
            TExamenRealizadoRespuestaEvaluadors = new HashSet<TExamenRealizadoRespuestaEvaluador>();
        }

        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Fk T_Postulante
        /// </summary>
        public int IdPostulante { get; set; }
        /// <summary>
        /// Fk T_Examen
        /// </summary>
        public int IdExamen { get; set; }
        /// <summary>
        /// Fk T_ProcesoSeleccion
        /// </summary>
        public int IdProcesoSeleccion { get; set; }
        /// <summary>
        /// Estado del examen 1: realizado , 0: no realizado
        /// </summary>
        public bool EstadoExamen { get; set; }
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

        public virtual TExaman IdExamenNavigation { get; set; } = null!;
        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
        public virtual TPostulante IdPostulanteNavigation { get; set; } = null!;
        public virtual TProcesoSeleccion IdProcesoSeleccionNavigation { get; set; } = null!;
        public virtual ICollection<TExamenRealizadoRespuestaEvaluador> TExamenRealizadoRespuestaEvaluadors { get; set; }
    }
}
