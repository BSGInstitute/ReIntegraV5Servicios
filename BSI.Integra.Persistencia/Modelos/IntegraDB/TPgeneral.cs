using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPgeneral
    {
        public TPgeneral()
        {
            TAdicionalProgramaGenerals = new HashSet<TAdicionalProgramaGeneral>();
            TCampaniaGeneralDetalleProgramas = new HashSet<TCampaniaGeneralDetallePrograma>();
            TCampaniaMailingDetalleProgramas = new HashSet<TCampaniaMailingDetallePrograma>();
            TCarreraPreRequisitoPgenerals = new HashSet<TCarreraPreRequisitoPgeneral>();
            TConfiguracionBeneficioProgramaGenerals = new HashSet<TConfiguracionBeneficioProgramaGeneral>();
            TConfigurarEvaluacionTrabajos = new HashSet<TConfigurarEvaluacionTrabajo>();
            TConfigurarVideoProgramas = new HashSet<TConfigurarVideoPrograma>();
            TEsquemaEvaluacionPgenerals = new HashSet<TEsquemaEvaluacionPgeneral>();
            TFeedbackGrupoPreguntaProgramaGenerals = new HashSet<TFeedbackGrupoPreguntaProgramaGeneral>();
            TMaterialAdicionalAulaVirtuals = new HashSet<TMaterialAdicionalAulaVirtual>();
            TModeloPredictivoCargos = new HashSet<TModeloPredictivoCargo>();
            TModeloPredictivoCategoriaDatos = new HashSet<TModeloPredictivoCategoriaDato>();
            TModeloPredictivoEscalaProbabilidads = new HashSet<TModeloPredictivoEscalaProbabilidad>();
            TModeloPredictivoFormacions = new HashSet<TModeloPredictivoFormacion>();
            TModeloPredictivoIndustria = new HashSet<TModeloPredictivoIndustrium>();
            TModeloPredictivoTipoDatos = new HashSet<TModeloPredictivoTipoDato>();
            TModeloPredictivoTrabajos = new HashSet<TModeloPredictivoTrabajo>();
            TModeloPredictivos = new HashSet<TModeloPredictivo>();
            TPgeneralCodigoPartners = new HashSet<TPgeneralCodigoPartner>();
            TPgeneralConfiguracionPlantillas = new HashSet<TPgeneralConfiguracionPlantilla>();
            TPgeneralDescripcions = new HashSet<TPgeneralDescripcion>();
            TPgeneralExpositors = new HashSet<TPgeneralExpositor>();
            TPgeneralModalidads = new HashSet<TPgeneralModalidad>();
            TPgeneralParametroSeoPws = new HashSet<TPgeneralParametroSeoPw>();
            TPgeneralVersionProgramas = new HashSet<TPgeneralVersionPrograma>();
            TPreguntaProgramaCapacitacions = new HashSet<TPreguntaProgramaCapacitacion>();
            TProgramaAreaRelacionada = new HashSet<TProgramaAreaRelacionadum>();
            TProgramaGeneralArgumentos = new HashSet<TProgramaGeneralArgumento>();
            TProgramaGeneralMaterialEstudioAdicionals = new HashSet<TProgramaGeneralMaterialEstudioAdicional>();
            TProgramaGeneralPerfilAformacionCoeficientes = new HashSet<TProgramaGeneralPerfilAformacionCoeficiente>();
            TProgramaGeneralPerfilAtrabajoCoeficientes = new HashSet<TProgramaGeneralPerfilAtrabajoCoeficiente>();
            TProgramaGeneralPerfilCargoCoeficientes = new HashSet<TProgramaGeneralPerfilCargoCoeficiente>();
            TProgramaGeneralPerfilCategoriaCoeficientes = new HashSet<TProgramaGeneralPerfilCategoriaCoeficiente>();
            TProgramaGeneralPerfilCiudadCoeficientes = new HashSet<TProgramaGeneralPerfilCiudadCoeficiente>();
            TProgramaGeneralPerfilEscalaProbabilidads = new HashSet<TProgramaGeneralPerfilEscalaProbabilidad>();
            TProgramaGeneralPerfilIndustriaCoeficientes = new HashSet<TProgramaGeneralPerfilIndustriaCoeficiente>();
            TProgramaGeneralPerfilInterceptos = new HashSet<TProgramaGeneralPerfilIntercepto>();
            TProgramaGeneralPerfilModalidadCoeficientes = new HashSet<TProgramaGeneralPerfilModalidadCoeficiente>();
            TProgramaGeneralPerfilScoringAformacions = new HashSet<TProgramaGeneralPerfilScoringAformacion>();
            TProgramaGeneralPerfilScoringAtrabajos = new HashSet<TProgramaGeneralPerfilScoringAtrabajo>();
            TProgramaGeneralPerfilScoringCargos = new HashSet<TProgramaGeneralPerfilScoringCargo>();
            TProgramaGeneralPerfilScoringCategoria = new HashSet<TProgramaGeneralPerfilScoringCategorium>();
            TProgramaGeneralPerfilScoringCiudads = new HashSet<TProgramaGeneralPerfilScoringCiudad>();
            TProgramaGeneralPerfilScoringIndustria = new HashSet<TProgramaGeneralPerfilScoringIndustrium>();
            TProgramaGeneralPerfilScoringModalidads = new HashSet<TProgramaGeneralPerfilScoringModalidad>();
            TProgramaGeneralPerfilTipoDatos = new HashSet<TProgramaGeneralPerfilTipoDato>();
            TProgramaGeneralPresentacionArgumentos = new HashSet<TProgramaGeneralPresentacionArgumento>();
            TProgramaGeneralProblemaDetalles = new HashSet<TProgramaGeneralProblemaDetalle>();
            TProgramaGeneralPuntoCortes = new HashSet<TProgramaGeneralPuntoCorte>();
            TSuscripcionProgramaGenerals = new HashSet<TSuscripcionProgramaGeneral>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key tPGeneral
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Nombre del programa 
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Nombre de la imagen de portada
        /// </summary>
        public string? PwImgPortada { get; set; }
        /// <summary>
        /// Nombre alfanumerico de la imagen de portada
        /// </summary>
        public string? PwImgPortadaAlf { get; set; }
        /// <summary>
        /// Nombre de la imagen secundaria de portada 
        /// </summary>
        public string? PwImgSecundaria { get; set; }
        /// <summary>
        /// Nombre alfanumerico de la imagen secundaria de portada
        /// </summary>
        public string? PwImgSecundariaAlf { get; set; }
        /// <summary>
        /// Es foreing key TPW_Partner
        /// </summary>
        public int? IdPartner { get; set; }
        /// <summary>
        /// Es foreing key TPW_AreaCapacitacion
        /// </summary>
        public int? IdArea { get; set; }
        /// <summary>
        /// Es foreing key  TPW_SubAreaCapacitacion 
        /// </summary>
        public int? IdSubArea { get; set; }
        /// <summary>
        /// Es foreing key tCategoriaP 
        /// </summary>
        public int? IdCategoria { get; set; }
        /// <summary>
        /// Estado del programa en el portal web 
        /// </summary>
        public string? PwEstado { get; set; }
        /// <summary>
        /// Indica si de debe mostrar o no el programa en el portal web
        /// </summary>
        public string? PwMostrarBsplay { get; set; }
        /// <summary>
        /// Duracion del programa en el portal web
        /// </summary>
        public string? PwDuracion { get; set; }
        /// <summary>
        /// Identificador de la columna idBusquedaWeb de la tabla TPLA_PgeneralMatchWeb 
        /// </summary>
        public int? IdBusqueda { get; set; }
        /// <summary>
        /// Es foreing key TCRM_ChatZopim
        /// </summary>
        public int? IdChatZopim { get; set; }
        /// <summary>
        /// Titutlo del programa general
        /// </summary>
        public string? PgTitulo { get; set; }
        /// <summary>
        /// Codigo del programa
        /// </summary>
        public string? Codigo { get; set; }
        /// <summary>
        /// Url de la imagen de portada del programa
        /// </summary>
        public string? UrlImagenPortadaFr { get; set; }
        /// <summary>
        /// Url del brochure del programa
        /// </summary>
        public string? UrlBrochurePrograma { get; set; }
        /// <summary>
        /// Url de la entrega de certificados del partner
        /// </summary>
        public string? UrlPartner { get; set; }
        /// <summary>
        /// Url del programa en el portal web
        /// </summary>
        public string? UrlVersion { get; set; }
        /// <summary>
        /// Titulo del programa en formato html para mostrar en el portal web
        /// </summary>
        public string? PwTituloHtml { get; set; }
        /// <summary>
        /// Bandera que indica si el ProgramaGeneral es Modulo
        /// </summary>
        public bool? EsModulo { get; set; }
        /// <summary>
        /// Nombre Corto del ProgramaGeneral
        /// </summary>
        public string? NombreCorto { get; set; }
        /// <summary>
        /// Identificador de la Pagina a la que esta asociada el ProgramaGeneral
        /// </summary>
        public int? IdPagina { get; set; }
        /// <summary>
        /// Bandera de ChatActivo para ProgramaGeneral
        /// </summary>
        public int? ChatActivo { get; set; }
        /// <summary>
        /// Estado del registro (creado, eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de la ultima modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que hizo la ultima modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Descripciòn General del programa
        /// </summary>
        public string? PwDescripcionGeneral { get; set; }
        /// <summary>
        /// Indica si el programa tiene proyecto de aplicacion practica
        /// </summary>
        public bool? TieneProyectoDeAplicacion { get; set; }
        /// <summary>
        /// Clave foranea de la tabla pla.T_TipoPrograma
        /// </summary>
        public int? IdTipoPrograma { get; set; }
        /// <summary>
        /// Codigo que identifica Curso o Programa
        /// </summary>
        public string? CodigoPartner { get; set; }
        /// <summary>
        /// Nombre de la imagen del logo a asignar al programa general
        /// </summary>
        public string? LogoPrograma { get; set; }
        /// <summary>
        /// Direccion Url de la imagen del logo a asignar al programa general
        /// </summary>
        public string? UrlLogoPrograma { get; set; }
        /// <summary>
        /// Toma la fecha de inicio de un pespecifico Asincronico para mostrar en el portal solo Asincronicos tabla
        /// </summary>
        public DateTime? FechaInicioAsincronico { get; set; }
        /// <summary>
        /// Indica si el programa asigna venta
        /// </summary>
        public bool? AsignaVenta { get; set; }
        /// <summary>
        /// Bandera que indica si el ProgramaGeneral otorga certificado modular
        /// </summary>
        public bool? TieneCertificadoModular { get; set; }
        /// <summary>
        /// Bandera que indica si el ProgramaGeneral su certificado requiere pago
        /// </summary>
        public bool? CertificadoRequierePago { get; set; }
        /// <summary>
        /// apunta al pgeneral inicial
        /// </summary>
        public int? IdPgeneralBase { get; set; }
        /// <summary>
        /// Llave foranea de T_PGeneralPeriodoAsincronico
        /// </summary>
        public int? IdPgeneralPeriodoAsincronico { get; set; }
        /// <summary>
        /// Cantidad de creditos teoria
        /// </summary>
        public double? CreditosTeoricos { get; set; }
        /// <summary>
        /// Cantidad de creditos practica
        /// </summary>
        public double? CreditosPracticos { get; set; }
        /// <summary>
        /// Cantidad de creditos totales
        /// </summary>
        public double? CreditosTotales { get; set; }
        /// <summary>
        /// Cantidad de horas teoricas
        /// </summary>
        public int? HorasTeoricas { get; set; }
        /// <summary>
        /// Cantidad de horas practicas
        /// </summary>
        public int? HorasPracticas { get; set; }
        /// <summary>
        /// Cantidad de horas totales
        /// </summary>
        public int? HorasTotales { get; set; }
        /// <summary>
        /// (1: Formacion, 2: Carrera)
        /// </summary>
        public int? IdTipoProgramaCarrera { get; set; }

        public virtual TPgeneralPeriodoAsincronico? IdPgeneralPeriodoAsincronicoNavigation { get; set; }
        public virtual TTipoPrograma? IdTipoProgramaNavigation { get; set; }
        public virtual ICollection<TAdicionalProgramaGeneral> TAdicionalProgramaGenerals { get; set; }
        public virtual ICollection<TCampaniaGeneralDetallePrograma> TCampaniaGeneralDetalleProgramas { get; set; }
        public virtual ICollection<TCampaniaMailingDetallePrograma> TCampaniaMailingDetalleProgramas { get; set; }
        public virtual ICollection<TCarreraPreRequisitoPgeneral> TCarreraPreRequisitoPgenerals { get; set; }
        public virtual ICollection<TConfiguracionBeneficioProgramaGeneral> TConfiguracionBeneficioProgramaGenerals { get; set; }
        public virtual ICollection<TConfigurarEvaluacionTrabajo> TConfigurarEvaluacionTrabajos { get; set; }
        public virtual ICollection<TConfigurarVideoPrograma> TConfigurarVideoProgramas { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneral> TEsquemaEvaluacionPgenerals { get; set; }
        public virtual ICollection<TFeedbackGrupoPreguntaProgramaGeneral> TFeedbackGrupoPreguntaProgramaGenerals { get; set; }
        public virtual ICollection<TMaterialAdicionalAulaVirtual> TMaterialAdicionalAulaVirtuals { get; set; }
        public virtual ICollection<TModeloPredictivoCargo> TModeloPredictivoCargos { get; set; }
        public virtual ICollection<TModeloPredictivoCategoriaDato> TModeloPredictivoCategoriaDatos { get; set; }
        public virtual ICollection<TModeloPredictivoEscalaProbabilidad> TModeloPredictivoEscalaProbabilidads { get; set; }
        public virtual ICollection<TModeloPredictivoFormacion> TModeloPredictivoFormacions { get; set; }
        public virtual ICollection<TModeloPredictivoIndustrium> TModeloPredictivoIndustria { get; set; }
        public virtual ICollection<TModeloPredictivoTipoDato> TModeloPredictivoTipoDatos { get; set; }
        public virtual ICollection<TModeloPredictivoTrabajo> TModeloPredictivoTrabajos { get; set; }
        public virtual ICollection<TModeloPredictivo> TModeloPredictivos { get; set; }
        public virtual ICollection<TPgeneralCodigoPartner> TPgeneralCodigoPartners { get; set; }
        public virtual ICollection<TPgeneralConfiguracionPlantilla> TPgeneralConfiguracionPlantillas { get; set; }
        public virtual ICollection<TPgeneralDescripcion> TPgeneralDescripcions { get; set; }
        public virtual ICollection<TPgeneralExpositor> TPgeneralExpositors { get; set; }
        public virtual ICollection<TPgeneralModalidad> TPgeneralModalidads { get; set; }
        public virtual ICollection<TPgeneralParametroSeoPw> TPgeneralParametroSeoPws { get; set; }
        public virtual ICollection<TPgeneralVersionPrograma> TPgeneralVersionProgramas { get; set; }
        public virtual ICollection<TPreguntaProgramaCapacitacion> TPreguntaProgramaCapacitacions { get; set; }
        public virtual ICollection<TProgramaAreaRelacionadum> TProgramaAreaRelacionada { get; set; }
        public virtual ICollection<TProgramaGeneralArgumento> TProgramaGeneralArgumentos { get; set; }
        public virtual ICollection<TProgramaGeneralMaterialEstudioAdicional> TProgramaGeneralMaterialEstudioAdicionals { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilAformacionCoeficiente> TProgramaGeneralPerfilAformacionCoeficientes { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilAtrabajoCoeficiente> TProgramaGeneralPerfilAtrabajoCoeficientes { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilCargoCoeficiente> TProgramaGeneralPerfilCargoCoeficientes { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilCategoriaCoeficiente> TProgramaGeneralPerfilCategoriaCoeficientes { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilCiudadCoeficiente> TProgramaGeneralPerfilCiudadCoeficientes { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilEscalaProbabilidad> TProgramaGeneralPerfilEscalaProbabilidads { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilIndustriaCoeficiente> TProgramaGeneralPerfilIndustriaCoeficientes { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilIntercepto> TProgramaGeneralPerfilInterceptos { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilModalidadCoeficiente> TProgramaGeneralPerfilModalidadCoeficientes { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringAformacion> TProgramaGeneralPerfilScoringAformacions { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringAtrabajo> TProgramaGeneralPerfilScoringAtrabajos { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringCargo> TProgramaGeneralPerfilScoringCargos { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringCategorium> TProgramaGeneralPerfilScoringCategoria { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringCiudad> TProgramaGeneralPerfilScoringCiudads { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringIndustrium> TProgramaGeneralPerfilScoringIndustria { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringModalidad> TProgramaGeneralPerfilScoringModalidads { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilTipoDato> TProgramaGeneralPerfilTipoDatos { get; set; }
        public virtual ICollection<TProgramaGeneralPresentacionArgumento> TProgramaGeneralPresentacionArgumentos { get; set; }
        public virtual ICollection<TProgramaGeneralProblemaDetalle> TProgramaGeneralProblemaDetalles { get; set; }
        public virtual ICollection<TProgramaGeneralPuntoCorte> TProgramaGeneralPuntoCortes { get; set; }
        public virtual ICollection<TSuscripcionProgramaGeneral> TSuscripcionProgramaGenerals { get; set; }
    }
}
