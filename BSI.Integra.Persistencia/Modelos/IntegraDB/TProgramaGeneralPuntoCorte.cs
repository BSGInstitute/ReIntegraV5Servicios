using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProgramaGeneralPuntoCorte
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK  a la tabla pla.T_pgeneral 
        /// </summary>
        public int? IdProgramaGeneral { get; set; }
        /// <summary>
        /// Valor de punto de corte para que un dato sea media
        /// </summary>
        public decimal? PuntoCorteMedia { get; set; }
        /// <summary>
        /// Valor de punto de corte para que un dato sea alta
        /// </summary>
        public decimal? PuntoCorteAlta { get; set; }
        /// <summary>
        /// Valor de punto de corte para que un dato sea muy alta
        /// </summary>
        public decimal? PuntoCorteMuyAlta { get; set; }
        /// <summary>
        /// indica el estado si esta activo o no
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
        public int? IdPais { get; set; }

        public virtual TPgeneral? IdProgramaGeneralNavigation { get; set; }
    }
}
