using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Representa un lote de carga masiva de currículums asociado a una convocatoria. Agrupa N ítems y controla el estado global del procesamiento.
    /// </summary>
    public partial class TLoteCurriculum
    {
        public TLoteCurriculum()
        {
            TLoteCurriculumItems = new HashSet<TLoteCurriculumItem>();
        }

        /// <summary>
        /// Identificador único del lote.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre descriptivo del lote
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// FK a T_ConvocatoriaPersonal. Convocatoria para la cual se evalúan los currículums del lote.
        /// </summary>
        public int IdConvocatoriaPersonal { get; set; }
        /// <summary>
        /// FK a T_LoteCurriculumModoProcesamiento. Determina si el lote es automático, asistido o híbrido.
        /// </summary>
        public int IdLoteCurriculumModoProcesamiento { get; set; }
        /// <summary>
        /// Valor 0-100. Ítems cuyo ConfianzaGlobal sea mayor o igual a este umbral se procesan automáticamente sin revisión manual.
        /// </summary>
        public int UmbralConfianza { get; set; }
        /// <summary>
        /// Origen declarado de los CVs del lote: portal | referido | bolsa | mixto. Nullable.
        /// </summary>
        public string? OrigenDeclarado { get; set; }
        /// <summary>
        /// Instrucciones adicionales enviadas al modelo de IA para personalizar la evaluación de este lote. Nullable.
        /// </summary>
        public string? InstruccionesPrompt { get; set; }
        /// <summary>
        /// FK a conf.T_EstadoProceso. Estado actual del lote.
        /// </summary>
        public int IdEstadoProceso { get; set; }
        /// <summary>
        /// Cantidad total de currículums incluidos en el lote.
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// Cantidad de currículums procesados y mergeados exitosamente a T_Postulante.
        /// </summary>
        public int ItemsCompletados { get; set; }
        /// <summary>
        /// Cantidad de currículums que fallaron durante el procesamiento de la IA.
        /// </summary>
        public int ItemsError { get; set; }
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

        public virtual TConvocatoriaPersonal IdConvocatoriaPersonalNavigation { get; set; } = null!;
        public virtual TEstadoProceso IdEstadoProcesoNavigation { get; set; } = null!;
        public virtual TLoteCurriculumModoProcesamiento IdLoteCurriculumModoProcesamientoNavigation { get; set; } = null!;
        public virtual ICollection<TLoteCurriculumItem> TLoteCurriculumItems { get; set; }
    }
}
