using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCertificadoDetalle
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_CertificadoBrochure
        /// </summary>
        public int? IdCertificadoBrochure { get; set; }
        /// <summary>
        /// Llave foranea a la tabla T_CertificadoSolicitud
        /// </summary>
        public int IdCertificadoSolicitud { get; set; }
        /// <summary>
        /// Llave Foranea a la tabla T_CertificadoTipo
        /// </summary>
        public int IdCertificadoTipo { get; set; }
        /// <summary>
        /// Flag para indicar si se utilizara el el formato de certificado o diploma
        /// </summary>
        public bool EsDiploma { get; set; }
        /// <summary>
        /// Llave Foranea a la tabla T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Codigo del certificado
        /// </summary>
        public string? CodigoCertificado { get; set; }
        /// <summary>
        /// Fecha de Inicio de clases
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha de Termino de clases
        /// </summary>
        public DateTime FechaTermino { get; set; }
        /// <summary>
        /// Nota
        /// </summary>
        public decimal? Nota { get; set; }
        /// <summary>
        /// Fecha de envio del certificado
        /// </summary>
        public DateTime? FechaEnvio { get; set; }
        /// <summary>
        /// Fecha de recepcion del certificado
        /// </summary>
        public DateTime? FechaRecepcion { get; set; }
        /// <summary>
        /// Escala de Calificación
        /// </summary>
        public decimal? EscalaCalificacion { get; set; }
        /// <summary>
        /// Fecha de Emision del Certificado
        /// </summary>
        public DateTime? FechaEmision { get; set; }
        /// <summary>
        /// Configuracion del tamaño fuente del nombre de alumno
        /// </summary>
        public int? TamanioFuenteNombreAlumno { get; set; }
        /// <summary>
        /// Configuracion del tamaño fuente del nombre del programa
        /// </summary>
        public int? TamanioFuenteNombrePrograma { get; set; }
        /// <summary>
        /// Nombre del archivo cara frontal del certificado
        /// </summary>
        public string? NombreArchivoFrontal { get; set; }
        /// <summary>
        /// Nombre del archivo cara reverso del certificado
        /// </summary>
        public string? NombreArchivoReverso { get; set; }
        /// <summary>
        /// Nombre del archivo cara frontal impresion del certificado
        /// </summary>
        public string? NombreArchivoFrontalImpresion { get; set; }
        /// <summary>
        /// Nombre del archivo cara reverso impresion del certificado
        /// </summary>
        public string? NombreArchivoReversoImpresion { get; set; }
        /// <summary>
        /// Ruta del Archivo
        /// </summary>
        public string? RutaArchivo { get; set; }
        public int? IdUrlBlockStorage { get; set; }
        /// <summary>
        /// Tipo de datos de los archivos generados
        /// </summary>
        public string? ContentType { get; set; }
        /// <summary>
        /// Direccion de envio a utilizar
        /// </summary>
        public string? DireccionEntrega { get; set; }
        /// <summary>
        /// Fecha de el Ultimo envio al alumno de la tabla ra_Certificado_Envio_CertificadoDigital
        /// </summary>
        public DateTime? FechaUltimoEnvioAlumno { get; set; }
        /// <summary>
        /// Relacion con la tabla T_CertificadoTipoPrograma
        /// </summary>
        public int? IdCertificadoTipoPrograma { get; set; }
        /// <summary>
        /// Campo especial se utiliza para visualizar la informacion del partner en el caso de certificados de asistencia
        /// </summary>
        public bool? EsAsistenciaPartner { get; set; }
        /// <summary>
        /// indica si aplica tambien para certificado partner
        /// </summary>
        public bool AplicaPartner { get; set; }
        /// <summary>
        /// Llave Foranea de la tabla pla.T_Pespecifico
        /// </summary>
        public int IdPespecifico { get; set; }
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
    }
}
