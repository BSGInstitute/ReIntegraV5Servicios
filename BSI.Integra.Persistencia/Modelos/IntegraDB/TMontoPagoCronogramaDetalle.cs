using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMontoPagoCronogramaDetalle
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Numero de la cuota generada del cronograma
        /// </summary>
        public int NumeroCuota { get; set; }
        /// <summary>
        /// Monto de la cuota para pagar
        /// </summary>
        public double MontoCuota { get; set; }
        /// <summary>
        /// Fecha de pago de la cuota
        /// </summary>
        public DateTime FechaPago { get; set; }
        /// <summary>
        /// Descriobe si es Cuota o matricula anteponiendo el Nro
        /// </summary>
        public string CuotaDescripcion { get; set; } = null!;
        /// <summary>
        /// Monto de la cuota con su respectivo descuento
        /// </summary>
        public double MontoCuotaDescuento { get; set; }
        /// <summary>
        /// Si se ha pagado la cuota (No se utiliza)
        /// </summary>
        public bool Pagado { get; set; }
        /// <summary>
        /// Es Foreing Key T_MontoPagoCronograma
        /// </summary>
        public int? IdMontoPagoCronograma { get; set; }
        /// <summary>
        /// De todo el cronograma se designa una matricula base
        /// </summary>
        public bool Matricula { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
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
        public Guid? IdMigracion { get; set; }

        public virtual TMontoPagoCronograma? IdMontoPagoCronogramaNavigation { get; set; }
    }
}
