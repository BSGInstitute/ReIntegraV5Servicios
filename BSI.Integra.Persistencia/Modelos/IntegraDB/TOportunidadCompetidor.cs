using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOportunidadCompetidor
    {
        public TOportunidadCompetidor()
        {
            TDetalleOportunidadCompetidors = new HashSet<TDetalleOportunidadCompetidor>();
            TOportunidadBeneficios = new HashSet<TOportunidadBeneficio>();
            TOportunidadPrerequisitoEspecificos = new HashSet<TOportunidadPrerequisitoEspecifico>();
            TOportunidadPrerequisitoGenerals = new HashSet<TOportunidadPrerequisitoGeneral>();
        }

        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OportunidadNew
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Descripcion de un beneficio particular
        /// </summary>
        public string OtroBeneficio { get; set; } = null!;
        /// <summary>
        /// Id respuesta
        /// </summary>
        public int Respuesta { get; set; }
        /// <summary>
        /// descripcion respuesta
        /// </summary>
        public string Completado { get; set; } = null!;
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

        public virtual TOportunidad? IdOportunidadNavigation { get; set; }
        public virtual ICollection<TDetalleOportunidadCompetidor> TDetalleOportunidadCompetidors { get; set; }
        public virtual ICollection<TOportunidadBeneficio> TOportunidadBeneficios { get; set; }
        public virtual ICollection<TOportunidadPrerequisitoEspecifico> TOportunidadPrerequisitoEspecificos { get; set; }
        public virtual ICollection<TOportunidadPrerequisitoGeneral> TOportunidadPrerequisitoGenerals { get; set; }
    }
}
