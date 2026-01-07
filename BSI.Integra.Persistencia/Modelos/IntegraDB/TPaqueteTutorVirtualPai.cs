using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPaqueteTutorVirtualPai
    {
        public TPaqueteTutorVirtualPai()
        {
            TPaqueteTutorVirtualPaisBeneficios = new HashSet<TPaqueteTutorVirtualPaisBeneficio>();
        }

        /// <summary>
        /// Identificador autoincremental del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id del paquete de tutor virtual
        /// </summary>
        public int IdPaqueteTutorVirtual { get; set; }
        /// <summary>
        /// Id del país asociado
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Id de la moneda con la que se vende el paquete
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Costo individual por tutor virtual
        /// </summary>
        public decimal CostoIndividual { get; set; }
        /// <summary>
        /// Costo total del paquete para este país
        /// </summary>
        public decimal CostoPaquete { get; set; }
        /// <summary>
        /// Estado lógico del registro (1=Activo, 0=Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro por última vez
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia (timestamp)
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TMonedum IdMonedaNavigation { get; set; } = null!;
        public virtual TPai IdPaisNavigation { get; set; } = null!;
        public virtual TPaqueteTutorVirtual IdPaqueteTutorVirtualNavigation { get; set; } = null!;
        public virtual ICollection<TPaqueteTutorVirtualPaisBeneficio> TPaqueteTutorVirtualPaisBeneficios { get; set; }
    }
}
