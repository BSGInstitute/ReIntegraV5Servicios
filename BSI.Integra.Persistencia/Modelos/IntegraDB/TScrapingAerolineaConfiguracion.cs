using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TScrapingAerolineaConfiguracion
    {
        /// <summary>
        /// Identificador primaria de la tabla T_ScrapingAerolineaConfiguracion
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificar foraneo de la tabla T_PEspecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Numero grupo de sesion al que pertenece las sesiones del cronograma
        /// </summary>
        public int? NroGrupoSesion { get; set; }
        public int? NroGrupoCronograma { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_ScrapingAerolineaEstadoConsulta
        /// </summary>
        public int IdScrapingAerolineaEstadoConsulta { get; set; }
        /// <summary>
        /// Llave foranea con la tabla tabla T_Ciudad
        /// </summary>
        public int IdCiudadOrigen { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Ciudad
        /// </summary>
        public int IdCiudadDestino { get; set; }
        /// <summary>
        /// Fecha ida a buscar en el scraping: Es calculada por el sistema o escogida por el usuario
        /// </summary>
        public DateTime FechaIda { get; set; }
        /// <summary>
        /// Fecha retorno a buscar en el scraping: Es calculada por el sistema o escogida por el usuario
        /// </summary>
        public DateTime FechaRetorno { get; set; }
        /// <summary>
        /// Precion, valor que se le restara a la fecha de ida para la busqueda de vuelos en scraping
        /// </summary>
        public decimal? PrecisionIda { get; set; }
        public int? NroFrecuencia { get; set; }
        public string? TipoFrecuencia { get; set; }
        public string? TipoVuelo { get; set; }
        public DateTime FechaEjecucion { get; set; }
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
        public int? IdFur { get; set; }
        /// <summary>
        /// Precion, valor que se le sumara a la fecha de regreso para la busqueda de vuelos en scraping
        /// </summary>
        public decimal? PrecisionRetorno { get; set; }
        /// <summary>
        /// True: si tiene un pasaje comprado, False: no tiene pasaje comprado
        /// </summary>
        public bool? TienePasajeComprado { get; set; }
    }
}
