using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAnuncioFacebookMetrica
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_AnuncioFacebook
        /// </summary>
        public int? IdAnuncioFacebook { get; set; }
        /// <summary>
        /// Gasto del anuncio expresado en USD
        /// </summary>
        public decimal? Gasto { get; set; }
        /// <summary>
        /// Llave foranea de la tabla pla.T_Moneda
        /// </summary>
        public int? IdMoneda { get; set; }
        /// <summary>
        /// Cantidad de impresiones del anuncio
        /// </summary>
        public int? Impresiones { get; set; }
        /// <summary>
        /// Cantidad de clics unicos
        /// </summary>
        public int? CantidadClicsUnicos { get; set; }
        /// <summary>
        /// Cantidad de clics
        /// </summary>
        public int? CantidadClics { get; set; }
        /// <summary>
        /// Alcance del anuncio
        /// </summary>
        public int? Alcance { get; set; }
        /// <summary>
        /// Fecha inicio de descarga de metrica
        /// </summary>
        public DateTime? FechaConsulta { get; set; }
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
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de V3
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Cantidad de clics en el enlace
        /// </summary>
        public int? CantidadClicsEnlace { get; set; }

        public virtual TAnuncioFacebook? IdAnuncioFacebookNavigation { get; set; }
        public virtual TMonedum? IdMonedaNavigation { get; set; }
    }
}
