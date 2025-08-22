using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TModuloPespecifico
    {
        /// <summary>
        /// Primary key de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripcion del nombre del modulo
        /// </summary>
        public string? NombreModulo { get; set; }
        /// <summary>
        /// Id foreing key de PEspecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Id foreing key de tabla modulo
        /// </summary>
        public int? IdModulo { get; set; }
        /// <summary>
        /// Id foreing key de tabla ciclo
        /// </summary>
        public int? IdCiclo { get; set; }
        /// <summary>
        /// Id foreing key de PEspecifico
        /// </summary>
        public int? IdPespecificoHijo { get; set; }
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

        public virtual TCiclo? IdCicloNavigation { get; set; }
        public virtual TModulo? IdModuloNavigation { get; set; }
    }
}
