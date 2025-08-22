using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TScrapingAerolineaResultado
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK Tabla pla.T_ScrapingAerolineaConfiguracion
        /// </summary>
        public int IdScrapingAerolineaConfiguracion { get; set; }
        /// <summary>
        /// Precio del vuelo encontrado por scraping
        /// </summary>
        public decimal Precio { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_ScrapingPagina, Indica de que pagina se hizo scraping
        /// </summary>
        public int IdScrapingPagina { get; set; }
        /// <summary>
        /// FK Tabla pla.T_CentroCosto
        /// </summary>
        public int IdCentroCosto { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_PEspecifico, con ello podremos llegar a la sesion para la que se hizo scraping
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Nos indica a que grupo de sesion se hizo scraping
        /// </summary>
        public int? NroSesionGrupo { get; set; }
        /// <summary>
        /// Nro de grupo al que pertenece el cronograma
        /// </summary>
        public int? NroGrupoCronograma { get; set; }
        /// <summary>
        /// Llave foranea con la tabla  T_Ciudad, Ciudad Origen
        /// </summary>
        public int IdCiudadOrigen { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Ciudad, Ciudad destino
        /// </summary>
        public int IdCiudadDestino { get; set; }
        /// <summary>
        /// Flag que indica que los resultados obtenidos en scraping son actuales
        /// </summary>
        public bool EsActual { get; set; }
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
        public int? IdMigracion { get; set; }
    }
}
