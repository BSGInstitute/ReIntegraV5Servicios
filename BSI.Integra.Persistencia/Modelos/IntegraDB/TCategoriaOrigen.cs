using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCategoriaOrigen
    {
        public TCategoriaOrigen()
        {
            TCampaniaGenerals = new HashSet<TCampaniaGeneral>();
            TGoogleAdsConversionQueues = new HashSet<TGoogleAdsConversionQueue>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la categoria de origen
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion del registro
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Es Foreing Key TCRM_TipoDato
        /// </summary>
        public int IdTipoDato { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_TipoCategoriaOrigen
        /// </summary>
        public int IdTipoCategoriaOrigen { get; set; }
        /// <summary>
        /// Meta de la categoria origen
        /// </summary>
        public int Meta { get; set; }
        public int? IdProveedorCampaniaIntegra { get; set; }
        public int? IdFormularioProcedencia { get; set; }
        public bool Considerar { get; set; }
        public string CodigoOrigen { get; set; } = null!;
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
        /// <summary>
        /// Este codigo identifica a la CategoriaOrigen, forma parte del nombre de un ConjuntoAnuncio, en la elaboracion del mismo (usado en EnlaceCampaniaController)
        /// </summary>
        public string? CodigoPublicidad { get; set; }

        public virtual ICollection<TCampaniaGeneral> TCampaniaGenerals { get; set; }
        public virtual ICollection<TGoogleAdsConversionQueue> TGoogleAdsConversionQueues { get; set; }
    }
}
