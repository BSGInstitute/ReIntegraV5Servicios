using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAsignacionOportunidadLog
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_AsignacionOportunidad
        /// </summary>
        public int? IdAsignacionOportunidad { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OportunidadNew
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Es Foreing Key tPersonal
        /// </summary>
        public int? IdPersonalAnterior { get; set; }
        /// <summary>
        /// Es Foreing Key tPersonal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Es Foreing Key tCentroCosto
        /// </summary>
        public int? IdCentroCostoAnt { get; set; }
        /// <summary>
        /// Es Foreing Key tCentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Es Foreing Key tAlumnos
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Estado del cambio registrado
        /// </summary>
        public DateTime FechaLog { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_TipoDato
        /// </summary>
        public int? IdTipoDato { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_FaseOportunidad
        /// </summary>
        public int? IdFaseOportunidad { get; set; }
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
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_ClasificacionPersona
        /// </summary>
        public int? IdClasificacionPersona { get; set; }

        public virtual TAsignacionOportunidad? IdAsignacionOportunidadNavigation { get; set; }
    }
}
