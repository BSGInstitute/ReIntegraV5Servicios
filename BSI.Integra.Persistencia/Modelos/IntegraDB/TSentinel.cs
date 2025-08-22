using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSentinel
    {
        public TSentinel()
        {
            TSentinelRepLegItems = new HashSet<TSentinelRepLegItem>();
            TSentinelSdtEstandarItems = new HashSet<TSentinelSdtEstandarItem>();
            TSentinelSdtInfGens = new HashSet<TSentinelSdtInfGen>();
            TSentinelSdtLincreItems = new HashSet<TSentinelSdtLincreItem>();
            TSentinelSdtPoshisItems = new HashSet<TSentinelSdtPoshisItem>();
            TSentinelSdtRepSbsitems = new HashSet<TSentinelSdtRepSbsitem>();
            TSentinelSdtResVenItems = new HashSet<TSentinelSdtResVenItem>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Numero de DNI
        /// </summary>
        public string Dni { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
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
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TSentinelRepLegItem> TSentinelRepLegItems { get; set; }
        public virtual ICollection<TSentinelSdtEstandarItem> TSentinelSdtEstandarItems { get; set; }
        public virtual ICollection<TSentinelSdtInfGen> TSentinelSdtInfGens { get; set; }
        public virtual ICollection<TSentinelSdtLincreItem> TSentinelSdtLincreItems { get; set; }
        public virtual ICollection<TSentinelSdtPoshisItem> TSentinelSdtPoshisItems { get; set; }
        public virtual ICollection<TSentinelSdtRepSbsitem> TSentinelSdtRepSbsitems { get; set; }
        public virtual ICollection<TSentinelSdtResVenItem> TSentinelSdtResVenItems { get; set; }
    }
}
