using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFiltroSegmento
    {
        public TFiltroSegmento()
        {
            TCampaniaGenerals = new HashSet<TCampaniaGeneral>();
            TFiltroSegmentoDetalles = new HashSet<TFiltroSegmentoDetalle>();
            TFiltroSegmentoValorTipos = new HashSet<TFiltroSegmentoValorTipo>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de solicitudes para un programa general
        /// </summary>
        public int? IdOperadorComparacionNroSolicitudInformacionPg { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de solicitudes por área
        /// </summary>
        public int? IdOperadorComparacionNroSolicitudInformacionArea { get; set; }
        /// <summary>
        /// Indica el número de solicitudes de información por área
        /// </summary>
        public int? NroSolicitudInformacionArea { get; set; }
        /// <summary>
        /// Indica el número de correos abiertos por el contacto por un envio masivo - mailchimp
        /// </summary>
        public int? NroCorreosAbiertosMailChimp { get; set; }
        /// <summary>
        /// Indica el número de solicitudes de información por sub área
        /// </summary>
        public int? NroSolicitudInformacionSubArea { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de clicks realizados por el contacto en el enlace por un envio masivo - mailchimp
        /// </summary>
        public int? IdOperadorComparacionNroClicksEnlaceMailChimp { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de solicitudes de informacioon por sub area
        /// </summary>
        public int? IdOperadorComparacionNroSolicitudInformacionSubArea { get; set; }
        /// <summary>
        /// Indica el número de correos no abiertos por el contacto por un envio masivo - mailchimp
        /// </summary>
        public int? NroCorreosNoAbiertosMailChimp { get; set; }
        /// <summary>
        /// Indica el número de click realizados por el contacto en el enlace por un envio masivo - mailchimp
        /// </summary>
        public int? NroClicksEnlaceMailChimp { get; set; }
        /// <summary>
        /// Indica si el filtro considerará la interaccion de suscripcion
        /// </summary>
        public bool EsSuscribirme { get; set; }
        /// <summary>
        /// Indica si el filtro considerará la interaccion de desuscripcion
        /// </summary>
        public bool EsDesuscribirme { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de correos no abiertos por el contacto por un envio masivo - mailchimp
        /// </summary>
        public int? IdOperadorComparacionNroCorreosNoAbiertosMailChimp { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de correos abiertos por el contacto por un envio masivo - mailchimp
        /// </summary>
        public int? IdOperadorComparacionNroCorreosAbiertosMailChimp { get; set; }
        /// <summary>
        /// Indica el número de solicitudes de información por programa general
        /// </summary>
        public int? NroSolicitudInformacionPg { get; set; }
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
        /// Es llave foranea de conf.T_FiltroSegmentoTipoContacto
        /// </summary>
        public int? IdFiltroSegmentoTipoContacto { get; set; }
        /// <summary>
        /// Fecha de inicio de fecha creacion de la ultima oportunidad del contacto
        /// </summary>
        public DateTime? FechaInicioCreacionUltimaOportunidad { get; set; }
        /// <summary>
        /// Fecha de fin de fecha creacion de la ultima oportunidad del contacto
        /// </summary>
        public DateTime? FechaFinCreacionUltimaOportunidad { get; set; }
        /// <summary>
        /// Fecha de inicio de fecha creacion desde cuando no se tuvo contacto con el cliente
        /// </summary>
        public DateTime? FechaInicioModificacionUltimaActividadDetalle { get; set; }
        /// <summary>
        /// Fecha de fin de fecha creacion desde cuando no se tuvo contacto con el cliente
        /// </summary>
        public DateTime? FechaFinModificacionUltimaActividadDetalle { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion a la cantidad de solicitudes de informacion que hizo el alumno
        /// </summary>
        public int? IdOperadorComparacionNroSolicitudInformacion { get; set; }
        /// <summary>
        /// Indica el numero de solicitudes de informacion realizadas por un contacto
        /// </summary>
        public int? NroSolicitudInformacion { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion a la cantidad de oportunidades de un contacto
        /// </summary>
        public int? IdOperadorComparacionNroOportunidades { get; set; }
        /// <summary>
        /// Cantidad de oportunidades creadas para un contacto
        /// </summary>
        public int? NroOportunidades { get; set; }
        /// <summary>
        /// Indica si se consideraran solo la fase RN2 para el filtro
        /// </summary>
        public bool EsRn2 { get; set; }
        /// <summary>
        /// Fecha de inicio de ultima actividad programada RN2
        /// </summary>
        public DateTime? FechaInicioProgramacionUltimaActividadDetalleRn2 { get; set; }
        /// <summary>
        /// Fecha de fin de ultima actividad programada RN2
        /// </summary>
        public DateTime? FechaFinProgramacionUltimaActividadDetalleRn2 { get; set; }
        /// <summary>
        /// Indica la fecha de inicio a considerar para el filtro de interaccion por formularios
        /// </summary>
        public DateTime? FechaInicioFormulario { get; set; }
        /// <summary>
        /// Indica la fecha de fin a considerar para el filtro de interaccion por formularios
        /// </summary>
        public DateTime? FechaFinFormulario { get; set; }
        /// <summary>
        /// Indica la fecha de inicio a considerar para el filtro de interaccion por el chat integra
        /// </summary>
        public DateTime? FechaInicioChatIntegra { get; set; }
        /// <summary>
        /// Indica la fecha de fin a considerar para el filtro de interaccion por el chat integra
        /// </summary>
        public DateTime? FechaFinChatIntegra { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al tiempo maximo de respuesta del usuario en el chat integra online
        /// </summary>
        public int? IdOperadorComparacionTiempoMaximoRespuestaChatOnline { get; set; }
        /// <summary>
        /// Indica el tiempo de respuesta maximo del usuario en el chat integra online
        /// </summary>
        public int? TiempoMaximoRespuestaChatOnline { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de palabras en el chat integra online
        /// </summary>
        public int? IdOperadorComparacionNroPalabrasClienteChatOnline { get; set; }
        /// <summary>
        /// Indica el número de palabras del cliente en el chat integra online
        /// </summary>
        public int? NroPalabrasClienteChatOnline { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al tiempo promedio de respuesta del usuario en el chat integra online
        /// </summary>
        public int? IdOperadorComparacionTiempoPromedioRespuestaChatOnline { get; set; }
        /// <summary>
        /// Indica el tiempo promedio de respuesta en el chat integra online
        /// </summary>
        public int? TiempoPromedioRespuestaChatOnline { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de palabras del cliente en el chat integra offline
        /// </summary>
        public int? IdOperadorComparacionNroPalabrasClienteChatOffline { get; set; }
        /// <summary>
        /// Indica el número de palabras del cliente en el chat integra offline
        /// </summary>
        public int? NroPalabrasClienteChatOffline { get; set; }
        /// <summary>
        /// Indica la fecha de inicio a considerar para el filtro de interaccion por correo
        /// </summary>
        public DateTime? FechaInicioCorreo { get; set; }
        /// <summary>
        /// Indica la fecha de fin a considerar para el filtro de interaccion por correo
        /// </summary>
        public DateTime? FechaFinCorreo { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de correos abiertos por el contacto
        /// </summary>
        public int? IdOperadorComparacionNroCorreosAbiertos { get; set; }
        /// <summary>
        /// Indica el número de correos abiertos por el contacto
        /// </summary>
        public int? NroCorreosAbiertos { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de correos no abiertos por el contacto
        /// </summary>
        public int? IdOperadorComparacionNroCorreosNoAbiertos { get; set; }
        /// <summary>
        /// Indica el número de correos no abiertos por el contacto
        /// </summary>
        public int? NroCorreosNoAbiertos { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de clicks realizados por el contacto en el enlace
        /// </summary>
        public int? IdOperadorComparacionNroClicksEnlace { get; set; }
        /// <summary>
        /// Indica el número de click realizados por el contacto en el enlace
        /// </summary>
        public int? NroClicksEnlace { get; set; }
        /// <summary>
        /// Indica si se considerará el filtro general (Area, SubArea,PGeneral y PEspecifico)
        /// </summary>
        public bool ConsiderarFiltroGeneral { get; set; }
        /// <summary>
        /// Indica si el filtro considerará el filtro especifico (tabs)
        /// </summary>
        public bool ConsiderarFiltroEspecifico { get; set; }
        /// <summary>
        /// Indica si el filtro considerará venta cruzada
        /// </summary>
        public bool TieneVentaCruzada { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número total de lineas de credito vigentes
        /// </summary>
        public int? IdOperadorComparacionNroTotalLineaCreditoVigente { get; set; }
        /// <summary>
        /// Indica el número total de linea de credito vigentes
        /// </summary>
        public int? NroTotalLineaCreditoVigente { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al monto total de linea de credito vigente
        /// </summary>
        public int? IdOperadorComparacionMontoTotalLineaCreditoVigente { get; set; }
        /// <summary>
        /// Indica el monto total de linea de credito vigente
        /// </summary>
        public int? MontoTotalLineaCreditoVigente { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al monto maximo otorgado en la linea de credito vigente
        /// </summary>
        public int? IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente { get; set; }
        /// <summary>
        /// Indica el monto maximo otorgado en la linea de credito vigente
        /// </summary>
        public int? MontoMaximoOtorgadoLineaCreditoVigente { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al monto minimo otorgado en la linea de credito vigente
        /// </summary>
        public int? IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente { get; set; }
        /// <summary>
        /// Indica el monto minimo otorgado en la linea de credito vigente
        /// </summary>
        public int? MontoMinimoOtorgadoLineaCreditoVigente { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número total de lineas de credito vigentes vencidas
        /// </summary>
        public int? IdOperadorComparacionNroTotalLineaCreditoVigenteVencida { get; set; }
        /// <summary>
        /// Indica el número total de lineas de credito vigentes vencidas
        /// </summary>
        public int? NroTotalLineaCreditoVigenteVencida { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al monto total de linea de credito vigente vencida
        /// </summary>
        public int? IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida { get; set; }
        /// <summary>
        /// Indica el monto total de linea de credito vigente vencida
        /// </summary>
        public int? MontoTotalLineaCreditoVigenteVencida { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de tarjetas de credito otorgadas
        /// </summary>
        public int? IdOperadorComparacionNroTcOtorgada { get; set; }
        /// <summary>
        /// Indica el número de tarjetas de credito otorgadas
        /// </summary>
        public int? NroTcOtorgada { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al monto total otorgado en tarjetas de credito
        /// </summary>
        public int? IdOperadorComparacionMontoTotalOtorgadoEnTcs { get; set; }
        /// <summary>
        /// Indica el monto total otorgado en tarjetas de credito
        /// </summary>
        public int? MontoTotalOtorgadoEnTcs { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al monto maximo otorgado en una tarjeta de credito
        /// </summary>
        public int? IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc { get; set; }
        /// <summary>
        /// Indica el monto maximo otorgado en una tarjeta de credito
        /// </summary>
        public int? MontoMaximoOtorgadoEnUnaTc { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al monto minimo otorgado en una tarjeta de credito
        /// </summary>
        public int? IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc { get; set; }
        /// <summary>
        /// Indica el monto minimo otorgado en una tarjeta de credito
        /// </summary>
        public int? MontoMinimoOtorgadoEnUnaTc { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al monto disponible total en las tarjetas de credito
        /// </summary>
        public int? IdOperadorComparacionMontoDisponibleTotalEnTcs { get; set; }
        /// <summary>
        /// Indica el monto disponible total en una tarjeta de credito
        /// </summary>
        public int? MontoDisponibleTotalEnTcs { get; set; }
        /// <summary>
        /// Indica la fecha de inicio a considerar para el filtro de interaccion por llamada
        /// </summary>
        public DateTime? FechaInicioLlamada { get; set; }
        /// <summary>
        /// Indica la fecha de fin a considerar para el filtro de interaccion por llamada
        /// </summary>
        public DateTime? FechaFinLlamada { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion a la duracion promedio de llamada por minutos y oportunidad
        /// </summary>
        public int? IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad { get; set; }
        /// <summary>
        /// Indica la duracion promedio de llamada por minuto y oportunidad
        /// </summary>
        public int? DuracionPromedioLlamadaPorOportunidad { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion a la duracion total de llamadas en minutos por oportunidad 
        /// </summary>
        public int? IdOperadorComparacionDuracionTotalLlamadaPorOportunidad { get; set; }
        /// <summary>
        /// Indica la duración total de llamadas en minutos por oportunidad
        /// </summary>
        public int? DuracionTotalLlamadaPorOportunidad { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion al número de llamadas
        /// </summary>
        public int? IdOperadorComparacionNroLlamada { get; set; }
        /// <summary>
        /// Indica el número de llamadas
        /// </summary>
        public int? NroLlamada { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion a la duración de llamadas en minutos
        /// </summary>
        public int? IdOperadorComparacionDuracionLlamada { get; set; }
        /// <summary>
        /// Indica la duración de llamadas en minutos
        /// </summary>
        public int? DuracionLlamada { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relacion a la tasa de ejecución de llamadas
        /// </summary>
        public int? IdOperadorComparacionTasaEjecucionLlamada { get; set; }
        /// <summary>
        /// Indica el porcentaje de ejecución de llamadas
        /// </summary>
        public int? TasaEjecucionLlamada { get; set; }
        /// <summary>
        /// Indica la fecha de inicio a considerar para el filtro de interacción de sitio web
        /// </summary>
        public DateTime? FechaInicioInteraccionSitioWeb { get; set; }
        /// <summary>
        /// Indica la fecha de fin a considerar para el filtro de interacción de sitio web
        /// </summary>
        public DateTime? FechaFinInteraccionSitioWeb { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al tiempo de visualización total del sitio web
        /// </summary>
        public int? IdOperadorComparacionTiempoVisualizacionTotalSitioWeb { get; set; }
        /// <summary>
        /// Indica el tiempo de visualización total del sitio web
        /// </summary>
        public int? TiempoVisualizacionTotalSitioWeb { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al nro clicks en enlaces del todo sitio web
        /// </summary>
        public int? IdOperadorComparacionNroClickEnlaceTodoSitioWeb { get; set; }
        /// <summary>
        /// Indica el número de clicks en enlaces del todo sitio web
        /// </summary>
        public int? NroClickEnlaceTodoSitioWeb { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion,en relación al tiempo de visualización total en la pagina web de programas
        /// </summary>
        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma { get; set; }
        /// <summary>
        /// Indica el tiempo de visualización total de pagina en programas
        /// </summary>
        public int? TiempoVisualizacionTotalPaginaPrograma { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al tiempo de visualización maxima en una pagina web de la pagina programas
        /// </summary>
        public int? IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas { get; set; }
        /// <summary>
        /// Indica el tiempo de visualización maxima en una pagina web de la pagina programas
        /// </summary>
        public int? TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al número de clicks en los enlaces de la pagina programas
        /// </summary>
        public int? IdOperadorComparacionNroClickEnlacePaginaPrograma { get; set; }
        /// <summary>
        /// Indica el número de clicks en enlaces de la pagina programas
        /// </summary>
        public int? NroClickEnlacePaginaPrograma { get; set; }
        /// <summary>
        /// Indica si se considerará que exista visualización del video de vista previa en la pagina programas
        /// </summary>
        public bool? ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma { get; set; }
        /// <summary>
        /// Indica si se considerará que haya realizado click en el boton matricularme de la pagina programas
        /// </summary>
        public bool? ConsiderarClickBotonMatricularmePaginaPrograma { get; set; }
        /// <summary>
        /// Indica si se considerará que haya hecho click en el boton version prueba de la pagina programas
        /// </summary>
        public bool? ConsiderarClickBotonVersionPruebaPaginaPrograma { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al tiempo de visualización total en la pagina bs campus
        /// </summary>
        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus { get; set; }
        /// <summary>
        /// Indica el tiempo de visualización total en la pagina bs campus
        /// </summary>
        public int? TiempoVisualizacionTotalPaginaBscampus { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al tiempo de visualización máxima en una pagina web de la pagina bs campus
        /// </summary>
        public int? IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus { get; set; }
        /// <summary>
        /// Indica el tiempo de visualizacion máxima en una pagina web de la pagina bs campus
        /// </summary>
        public int? TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al número de visitas al directorio, tab, area y subarea
        /// </summary>
        public int? IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea { get; set; }
        /// <summary>
        /// Indica el número de visitas al directorio, tab, area y subarea
        /// </summary>
        public int? NroVisitasDirectorioTagAreaSubArea { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al tiempo de visualización total del directorio, tab, area y subarea
        /// </summary>
        public int? IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea { get; set; }
        /// <summary>
        /// Indica el tiempo de visualización total en el directorio, tab, area y subarea
        /// </summary>
        public int? TiempoVisualizacionTotalDirectorioTagAreaSubArea { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al número de click en enlaces del directorio, tag, area y subarea
        /// </summary>
        public int? IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea { get; set; }
        /// <summary>
        /// Indica el número de clicks en enlaces del directorio, tag, area y subarea
        /// </summary>
        public int? NroClickEnlaceDirectorioTagAreaSubArea { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al número de visitas a la pagina mis cursos
        /// </summary>
        public int? IdOperadorComparacionNroVisitasPaginaMisCursos { get; set; }
        /// <summary>
        /// Indica en número de visitas a la pagina mis cursos
        /// </summary>
        public int? NroVisitasPaginaMisCursos { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al tiempo de visualización total en la pagina mis cursos
        /// </summary>
        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos { get; set; }
        /// <summary>
        /// Indica el tiempo de visualización total en la pagina mis cursos
        /// </summary>
        public int? TiempoVisualizacionTotalPaginaMisCursos { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al número de clicks en enlaces de la pagina mis cursos
        /// </summary>
        public int? IdOperadorComparacionNroClickEnlacePaginaMisCursos { get; set; }
        /// <summary>
        /// Indica en número de clicks en enlaces de la pagina mis cursos
        /// </summary>
        public int? NroClickEnlacePaginaMisCursos { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al número de visitas a la pagina curso diplomado
        /// </summary>
        public int? IdOperadorComparacionNroVisitaPaginaCursoDiplomado { get; set; }
        /// <summary>
        /// Indica el número de visitas a la pagina curso diplomado
        /// </summary>
        public int? NroVisitaPaginaCursoDiplomado { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al tiempo de visualización total en la pagina curso diplomado
        /// </summary>
        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado { get; set; }
        /// <summary>
        /// Indica el tiempo de visualización total en la pagina curso diplomado
        /// </summary>
        public int? TiempoVisualizacionTotalPaginaCursoDiplomado { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_OperadorComparacion, en relación al número de clicks en enlaces  en la pagina curso diplomado
        /// </summary>
        public int? IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado { get; set; }
        /// <summary>
        /// Indica en número de clicks en enlaces de la pagina curso diplomado
        /// </summary>
        public int? NroClicksEnlacePaginaCursoDiplomado { get; set; }
        /// <summary>
        /// Indica si se considerará que exista click en el filtro de la pagina curso diplomado
        /// </summary>
        public bool? ConsiderarClickFiltroPaginaCursoDiplomado { get; set; }
        /// <summary>
        /// Indica si se considerara el tab de oportunidad historica para la ejecución de filtros
        /// </summary>
        public bool? ConsiderarOportunidadHistorica { get; set; }
        /// <summary>
        /// Indica si se considerara el tab de categoria dato para la ejecución de filtros
        /// </summary>
        public bool? ConsiderarCategoriaDato { get; set; }
        /// <summary>
        /// Indica si se considerara el tab de interacción offline-online para la ejecución de filtros
        /// </summary>
        public bool? ConsiderarInteraccionOfflineOnline { get; set; }
        /// <summary>
        /// Indica si se considerara el tab de interacción sitio web para la ejecución de filtros
        /// </summary>
        public bool? ConsiderarInteraccionSitioWeb { get; set; }
        /// <summary>
        /// Indica si se considerara el tab de interacción formularios para la ejecución de filtros
        /// </summary>
        public bool? ConsiderarInteraccionFormularios { get; set; }
        /// <summary>
        /// Indica si se considerara el tab de interacción en el chat del portal web para la ejecución de filtros
        /// </summary>
        public bool? ConsiderarInteraccionChatPw { get; set; }
        /// <summary>
        /// Indica si se considerara el tab de interacción correo para la ejecución de filtros
        /// </summary>
        public bool? ConsiderarInteraccionCorreo { get; set; }
        /// <summary>
        /// Indica si se considerara el tab de historial financiero para la ejecución de filtros
        /// </summary>
        public bool? ConsiderarHistorialFinanciero { get; set; }
        /// <summary>
        /// Indica si se considerara el tab de interacción whatsapp para la ejecución de filtros
        /// </summary>
        public bool? ConsiderarInteraccionWhatsApp { get; set; }
        /// <summary>
        /// Indica si se considerara el el tab de interacción chat messenger para la ejecución de filtros
        /// </summary>
        public bool? ConsiderarInteraccionChatMessenger { get; set; }
        /// <summary>
        /// Indica si se excluira por correo enviado al mismo programa general principal
        /// </summary>
        public bool? ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal { get; set; }
        /// <summary>
        /// Fecha inicio para excluir por correo enviado al mismo programa general principal
        /// </summary>
        public DateTime? FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal { get; set; }
        /// <summary>
        /// Fecha final para excluir por correo enviado al mismo programa general principal
        /// </summary>
        public DateTime? FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_TiempoFrecuencia
        /// </summary>
        public int? IdTiempoFrecuenciaMatriculaAlumno { get; set; }
        /// <summary>
        /// Indica la cantidad de tiempo de matricula del alumno
        /// </summary>
        public int? CantidadTiempoMatriculaAlumno { get; set; }
        /// <summary>
        /// Indica si se considerara solo alumnos con id messenger valido
        /// </summary>
        public bool? ConsiderarConMessengerValido { get; set; }
        /// <summary>
        /// Indica si se considerara solo alumnos con nro de WhatsApp valido
        /// </summary>
        public bool? ConsiderarConWhatsAppValido { get; set; }
        /// <summary>
        /// Indica si se considerara solo alumnos con Email valido
        /// </summary>
        public bool? ConsiderarConEmailValido { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_TiempoFrecuencia
        /// </summary>
        public int? IdTiempoFrecuenciaCumpleaniosContactoDentroDe { get; set; }
        /// <summary>
        /// Indica la cantidad de tiempo a considerar para calcular la fecha de cumpleaños
        /// </summary>
        public int? CantidadTiempoCumpleaniosContactoDentroDe { get; set; }
        /// <summary>
        /// Almacena la fecha de matricula del alumno desde la que se contara para los filtros
        /// </summary>
        public DateTime? FechaInicioMatriculaAlumno { get; set; }
        /// <summary>
        /// Almacena la fecha de matricula del alumno hasta la que se contara para los filtros
        /// </summary>
        public DateTime? FechaFinMatriculaAlumno { get; set; }
        /// <summary>
        /// Indica si se deben considerar los alumnos del asesor &quot;asignacion automatica operaciones&quot;
        /// </summary>
        public bool? ConsiderarAlumnosAsignacionAutomaticaOperaciones { get; set; }
        /// <summary>
        /// Indica la medida de tiempo a usar para la creacion de la oportunidad
        /// </summary>
        public int? IdOperadorMedidaTiempoCreacionOportunidad { get; set; }
        /// <summary>
        /// Indica la cantidad de tiempo a usar para la creacion de la oportunidad
        /// </summary>
        public int? NroMedidaTiempoCreacionOportunidad { get; set; }
        /// <summary>
        /// Indica la medida de tiempo a usar para la ultima actividad
        /// </summary>
        public int? IdOperadorMedidaTiempoUltimaActividadEjecutada { get; set; }
        /// <summary>
        /// Indica la cantidad de tiempo a usar para la ultima actividad
        /// </summary>
        public int? NroMedidaTiempoUltimaActividadEjecutada { get; set; }
        /// <summary>
        /// Indica el estado de la actividad detalle
        /// </summary>
        public int? EnvioAutomaticoEstadoActividadDetalle { get; set; }
        /// <summary>
        /// Indica si se consideraran a los ya enviados
        /// </summary>
        public int? ConsiderarYaEnviados { get; set; }
        /// <summary>
        /// Indica si se considerara el Tab de envio automatico
        /// </summary>
        public bool? ConsiderarEnvioAutomatico { get; set; }
        /// <summary>
        /// Indica si se aplica la logica de la creacion de la oportunidad
        /// </summary>
        public bool? AplicaSobreCreacionOportunidad { get; set; }
        /// <summary>
        /// Indica si se aplica sobre la ultima actividad
        /// </summary>
        public bool? AplicaSobreUltimaActividad { get; set; }
        /// <summary>
        /// Valida si se excluye alumnos matriculados de los prospectos
        /// </summary>
        public bool? ExcluirMatriculados { get; set; }
        /// <summary>
        /// Indica si el filtro considerará la última oportunidad de los alumnos.
        /// </summary>
        public bool? ConsiderarUltimaOportunidad { get; set; }
        

        public virtual ICollection<TCampaniaGeneral> TCampaniaGenerals { get; set; }
        public virtual ICollection<TFiltroSegmentoDetalle> TFiltroSegmentoDetalles { get; set; }
        public virtual ICollection<TFiltroSegmentoValorTipo> TFiltroSegmentoValorTipos { get; set; }
    }
}
