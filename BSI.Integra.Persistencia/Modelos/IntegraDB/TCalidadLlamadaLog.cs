using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCalidadLlamadaLog
    {
        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///  Idetifica Valor estatico del Problema
        /// </summary>
        public int IdProblemaLlamada { get; set; }
        /// <summary>
        /// Identifica Valor estatico de Calidad
        /// </summary>
        public int IdCalidadLlamada { get; set; }
        /// <summary>
        /// Actividad Detalle de la tabla T_ActividadDetalle
        /// </summary>
        public int? IdActividadDetalle { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario Creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario Modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha Creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha Modificacion 
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id  de  la tabla TCRM_CalidadLLamadaLog
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
