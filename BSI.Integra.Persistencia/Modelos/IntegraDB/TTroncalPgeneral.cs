using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTroncalPgeneral
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del programa 
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Codigo del programa
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Es foreing key tPartner
        /// </summary>
        public int? IdTroncalPartner { get; set; }
        /// <summary>
        /// Duracion del programa
        /// </summary>
        public int Duracion { get; set; }
        /// <summary>
        /// Es foreing key tArea
        /// </summary>
        public int IdArea { get; set; }
        /// <summary>
        /// Es foreing key tSubArea
        /// </summary>
        public int IdSubArea { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public int? IdBusqueda { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
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
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
