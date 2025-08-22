using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTokenPostulanteProcesoSeleccion
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_PostulanteProcesoSeleccion
        /// </summary>
        public int IdPostulanteProcesoSeleccion { get; set; }
        /// <summary>
        /// Token de accesos del postulante
        /// </summary>
        public string Token { get; set; } = null!;
        public string TokenHash { get; set; } = null!;
        /// <summary>
        /// Guid que se usa para armar el url al que el postulante accedera para el proceso de seleccion
        /// </summary>
        public Guid GuidAccess { get; set; }
        /// <summary>
        /// Estado del token
        /// </summary>
        public bool Activo { get; set; }
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
        /// <summary>
        /// Fecha en la que se enviaron los accesos al alumno
        /// </summary>
        public DateTime? FechaEnvioAccesos { get; set; }
    }
}
