using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfiguracionAsignacionEvaluacion
    {
        /// <summary>
        /// PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de gp.T_ProcesoSeleccion
        /// </summary>
        public int IdProcesoSeleccion { get; set; }
        /// <summary>
        /// FK de la tabla gp.T_ExamenTest
        /// </summary>
        public int IdEvaluacion { get; set; }
        /// <summary>
        /// Numero de Orden de la Evaluacion en el Proceso de Seleccion
        /// </summary>
        public int NroOrden { get; set; }
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
        /// Es Primary Key de gp.T_ProcesoSeleccionEtapa
        /// </summary>
        public int? IdProcesoSeleccionEtapa { get; set; }
    }
}
