using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPgeneralAsubPgeneral
    {
        public TPgeneralAsubPgeneral()
        {
            TPgeneralAsubPgeneralVersionProgramas = new HashSet<TPgeneralAsubPgeneralVersionPrograma>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key T_PGeneral (Identificador del programa general padre)
        /// </summary>
        public int IdPgeneralPadre { get; set; }
        /// <summary>
        /// Es foreing key T_PGeneral (Identificador del programa general hijo)
        /// </summary>
        public int IdPgeneralHijo { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
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
        /// orden de asociacion  de padre e hijos
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Muestra si es visible en portal
        /// </summary>
        public bool? EsVisiblePortal { get; set; }
        /// <summary>
        /// FK de Id de Modulo
        /// </summary>
        public int? IdModulo { get; set; }
        /// <summary>
        /// FK de Id de Ciclo
        /// </summary>
        public int? IdCiclo { get; set; }

        public virtual ICollection<TPgeneralAsubPgeneralVersionPrograma> TPgeneralAsubPgeneralVersionProgramas { get; set; }
    }
}
