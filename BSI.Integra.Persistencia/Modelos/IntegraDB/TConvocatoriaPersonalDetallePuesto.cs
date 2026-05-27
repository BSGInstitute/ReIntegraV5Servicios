using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena las secciones de detalle del puesto que se visualizan en el portal web, asociadas a una convocatoria personal.
    /// </summary>
    public partial class TConvocatoriaPersonalDetallePuesto
    {
        /// <summary>
        /// Identificador unico del registro.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK hacia T_ConvocatoriaPersonal. Convocatoria a la que pertenece el detalle.
        /// </summary>
        public int IdConvocatoriaPersonal { get; set; }
        /// <summary>
        /// Titulo de la seccion del puesto que se muestra en el portal web.
        /// </summary>
        public string TituloSeccion { get; set; } = null!;
        /// <summary>
        /// Contenido descriptivo de la seccion. Maximo 1500 caracteres.
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Orden de visualizacion de la seccion dentro de la convocatoria.
        /// </summary>
        public int NroOrden { get; set; }
        /// <summary>
        /// 1 = activo, 0 = eliminado logicamente.
        /// </summary>
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;

        public virtual TConvocatoriaPersonal IdConvocatoriaPersonalNavigation { get; set; } = null!;
    }
}
