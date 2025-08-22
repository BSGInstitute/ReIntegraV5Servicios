using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCertificadoTipoPrograma
    {
        /// <summary>
        /// Llave Primaria de la Tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del programa que ira como texto en el certificado
        /// </summary>
        public string NombreProgramaCertificado { get; set; } = null!;
        /// <summary>
        /// Codigo interno de manejo del tipo
        /// </summary>
        public string? Codigo { get; set; }
        /// <summary>
        /// Flag para indicar si corresponde fondo de Certificado
        /// </summary>
        public bool AplicaFondoDiploma { get; set; }
        /// <summary>
        /// Flag para indicar si corresponde parrafo de se otorga
        /// </summary>
        public bool AplicaSeOtorga { get; set; }
        /// <summary>
        /// Flag para indicar si corresponde nota
        /// </summary>
        public bool AplicaNota { get; set; }
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
