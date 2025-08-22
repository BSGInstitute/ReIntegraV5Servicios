using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSolucionClienteByActividad
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OportunidadNew
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_ActividadesDetalleNew
        /// </summary>
        public int? IdActividadDetalle { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Causa
        /// </summary>
        public int IdCausa { get; set; }
        /// <summary>
        /// Es Foreing Key tPersonal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Flag de solucionado
        /// </summary>
        public bool Solucionado { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_ProblemaCliente
        /// </summary>
        public int IdProblemaCliente { get; set; }
        /// <summary>
        /// Descripcion de otro problema
        /// </summary>
        public string? OtroProblema { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// fecha de modificacion del registro
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

        public virtual TOportunidad? IdOportunidadNavigation { get; set; }
    }
}
