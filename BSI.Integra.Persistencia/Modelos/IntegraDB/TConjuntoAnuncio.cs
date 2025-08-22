using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConjuntoAnuncio
    {
        public TConjuntoAnuncio()
        {
            InverseIdConjuntoAnuncioFuenteNavigation = new HashSet<TConjuntoAnuncio>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del anuncio
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Es foreing key T_CategoriaOrigen
        /// </summary>
        public int? IdCategoriaOrigen { get; set; }
        /// <summary>
        /// Forma en la que las personas llegan al anuncio
        /// </summary>
        public string? Origen { get; set; }
        /// <summary>
        /// Identicador de la columna id_facebook en la tabla TFM_ConjuntoAnunciosFacebook (es el id de la campaña en facebook)
        /// </summary>
        public string? IdConjuntoAnuncioFacebook { get; set; }
        public DateTime? FechaCreacionCampania { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
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
        /// FK de T_ConjuntoAnuncioFuente
        /// </summary>
        public int? IdConjuntoAnuncioFuente { get; set; }
        /// <summary>
        /// FK de pla.T_CentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// FK de pla.T_ConjuntoAnuncioTipoObjetivo
        /// </summary>
        public int? IdConjuntoAnuncioTipoObjetivo { get; set; }
        /// <summary>
        /// FK de mkt.T_FormularioPlantilla
        /// </summary>
        public int? IdFormularioPlantilla { get; set; }
        /// <summary>
        /// Caracteristica Adicional del conjunto Anuncio
        /// </summary>
        public string? Adicional { get; set; }
        /// <summary>
        /// Enlace al formulario del conjunto Anuncio en el portal web
        /// </summary>
        public string? EnlaceFormulario { get; set; }
        /// <summary>
        /// Determina si su plantilla heredara el Centro de Costo del ConjuntoAnuncio
        /// </summary>
        public bool? EsGrupal { get; set; }
        /// <summary>
        /// Determina si su plantilla sera utilizada para una pagina web standar
        /// </summary>
        public bool? EsPaginaWeb { get; set; }
        /// <summary>
        /// Determina si su plantilla sera utilizada para un prelanzamiento e identificara que el tipo de plantilla permita registrar url de imagenes
        /// </summary>
        public bool? EsPreLanzamiento { get; set; }
        /// <summary>
        /// fk de mkt.T_ConjuntoAnuncioSegmento [Segmento a donde va dirigido la publicidad]
        /// </summary>
        public int? IdConjuntoAnuncioSegmento { get; set; }
        /// <summary>
        /// FK de mkt.T_ConjuntoAnuncioTipoGenero [genero de persona a quien va dirigido la publicidad]
        /// </summary>
        public int? IdConjuntoAnuncioTipoGenero { get; set; }
        /// <summary>
        /// FK de conf.T_Pais: Pais donde se imprimira la publicidad. Se usara el CodigoISO para su abreviacion
        /// </summary>
        public int? IdPais { get; set; }
        public string? Propietario { get; set; }
        public string? NumeroAnuncio { get; set; }
        public string? NumeroSemana { get; set; }
        public string? DiaEnvio { get; set; }
        /// <summary>
        /// Nombre Inicial del Conjunto de Anuncio
        /// </summary>
        public string? NombreInicial { get; set; }

        public virtual TConjuntoAnuncio? IdConjuntoAnuncioFuenteNavigation { get; set; }
        public virtual TConjuntoAnuncioTipoObjetivo? IdConjuntoAnuncioTipoObjetivoNavigation { get; set; }
        public virtual TFormularioPlantilla? IdFormularioPlantillaNavigation { get; set; }
        public virtual ICollection<TConjuntoAnuncio> InverseIdConjuntoAnuncioFuenteNavigation { get; set; }
    }
}
