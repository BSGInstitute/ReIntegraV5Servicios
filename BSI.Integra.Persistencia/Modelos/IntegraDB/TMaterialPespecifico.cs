using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMaterialPespecifico
    {
        public TMaterialPespecifico()
        {
            TMaterialPespecificoDetalles = new HashSet<TMaterialPespecificoDetalle>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea a la tabla pla.T_PEspecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Almacena el grupo del programa especifico
        /// </summary>
        public int Grupo { get; set; }
        /// <summary>
        /// Llave foranea a la tabla ope.T_MaterialTipo
        /// </summary>
        public int IdMaterialTipo { get; set; }
        /// <summary>
        /// Indica a que grupo de edicion pertenece
        /// </summary>
        public int GrupoEdicion { get; set; }
        /// <summary>
        /// Indice el orden dentro de un grupo de edicion
        /// </summary>
        public int GrupoEdicionOrden { get; set; }
        /// <summary>
        /// Llave foranea a la tabla fin.T_Fur
        /// </summary>
        public int? IdFur { get; set; }
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
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual ICollection<TMaterialPespecificoDetalle> TMaterialPespecificoDetalles { get; set; }
    }
}
