using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMatriculaCabeceraDatosCertificado
    {
        /// <summary>
        /// 
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la tabla TMatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Durancion del certificado
        /// </summary>
        public string Duracion { get; set; } = null!;
        /// <summary>
        /// Fecha de inicio del certificado
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha final del certificado
        /// </summary>
        public DateTime FechaFinal { get; set; }
        /// <summary>
        /// Nombre del curso del certificado
        /// </summary>
        public string NombreCurso { get; set; } = null!;
        /// <summary>
        /// Estado de la modificacion de los datos del certificado
        /// </summary>
        public bool EstadoCambioDatos { get; set; }
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
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Identificador de tabla certificadogeneradoautomatico
        /// </summary>
        public int? IdCertificadoGeneradoAutomatico { get; set; }
    }
}
