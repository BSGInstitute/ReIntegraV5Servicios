using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPgeneralConfiguracionPlantillaDetalle
    {
        public TPgeneralConfiguracionPlantillaDetalle()
        {
            TPgeneralConfiguracionPlantillaEstadoMatriculas = new HashSet<TPgeneralConfiguracionPlantillaEstadoMatricula>();
            TPgeneralConfiguracionPlantillaSubEstadoMatriculas = new HashSet<TPgeneralConfiguracionPlantillaSubEstadoMatricula>();
        }

        /// <summary>
        /// Clave Primaria de la Tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave Forane de la tabla pla.T_PgeneralConfiguracionPlantilla
        /// </summary>
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        /// <summary>
        /// Clave Forane de la tabla pla.T_ModalidadCurso
        /// </summary>
        public int IdModalidadCurso { get; set; }
        /// <summary>
        /// Clave Foranea de la tabla mkt.T_OperadorComparacion
        /// </summary>
        public int? IdOperadorComparacion { get; set; }
        /// <summary>
        /// Criterio de nota de Aprobacion
        /// </summary>
        public decimal? NotaAprobatoria { get; set; }
        /// <summary>
        /// Criterio para ver si aplica deudapendiente
        /// </summary>
        public bool DeudaPendiente { get; set; }
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

        public virtual TModalidadCurso IdModalidadCursoNavigation { get; set; } = null!;
        public virtual TOperadorComparacion? IdOperadorComparacionNavigation { get; set; }
        public virtual TPgeneralConfiguracionPlantilla IdPgeneralConfiguracionPlantillaNavigation { get; set; } = null!;
        public virtual ICollection<TPgeneralConfiguracionPlantillaEstadoMatricula> TPgeneralConfiguracionPlantillaEstadoMatriculas { get; set; }
        public virtual ICollection<TPgeneralConfiguracionPlantillaSubEstadoMatricula> TPgeneralConfiguracionPlantillaSubEstadoMatriculas { get; set; }
    }
}
