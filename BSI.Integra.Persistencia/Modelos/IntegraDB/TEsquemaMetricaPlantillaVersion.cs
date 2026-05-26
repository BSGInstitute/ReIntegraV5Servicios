using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Versiones de esquemas de evaluacion por puesto. Un puesto puede tener multiples versiones con ciclo de vida: borrador -&gt; vigente -&gt; archivada. Solo puede existir un registro con EstadoVersion = vigente por IdPuesto (regla de negocio aplicada en SP_T_EsquemaVersion_Publicar).
    /// </summary>
    public partial class TEsquemaMetricaPlantillaVersion
    {
        public TEsquemaMetricaPlantillaVersion()
        {
            TEsquemaMetricas = new HashSet<TEsquemaMetrica>();
        }

        /// <summary>
        /// Identificador unico autoincrementado.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK a T_PuestoTrabajo. Puesto al que pertenece esta version de esquema.
        /// </summary>
        public int IdPuestoTrabajo { get; set; }
        /// <summary>
        /// Etiqueta de version legible para el UI (ej: v2.0, v1.1, borrador).
        /// </summary>
        public string NumeroVersion { get; set; } = null!;
        /// <summary>
        /// Estado del ciclo de vida. Valores validos: vigente | borrador | archivada. Solo puede existir un registro vigente por IdPuesto.
        /// </summary>
        public string EstadoVersion { get; set; } = null!;
        /// <summary>
        /// Descripcion del cambio que motiva esta version (ej: TypeScript como obligatorio y Certificaciones incorporada).
        /// </summary>
        public string? Motivo { get; set; }
        /// <summary>
        /// Fecha en que el borrador fue publicado como vigente. NULL mientras la version sea borrador.
        /// </summary>
        public DateTime? FechaPublicacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;

        public virtual TPuestoTrabajo IdPuestoTrabajoNavigation { get; set; } = null!;
        public virtual ICollection<TEsquemaMetrica> TEsquemaMetricas { get; set; }
    }
}
