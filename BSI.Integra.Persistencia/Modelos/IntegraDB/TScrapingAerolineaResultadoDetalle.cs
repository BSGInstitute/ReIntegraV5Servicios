using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TScrapingAerolineaResultadoDetalle
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_ScrapingAerolineaResultado
        /// </summary>
        public int IdScrapingAerolineaResultado { get; set; }
        /// <summary>
        /// Identificador del vuelo encontrado en scraping
        /// </summary>
        public string? NroVuelo { get; set; }
        /// <summary>
        /// Llave foranea con la tabla  T_Proveedor, con ello podremos llegar al proveedor para generar o actualizar los FUR
        /// </summary>
        public int? IdProveedor { get; set; }
        public string NombreAerolinea { get; set; } = null!;
        /// <summary>
        /// Llave foranea con la tabla  T_VueloTipoTramo, Indica si el vuelo es Directo o Escala
        /// </summary>
        public int? IdVueloTipoTramo { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Ciudad, Ciudad Origen
        /// </summary>
        public int? IdCiudadOrigen { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Ciudad, Ciudad destino
        /// </summary>
        public int? IdCiudadDestino { get; set; }
        /// <summary>
        /// Flag que indica si el vuelo es de Ida
        /// </summary>
        public bool EsIda { get; set; }
        /// <summary>
        /// Fecha de salida del vuelo
        /// </summary>
        public DateTime FechaSalida { get; set; }
        /// <summary>
        /// Fecha llegada del vuelo
        /// </summary>
        public DateTime FechaLlegada { get; set; }
        /// <summary>
        /// indica el viel de la clase para le vuelo
        /// </summary>
        public string? Clase { get; set; }
        /// <summary>
        /// Indica si permite Mochila
        /// </summary>
        public bool AplicaMochila { get; set; }
        /// <summary>
        /// Indica si permite Equipaje de mano
        /// </summary>
        public bool AplicaEquipajeMano { get; set; }
        /// <summary>
        /// Indica si permite equipaje en bodega
        /// </summary>
        public bool AplicaEquipajeBodega { get; set; }
        /// <summary>
        /// Duracion en minutos del vuelo
        /// </summary>
        public int DuracionMinuto { get; set; }
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
        /// <summary>
        /// Id del registro que es padre para formar las escalas del vuelo
        /// </summary>
        public int? IdPadre { get; set; }
        /// <summary>
        /// Nombre de la ciudad de origen
        /// </summary>
        public string? NombreCiudadOrigen { get; set; }
        /// <summary>
        /// Nombre de la ciudad destino
        /// </summary>
        public string? NombreCiudadDestino { get; set; }
    }
}
