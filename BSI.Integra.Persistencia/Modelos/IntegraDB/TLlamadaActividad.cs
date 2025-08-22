using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TLlamadaActividad
    {
        /// <summary>
        /// es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// es foreign key de TCRM_ActividadesDetalleNew
        /// </summary>
        public int? IdActividadDetalle { get; set; }
        /// <summary>
        /// Es foreign key de tpersonal
        /// </summary>
        public int? IdAsesor { get; set; }
        /// <summary>
        /// es foreign key de tCentral_Llamadas
        /// </summary>
        public int? IdLlamada { get; set; }
        public bool EstadoProgramado { get; set; }
        public string Tag { get; set; } = null!;
        /// <summary>
        /// fecha de inicio de llamada
        /// </summary>
        public DateTime? FechaInicioLlamada { get; set; }
        /// <summary>
        /// fecha fin de llamada
        /// </summary>
        public DateTime? FechaFinLlamada { get; set; }
        /// <summary>
        /// T_AgendaTab
        /// </summary>
        public int? IdAgendaTab { get; set; }
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

        public virtual TActividadDetalle? IdActividadDetalleNavigation { get; set; }
    }
}
