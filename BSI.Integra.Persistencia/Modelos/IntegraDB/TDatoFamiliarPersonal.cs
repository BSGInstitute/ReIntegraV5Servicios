using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDatoFamiliarPersonal
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key Do.TDO_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Es foreing key Do.TDO_Sexo
        /// </summary>
        public int IdSexo { get; set; }
        /// <summary>
        /// Es foreing key Do.TDO_Parentesco
        /// </summary>
        public int IdParentescoPersonal { get; set; }
        /// <summary>
        /// Numero de telefono de referencia
        /// </summary>
        public int IdTipoDocumentoPersonal { get; set; }
        /// <summary>
        /// Apellidos y nombres del familiar
        /// </summary>
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateTime FechaNacimiento { get; set; }
        public string? NumeroDocumento { get; set; }
        /// <summary>
        /// Numero de telefono de referencia 
        /// </summary>
        public string? NumeroReferencia1 { get; set; }
        /// <summary>
        /// Numero de telefono de referencia
        /// </summary>
        public string? NumeroReferencia2 { get; set; }
        public bool DerechoHabiente { get; set; }
        public bool EsContactoInmediato { get; set; }
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
        /// Sistema Automatico Fecha de Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Relacion con el id de la tabla original
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
