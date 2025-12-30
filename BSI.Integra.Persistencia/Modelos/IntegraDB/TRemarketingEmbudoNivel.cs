using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla para gestión de niveles para los esquemas de embudo de remarketing. Define la jerarquía y criterios de clasificación para leads/prospectos.
    /// </summary>
    public partial class TRemarketingEmbudoNivel
    {
        public TRemarketingEmbudoNivel()
        {
            TRemarketingEmbudoHistoricos = new HashSet<TRemarketingEmbudoHistorico>();
        }

        /// <summary>
        /// Identificador único del nivel de embudo. Clave primaria autoincremental.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Referencia al esquema de embudo al que pertenece este nivel. Clave foránea a [ia].[T_RemarketingEmbudoEsquema].
        /// </summary>
        public int IdRemarketingEmbudoEsquema { get; set; }
        /// <summary>
        /// Código único del nivel dentro del esquema.
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Nombre descriptivo del nivel.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción general del nivel. Resumen breve de su propósito y características principales.
        /// </summary>
        public string DescripcionGeneral { get; set; } = null!;
        /// <summary>
        /// Descripción detallada del nivel.
        /// </summary>
        public string DescripcionDetalle { get; set; } = null!;
        /// <summary>
        /// Orden de secuencia del nivel dentro del esquema.
        /// </summary>
        public int Orden { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del Token
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion del Token
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion del Token
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del Token
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TRemarketingEmbudoEsquema IdRemarketingEmbudoEsquemaNavigation { get; set; } = null!;
        public virtual ICollection<TRemarketingEmbudoHistorico> TRemarketingEmbudoHistoricos { get; set; }
    }
}
