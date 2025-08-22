using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena Codigos Asociados al PEspecifico
    /// </summary>
    public partial class TPespecificoCodigoPartner
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Codigo asignado al PEspecifico
        /// </summary>
        public string? Codigo { get; set; }
        /// <summary>
        /// Unidad de Desarrollo Profesional (PDU) asignado al Curso PEspecifico Especializado
        /// </summary>
        public int? Pdu { get; set; }
        /// <summary>
        /// Fecha Inicio del Curso Especializado Asociado
        /// </summary>
        public DateTime? FechaInicio { get; set; }
        /// <summary>
        /// Estado del Año
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del Codigo Partner PEspecifico
        /// </summary>
        public string? UsuarioCreacion { get; set; }
        /// <summary>
        /// Usuario Modificacion del Codigo Partner PEspecifico
        /// </summary>
        public string? UsuarioModificacion { get; set; }
        /// <summary>
        /// Fecha Creacion del Codigo Partner PEspecifico
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del Codigo Partner PEspecifico
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda el Codigo Partner PEspecifico
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TPespecifico IdPespecificoNavigation { get; set; } = null!;
    }
}
