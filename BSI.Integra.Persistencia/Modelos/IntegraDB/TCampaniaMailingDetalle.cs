using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCampaniaMailingDetalle
    {
        public TCampaniaMailingDetalle()
        {
            TAreaCampaniaMailingDetalles = new HashSet<TAreaCampaniaMailingDetalle>();
            TCampaniaMailingDetalleProgramas = new HashSet<TCampaniaMailingDetallePrograma>();
            TSubAreaCampaniaMailingDetalles = new HashSet<TSubAreaCampaniaMailingDetalle>();
        }

        public int Id { get; set; }
        /// <summary>
        /// tpla_campaniasMailing
        /// </summary>
        public int IdCampaniaMailing { get; set; }
        public int Prioridad { get; set; }
        public int Tipo { get; set; }
        /// <summary>
        /// tpla_RemitenteMailing
        /// </summary>
        public int IdRemitenteMailing { get; set; }
        /// <summary>
        /// TPersonal
        /// </summary>
        public int IdPersonal { get; set; }
        public string Subject { get; set; } = null!;
        public DateTime FechaEnvio { get; set; }
        /// <summary>
        /// TCRM_Hora
        /// </summary>
        public int IdHoraEnvio { get; set; }
        public int Proveedor { get; set; }
        /// <summary>
        /// Estado seteado Manualmente
        /// </summary>
        public int EstadoEnvio { get; set; }
        /// <summary>
        /// TCRM_FiltroSegmento
        /// </summary>
        public int? IdFiltroSegmento { get; set; }
        /// <summary>
        /// TCRM_PLANTILLA
        /// </summary>
        public int IdPlantilla { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public string? Campania { get; set; }
        public string? CodMailing { get; set; }
        public int? CantidadContactos { get; set; }
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
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Llave foranea a la tabla pla.T_CentroCosto
        /// </summary>
        public int? IdConjuntoListaDetalle { get; set; }
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Indica si el usuario subira manualmente la lista via el portal de el proveedor
        /// </summary>
        public bool? EsSubidaManual { get; set; }

        public virtual TCampaniaMailing IdCampaniaMailingNavigation { get; set; } = null!;
        public virtual ICollection<TAreaCampaniaMailingDetalle> TAreaCampaniaMailingDetalles { get; set; }
        public virtual ICollection<TCampaniaMailingDetallePrograma> TCampaniaMailingDetalleProgramas { get; set; }
        public virtual ICollection<TSubAreaCampaniaMailingDetalle> TSubAreaCampaniaMailingDetalles { get; set; }
    }
}
