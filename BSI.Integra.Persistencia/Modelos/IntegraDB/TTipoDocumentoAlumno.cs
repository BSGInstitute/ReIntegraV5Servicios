using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTipoDocumentoAlumno
    {
        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre Tipo Documento
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Es Foreigh Key de T_Plantilla
        /// </summary>
        public int IdPlantillaFrontal { get; set; }
        /// <summary>
        /// Es Foreigh Key de T_Plantilla
        /// </summary>
        public int IdPlantillaPosterior { get; set; }
        /// <summary>
        /// Es foreigh Key de T_OperadorComparacion
        /// </summary>
        public int IdOperadorComparacion { get; set; }
        public bool TieneDeuda { get; set; }
        /// <summary>
        /// Para saber si el registro fue eliminado de forma logica
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
