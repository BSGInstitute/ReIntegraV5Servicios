using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTipoDescuento
    {
        public TTipoDescuento()
        {
            TMontoPagoLogIdTipoDescuentoModificadoNavigations = new HashSet<TMontoPagoLog>();
            TMontoPagoLogIdTipoDescuentoOriginalNavigations = new HashSet<TMontoPagoLog>();
            TTipoDescuentoSolicituds = new HashSet<TTipoDescuentoSolicitud>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Codigo del tipo de descuento que se da al programa
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Descripcion del tipo de descuento
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Formula
        /// </summary>
        public int Formula { get; set; }
        /// <summary>
        /// Porcentage general
        /// </summary>
        public int? PorcentajeGeneral { get; set; }
        /// <summary>
        /// Porcentaje a la matricula
        /// </summary>
        public int? PorcentajeMatricula { get; set; }
        /// <summary>
        /// Fraccionamiento a la matricula
        /// </summary>
        public int? FraccionesMatricula { get; set; }
        /// <summary>
        /// Porcentaje a las cuotas
        /// </summary>
        public int? PorcentajeCuotas { get; set; }
        /// <summary>
        /// Cuotas adicionales
        /// </summary>
        public int? CuotasAdicionales { get; set; }
        /// <summary>
        /// Creado o eliminado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha creacion
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

        public virtual ICollection<TMontoPagoLog> TMontoPagoLogIdTipoDescuentoModificadoNavigations { get; set; }
        public virtual ICollection<TMontoPagoLog> TMontoPagoLogIdTipoDescuentoOriginalNavigations { get; set; }
        public virtual ICollection<TTipoDescuentoSolicitud> TTipoDescuentoSolicituds { get; set; }
    }
}
