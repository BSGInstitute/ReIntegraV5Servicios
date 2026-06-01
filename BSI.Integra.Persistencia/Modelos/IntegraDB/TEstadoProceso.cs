using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catalogo de estados de procesamiento para flujos en general
    /// </summary>
    public partial class TEstadoProceso
    {
        public TEstadoProceso()
        {
            TLoteCurricula = new HashSet<TLoteCurriculum>();
            TLoteCurriculumItems = new HashSet<TLoteCurriculumItem>();
        }

        /// <summary>
        /// Llave primaria de la tabla.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del estado de procesamiento. Valores validos: pendiente, procesando, finalizado, error.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Indicador logico del registro. 1 = activo, 0 = inactivo.
        /// </summary>
        public bool? Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizo la ultima modificacion del registro.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creacion del registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la ultima modificacion del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia optimista gestionado automaticamente por SQL Server.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TLoteCurriculum> TLoteCurricula { get; set; }
        public virtual ICollection<TLoteCurriculumItem> TLoteCurriculumItems { get; set; }
    }
}
