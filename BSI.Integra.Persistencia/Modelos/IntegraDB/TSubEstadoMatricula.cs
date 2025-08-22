using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSubEstadoMatricula
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del sub estado de matricula
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Es clave foranea de T_EstadoMatricula
        /// </summary>
        public int IdEstadoMatricula { get; set; }
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
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
        public int? AvanceAcademicoValor1 { get; set; }
        public int? AvanceAcademicoValor2 { get; set; }
        public int? IdParametrocomparativoAvanceAcademico { get; set; }
        public string? IdEstadoPago { get; set; }
        public int? NotaPromedioValor1 { get; set; }
        public int? NotaPromedioValor2 { get; set; }
        public int? IdParametrocomparativoNotaPromedio { get; set; }
        public bool? TieneDeuda { get; set; }
        public bool? ProyectoFinal { get; set; }
        public bool? RequiereVerificacionInformacion { get; set; }
    }
}
