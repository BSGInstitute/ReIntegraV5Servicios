using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Representa un currículo individual dentro de un lote. Gestiona únicamente el ciclo de vida del procesamiento. Los datos evaluados y extraídos se almacenan en tablas relacionadas 1:1.
    /// </summary>
    public partial class TLoteCurriculumItem
    {
        /// <summary>
        /// Identificador único del ítem.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK a T_LoteCurriculum. Lote al que pertenece este ítem.
        /// </summary>
        public int IdLoteCurriculum { get; set; }
        /// <summary>
        /// Nombre original del archivo subido por el usuario (ej. juan_perez_cv.pdf).
        /// </summary>
        public string NombreArchivo { get; set; } = null!;
        /// <summary>
        /// Es la url que almacena el archivo del item de un lote
        /// </summary>
        public string? UrlArchivo { get; set; }
        /// <summary>
        /// FK a conf.T_EstadoProceso. Estado actual del item.
        /// </summary>
        public int IdEstadoProceso { get; set; }
        /// <summary>
        /// Detalle del error ocurrido durante el procesamiento. Nulo cuando IdLoteCurriculumEstadoItem es distinto de Error.
        /// </summary>
        public string? MensajeError { get; set; }
        /// <summary>
        /// FK a T_Postulante. Nulo mientras el ítem está en revisión. Se popula al confirmar el currículo, indicando que el merge a las tablas principales fue completado.
        /// </summary>
        public int? IdPostulante { get; set; }
        public decimal? ConfiabilidadIa { get; set; }
        /// <summary>
        /// 1 = activo, 0 = eliminado lógico.
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

        public virtual TEstadoProceso IdEstadoProcesoNavigation { get; set; } = null!;
        public virtual TLoteCurriculum IdLoteCurriculumNavigation { get; set; } = null!;
        public virtual TPostulante? IdPostulanteNavigation { get; set; }
    }
}
