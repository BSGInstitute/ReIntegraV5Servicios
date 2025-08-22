using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMandrilEnvioCorreo
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK a T_Oportunidad
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// FK a T_CentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Fk a T_Personal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// FK a  T_Alumno
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Fk a T_MandrilTipoAsignacion
        /// </summary>
        public int IdMandrilTipoAsignacion { get; set; }
        public int? EstadoEnvio { get; set; }
        /// <summary>
        /// FK a T_MandrilTipoEnvio
        /// </summary>
        public int IdMandrilTipoEnvio { get; set; }
        public string? Asunto { get; set; }
        public DateTime? FechaEnvio { get; set; }
        /// <summary>
        /// FK asignada por Mandril
        /// </summary>
        public string? FkMandril { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
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
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Indica si el correo es parte de un envio masivo
        /// </summary>
        public bool EsEnvioMasivo { get; set; }
    }
}
