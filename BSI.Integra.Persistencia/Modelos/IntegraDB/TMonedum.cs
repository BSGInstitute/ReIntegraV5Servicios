using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMonedum
    {
        public TMonedum()
        {
            TAnuncioFacebookMetricas = new HashSet<TAnuncioFacebookMetrica>();
            TPaqueteTutorVirtualPais = new HashSet<TPaqueteTutorVirtualPai>();
            TTipoCambioMoneda = new HashSet<TTipoCambioMonedum>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre completo de la moneda
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Nombre comercial de la moneda
        /// </summary>
        public string NombreCorto { get; set; } = null!;
        /// <summary>
        /// Nombre comercial en plural de la moneda
        /// </summary>
        public string NombrePlural { get; set; } = null!;
        /// <summary>
        /// Simbolo de la moneda
        /// </summary>
        public string Simbolo { get; set; } = null!;
        /// <summary>
        /// Codigo ISO de la moneda
        /// </summary>
        public string? Codigo { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Pais
        /// </summary>
        public int IdPais { get; set; }
        public int DigitoFinanzas { get; set; }
        /// <summary>
        /// Creado o eliminado
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
        /// Sistema Automatico Fecha creacion
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
        /// Valida que se muestre la información de moneda en proceso de selección, área GP
        /// </summary>
        public bool? ValidaProcesoSeleccion { get; set; }
        /// <summary>
        /// Valida si la moneda se muestra en Agenda para leaderboarding
        /// </summary>
        public bool? VisualizarTableroComercial { get; set; }
        /// <summary>
        /// Valida que se muestre la información de moneda para fiannzas en los cronogramas
        /// </summary>
        public bool? VisualizarFinanzas { get; set; }
        public decimal? PorcentajeMora { get; set; }

        public virtual ICollection<TAnuncioFacebookMetrica> TAnuncioFacebookMetricas { get; set; }
        public virtual ICollection<TPaqueteTutorVirtualPai> TPaqueteTutorVirtualPais { get; set; }
        public virtual ICollection<TTipoCambioMonedum> TTipoCambioMoneda { get; set; }
    }
}
