using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPlantillaPw
    {
        public TPlantillaPw()
        {
            TDocumentoSeccionPws = new HashSet<TDocumentoSeccionPw>();
            TPlantillaPais = new HashSet<TPlantillaPai>();
            TPlantillaRevisionPws = new HashSet<TPlantillaRevisionPw>();
            TSeccionPws = new HashSet<TSeccionPw>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la plantilla
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion de la plantilla creada
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Es Foreing Key T_PlantillaMaestro_Pw
        /// </summary>
        public int IdPlantillaMaestroPw { get; set; }
        /// <summary>
        /// Es Foreing Key T_Revision_Pw
        /// </summary>
        public int IdRevisionPw { get; set; }
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

        public virtual ICollection<TDocumentoSeccionPw> TDocumentoSeccionPws { get; set; }
        public virtual ICollection<TPlantillaPai> TPlantillaPais { get; set; }
        public virtual ICollection<TPlantillaRevisionPw> TPlantillaRevisionPws { get; set; }
        public virtual ICollection<TSeccionPw> TSeccionPws { get; set; }
    }
}
