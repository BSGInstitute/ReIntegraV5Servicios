using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMontoPagoCronograma
    {
        public TMontoPagoCronograma()
        {
            TMontoPagoCronogramaDetalles = new HashSet<TMontoPagoCronogramaDetalle>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OportunidadNew
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_MontoPago
        /// </summary>
        public int? IdMontoPago { get; set; }
        /// <summary>
        /// Es Foreing Key tPersonal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Monto total del programa
        /// </summary>
        public double Precio { get; set; }
        /// <summary>
        /// Monto con descuento
        /// </summary>
        public double PrecioDescuento { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_TipoDescuento
        /// </summary>
        public int? IdTipoDescuento { get; set; }
        /// <summary>
        /// Si el cronograma ha sido aprobada
        /// </summary>
        public bool EsAprobado { get; set; }
        /// <summary>
        /// Nombre de la moneda en plural
        /// </summary>
        public string NombrePlural { get; set; } = null!;
        /// <summary>
        /// Es el tipo de formula designado
        /// </summary>
        public int Formula { get; set; }
        public int MatriculaEnProceso { get; set; }
        public string? CodigoMatricula { get; set; }
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

        public virtual ICollection<TMontoPagoCronogramaDetalle> TMontoPagoCronogramaDetalles { get; set; }
    }
}
