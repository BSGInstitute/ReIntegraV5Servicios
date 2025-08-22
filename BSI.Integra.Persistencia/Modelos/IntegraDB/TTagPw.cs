using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTagPw
    {
        public TTagPw()
        {
            TPgeneralTagsPws = new HashSet<TPgeneralTagsPw>();
            TTagParametroSeoPws = new HashSet<TTagParametroSeoPw>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del tag
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion breve del tag
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Identificador del sistema antiguo de la pagina web
        /// </summary>
        public int? TagWebId { get; set; }
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
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Guarda el nombre de los tags sin caracteres especiales
        /// </summary>
        public string? Codigo { get; set; }

        public virtual ICollection<TPgeneralTagsPw> TPgeneralTagsPws { get; set; }
        public virtual ICollection<TTagParametroSeoPw> TTagParametroSeoPws { get; set; }
    }
}
