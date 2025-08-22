using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCertificadoGeneradoAutomaticoContenido
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave foranea de la tabla pla.T_CertificadoGeneradoAutomatico
        /// </summary>
        public int IdCertificadoGeneradoAutomatico { get; set; }
        /// <summary>
        /// valor etiqueta Nombres de Alumno
        /// </summary>
        public string NombreAlumno { get; set; } = null!;
        /// <summary>
        /// Valor Etiqueta Nombre Programa
        /// </summary>
        public string? NombrePrograma { get; set; }
        /// <summary>
        /// Valor Etiqueta Duracion
        /// </summary>
        public int? DuracionPespecifico { get; set; }
        /// <summary>
        /// Valor Etiqueta Nombre CentroCosto
        /// </summary>
        public string? NombreCentroCosto { get; set; }
        /// <summary>
        /// Valor Etiqueta  Ciudad
        /// </summary>
        public string Ciudad { get; set; } = null!;
        /// <summary>
        /// Valor Etiqueta EscalaCalificacion
        /// </summary>
        public int? EscalaCalificacion { get; set; }
        /// <summary>
        /// Valor Etiqueta FechaInicioCapacitacion
        /// </summary>
        public string FechaInicioCapacitacion { get; set; } = null!;
        /// <summary>
        /// Valor Etiqueta FechaFinCapacitacion
        /// </summary>
        public string FechaFinCapacitacion { get; set; } = null!;
        /// <summary>
        /// Valor Etiqueta CalificacionPromedio
        /// </summary>
        public int? CalificacionPromedio { get; set; }
        /// <summary>
        /// Valor Etiqueta FechaEmisionCertificado
        /// </summary>
        public string? FechaEmisionCertificado { get; set; }
        /// <summary>
        /// Valor Etiqueta CorrelativoConstancia
        /// </summary>
        public int? CorrelativoConstancia { get; set; }
        /// <summary>
        /// Valor Etiqueta CronogramaNotas
        /// </summary>
        public string? CronogramaNota { get; set; }
        /// <summary>
        /// Valor Etiqueta Cronograma Asistencia
        /// </summary>
        public string? CronogramaAsistencia { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de la tabla
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// contenido Certificado Etiqueta Estructura curricular
        /// </summary>
        public string? EstructuraCurricular { get; set; }
        /// <summary>
        /// contenido Certificado CodigoPartner
        /// </summary>
        public string? CodigoPartner { get; set; }
        /// <summary>
        /// contenido Certificado url CodigoQr
        /// </summary>
        public string? CodigoQr { get; set; }
    }
}
