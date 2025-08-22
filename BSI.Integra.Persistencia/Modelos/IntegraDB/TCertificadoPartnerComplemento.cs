using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCertificadoPartnerComplemento
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Codigo Interno
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Decripcion
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Texto para la parte FrontalCentral
        /// </summary>
        public string? FrontalCentral { get; set; }
        /// <summary>
        /// Texto para el campo FrontalInferiorIzquierda
        /// </summary>
        public string? FrontalInferiorIzquierda { get; set; }
        /// <summary>
        /// Texto para la Parte Posterior Central del Certificado
        /// </summary>
        public string? PosteriorCentral { get; set; }
        /// <summary>
        /// Texto para el campo la parte PosteriorInferiorIzquierda
        /// </summary>
        public string? PosteriorInferiorIzquierda { get; set; }
        /// <summary>
        /// Texto para el campo de Mencion en Certificado
        /// </summary>
        public string? MencionEnCertificado { get; set; }
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
