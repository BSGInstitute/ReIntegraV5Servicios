using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProgramaGeneralBeneficio
    {
        public TProgramaGeneralBeneficio()
        {
            TProgramaGeneralBeneficioArgumentos = new HashSet<TProgramaGeneralBeneficioArgumento>();
            TProgramaGeneralBeneficioModalidads = new HashSet<TProgramaGeneralBeneficioModalidad>();
        }

        /// <summary>
        /// Identificador de la tabla 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key T_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Nombre del beneficio
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TProgramaGeneralBeneficioArgumento> TProgramaGeneralBeneficioArgumentos { get; set; }
        public virtual ICollection<TProgramaGeneralBeneficioModalidad> TProgramaGeneralBeneficioModalidads { get; set; }
    }
}
