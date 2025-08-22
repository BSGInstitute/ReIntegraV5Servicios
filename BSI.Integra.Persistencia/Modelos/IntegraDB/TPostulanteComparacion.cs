using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPostulanteComparacion
    {
        /// <summary>
        /// Clave de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Primary key  de gp.T_Postulante
        /// </summary>
        public int? IdPostulante { get; set; }
        /// <summary>
        /// Primary key de gp.T_GrupoComparacionProcesoSeleccion
        /// </summary>
        public int? IdGrupoComparacionProcesoSeleccion { get; set; }
        /// <summary>
        /// Estado ,valida si esta activo o no
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario que modifico el registro
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
        /// Primary key de tabla en V3
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TGrupoComparacionProcesoSeleccion? IdGrupoComparacionProcesoSeleccionNavigation { get; set; }
        public virtual TPostulante? IdPostulanteNavigation { get; set; }
    }
}
