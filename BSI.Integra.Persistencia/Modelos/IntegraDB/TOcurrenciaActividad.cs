using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOcurrenciaActividad
    {
        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Ocurrencia
        /// </summary>
        public int IdOcurrencia { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_ActividadesCabecera
        /// </summary>
        public int IdActividadCabecera { get; set; }
        /// <summary>
        /// Sin uso
        /// </summary>
        public bool? PreProgramada { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OcurrenciaActividad
        /// </summary>
        public int? IdOcurrenciaActividadPadre { get; set; }
        /// <summary>
        /// Si es nodo padre
        /// </summary>
        public bool NodoPadre { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
    }
}
