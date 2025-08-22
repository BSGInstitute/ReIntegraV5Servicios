using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TEstructuraEspecifica
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key de fin.T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Es foreing key de pla.T_Pgeneral, Id del programa Padre
        /// </summary>
        public int IdPgeneralPadre { get; set; }
        /// <summary>
        /// Es foreing key de pla.T_Pgeneral, Id del programa Hijo
        /// </summary>
        public int IdPgeneralHijo { get; set; }
        /// <summary>
        /// Fecha en la que se hizo el congelamiento
        /// </summary>
        public DateTime FechaRegistro { get; set; }
        /// <summary>
        /// Fecha de inicio de capacitacion
        /// </summary>
        public DateTime? FechaInicio { get; set; }
        /// <summary>
        /// Fecha de Fin de capacitacion
        /// </summary>
        public DateTime? FechaFin { get; set; }
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
