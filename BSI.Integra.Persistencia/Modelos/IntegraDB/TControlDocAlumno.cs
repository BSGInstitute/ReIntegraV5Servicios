using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TControlDocAlumno
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key tMatriculaCabecera
        /// </summary>
        public int? IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Es Foreing Key TCriteriosCalificacion
        /// </summary>
        public int? IdCriterioCalificacion { get; set; }
        /// <summary>
        /// Nomenclatura del area que entrego el documento
        /// </summary>
        public string QuienEntrego { get; set; } = null!;
        /// <summary>
        /// Fecha de entrega
        /// </summary>
        public DateTime? FechaEntregaDocumento { get; set; }
        /// <summary>
        /// Descripcion de la entrega
        /// </summary>
        public string? Observaciones { get; set; }
        /// <summary>
        /// Si hay comision
        /// </summary>
        public string? ComisionableEditable { get; set; }
        /// <summary>
        /// Monto de la comision
        /// </summary>
        public decimal? MontoComisionable { get; set; }
        /// <summary>
        /// Descripcion de alguna observacion
        /// </summary>
        public string? ObservacionesComisionable { get; set; }
        /// <summary>
        /// Monto total de la comision
        /// </summary>
        public decimal? PagadoComisionable { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
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
        /// Id de la tabla Original al migrar
        /// </summary>
        public string? IdMigracion { get; set; }
        public int? IdMatriculaObservacion { get; set; }
    }
}
