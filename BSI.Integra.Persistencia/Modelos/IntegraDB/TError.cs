using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TError
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de T_ErrorTipo
        /// </summary>
        public int IdErrorTipo { get; set; }
        /// <summary>
        /// Código del error
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Descripcion del error
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Descripcion perzonalizada del error
        /// </summary>
        public string? DescripcionPersonalizada { get; set; }
        /// <summary>
        /// Almacena el nombre del objeto en el cual se presenta el error
        /// </summary>
        public string? NombreObjeto { get; set; }
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

        public virtual TErrorTipo IdErrorTipoNavigation { get; set; } = null!;
    }
}
