using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDocumentoSeccionPw
    {
        public TDocumentoSeccionPw()
        {
            TConfigurarVideoProgramas = new HashSet<TConfigurarVideoPrograma>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Titulo de la seccion
        /// </summary>
        public string Titulo { get; set; } = null!;
        /// <summary>
        /// Contenido de la seccion
        /// </summary>
        public string? Contenido { get; set; }
        /// <summary>
        /// Es Foreing Key T_Plantilla_PW
        /// </summary>
        public int IdPlantillaPw { get; set; }
        /// <summary>
        /// Posicion u orden de la seccion
        /// </summary>
        public int Posicion { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public int Tipo { get; set; }
        /// <summary>
        /// Es Foreing Key T_Documento
        /// </summary>
        public int IdDocumentoPw { get; set; }
        /// <summary>
        /// Es Foreing Key T_Seccion
        /// </summary>
        public int IdSeccionPw { get; set; }
        /// <summary>
        /// Si se publica en la web (no se usa)
        /// </summary>
        public bool VisibleWeb { get; set; }
        /// <summary>
        /// En que zona de la web se visualizara (programa)
        /// </summary>
        public int? ZonaWeb { get; set; }
        /// <summary>
        /// Orden en el que debe aparecer en la web (programa)
        /// </summary>
        public int? OrdenWeb { get; set; }
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
        /// <summary>
        /// Llave foranea con la tabla T_SeccionTipoDetalle
        /// </summary>
        public int? IdSeccionTipoDetallePw { get; set; }
        /// <summary>
        /// Indica el numero de fila al que pertenece el registro
        /// </summary>
        public int? NumeroFila { get; set; }
        /// <summary>
        /// Indica la cabecera que tiene el registro
        /// </summary>
        public string? Cabecera { get; set; }
        /// <summary>
        /// Indica el pie de pagina que tiene el registro
        /// </summary>
        public string? PiePagina { get; set; }

        public virtual TDocumentoPw IdDocumentoPwNavigation { get; set; } = null!;
        public virtual TPlantillaPw IdPlantillaPwNavigation { get; set; } = null!;
        public virtual TSeccionPw IdSeccionPwNavigation { get; set; } = null!;
        public virtual ICollection<TConfigurarVideoPrograma> TConfigurarVideoProgramas { get; set; }
    }
}
