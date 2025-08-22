using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TContadorBicLog
    {
        public TContadorBicLog()
        {
            TContadorBicLogDetalles = new HashSet<TContadorBicLogDetalle>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key T_Oportunidad
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// Estado Sin Contacto Manhana
        /// </summary>
        public int SinContactoManhana { get; set; }
        /// <summary>
        /// Estado Sin Contacto Tarde
        /// </summary>
        public int SinContactoTarde { get; set; }
        /// <summary>
        /// Id Fase Oportunidad al momento del calculo
        /// </summary>
        public int IdFaseOportunidad { get; set; }
        public DateTime FechaCalculo { get; set; }
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
        /// Usuario de creacion del registro
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
        public int? IdMigracion { get; set; }

        public virtual TOportunidad IdOportunidadNavigation { get; set; } = null!;
        public virtual ICollection<TContadorBicLogDetalle> TContadorBicLogDetalles { get; set; }
    }
}
