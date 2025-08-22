using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCertificadoGeneradoAutomatico
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave foranea de la tabla fin.T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Clave Foranea de la Tabla pla.T_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Fecha de Emision del Certificado
        /// </summary>
        public DateTime? FechaEmision { get; set; }
        /// <summary>
        /// Clave foranea de la tabla ope.T_UrlBlockStorage
        /// </summary>
        public int IdUrlBlockStorage { get; set; }
        /// <summary>
        /// Tipo de datos de los archivos generados
        /// </summary>
        public string ContentType { get; set; } = null!;
        /// <summary>
        /// Nombre del Archivo
        /// </summary>
        public string NombreArchivo { get; set; } = null!;
        /// <summary>
        /// Clave Foranea de la tabla pla.T_PgeneralConfiguracionPlantilla
        /// </summary>
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        /// <summary>
        /// Clave foranea de la tabla pla.T_Pespecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Clave foranea de la tabla mkt.T_Plantilla
        /// </summary>
        public int IdPlantilla { get; set; }
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
        public int? IdCronogramaPagoTarifario { get; set; }
    }
}
