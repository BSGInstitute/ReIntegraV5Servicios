using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TParametroSeoPw
    {
        public TParametroSeoPw()
        {
            TAreaParametroSeoPws = new HashSet<TAreaParametroSeoPw>();
            TSubAreaParametroSeoPws = new HashSet<TSubAreaParametroSeoPw>();
            TTagParametroSeoPws = new HashSet<TTagParametroSeoPw>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del tipo de SEO para HTML
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Restringe el numero de caracteres para su uso en otras plantillas
        /// </summary>
        public int NumeroCaracteres { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Usuario de creacion del registro
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
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TAreaParametroSeoPw> TAreaParametroSeoPws { get; set; }
        public virtual ICollection<TSubAreaParametroSeoPw> TSubAreaParametroSeoPws { get; set; }
        public virtual ICollection<TTagParametroSeoPw> TTagParametroSeoPws { get; set; }
    }
}
