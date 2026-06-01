using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de modos de procesamiento disponibles para un lote de currículums.
    /// </summary>
    public partial class TLoteCurriculumModoProcesamiento
    {
        public TLoteCurriculumModoProcesamiento()
        {
            TLoteCurricula = new HashSet<TLoteCurriculum>();
        }

        /// <summary>
        /// Identificador único del modo de procesamiento.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre descriptivo del modo: Automático | Asistido | Híbrido.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// 1 = activo, 0 = inactivo.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizó la última modificación.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia optimista generado automáticamente por SQL Server.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TLoteCurriculum> TLoteCurricula { get; set; }
    }
}
