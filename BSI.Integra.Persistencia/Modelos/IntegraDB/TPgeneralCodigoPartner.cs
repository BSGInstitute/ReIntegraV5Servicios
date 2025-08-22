using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPgeneralCodigoPartner
    {
        public TPgeneralCodigoPartner()
        {
            TPgeneralCodigoPartnerModalidadCursos = new HashSet<TPgeneralCodigoPartnerModalidadCurso>();
            TPgeneralCodigoPartnerVersionProgramas = new HashSet<TPgeneralCodigoPartnerVersionPrograma>();
        }

        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// clave foranea de la tabla pla.T_Pgeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Codigo identificados para programa
        /// </summary>
        public string Codigo { get; set; } = null!;
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
        /// Unidad de Desarrollo Profesional (PDU) asignado al al PGeneral
        /// </summary>
        public int? Pdu { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
        public virtual ICollection<TPgeneralCodigoPartnerModalidadCurso> TPgeneralCodigoPartnerModalidadCursos { get; set; }
        public virtual ICollection<TPgeneralCodigoPartnerVersionPrograma> TPgeneralCodigoPartnerVersionProgramas { get; set; }
    }
}
