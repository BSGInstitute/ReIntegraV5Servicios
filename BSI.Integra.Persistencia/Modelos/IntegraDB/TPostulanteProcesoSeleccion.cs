using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPostulanteProcesoSeleccion
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_Postulante
        /// </summary>
        public int IdPostulante { get; set; }
        /// <summary>
        /// Fk T_ProcesoSeleccion
        /// </summary>
        public int IdProcesoSeleccion { get; set; }
        public DateTime FechaRegistro { get; set; }
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
        /// Fk tabla T_EstadoProcesoSeleccion
        /// </summary>
        public int? IdEstadoProcesoSeleccion { get; set; }
        /// <summary>
        /// PK de gp.T_PostulanteNivelPotencial
        /// </summary>
        public int? IdPostulanteNivelPotencial { get; set; }
        /// <summary>
        /// PK de fin.T_Proveedor
        /// </summary>
        public int? IdProveedor { get; set; }
        /// <summary>
        /// PK de gp.T_Personal
        /// </summary>
        public int? IdPersonalOperadorProceso { get; set; }
        /// <summary>
        /// PK de gp.T_ConvocatoriaPersonal
        /// </summary>
        public int? IdConvocatoriaPersonal { get; set; }
    }
}
