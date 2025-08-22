using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAdwordsApiVolumenBusquedum
    {
        /// <summary>
        /// Llave principal de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es clave foranea de T_AdwordsApiPalabraClave
        /// </summary>
        public int IdAdwordsApiPalabraClave { get; set; }
        /// <summary>
        /// Es el promedio de busqueda mensual de la palabra
        /// </summary>
        public int PromedioBusqueda { get; set; }
        /// <summary>
        /// Es el mes correspondiente a la búsqueda
        /// </summary>
        public int Mes { get; set; }
        /// <summary>
        /// Es el año correspondiente a la búsqueda
        /// </summary>
        public int Anho { get; set; }
        /// <summary>
        /// Es clave foranea de T_Pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
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
    }
}
