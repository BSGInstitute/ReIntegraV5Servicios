using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppMensajeEnviadoDTO
    {
        public int Id { get; set; }
        public string? WaTo { get; set; }
        public string? WaId { get; set; }
        public string? WaType { get; set; }
        public int? WaTypeMensaje { get; set; }
        public string? WaRecipientType { get; set; }
        public string? WaBody { get; set; }
        public string? WaFile { get; set; }
        public string? WaFileName { get; set; }
        public string? WaMimeType { get; set; }
        public string? WaSha256 { get; set; }
        public string? WaLink { get; set; }
        public string? WaCaption { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class WhatsAppMensajeEnviadoComboDTO
    {
        public int Id { get; set; }
        public string? WaBody { get; set; }
    }
    public class ComboTamanioEmpresa
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
    }
    public class HistorialAlumnoDTO
    {
        public int? IdOportunidad { get; set; }
        public int? IdAlumno { get; set; }
        public string? CelularWhatsApp { get; set; }
        public int? IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public int? IdCentroCosto { get; set; }
        public string? Tipo { get; set; }
        public string? Categoria { get; set; }
        public string? FechaCreacion { get; set; }
        public string? ChatValido { get; set; }
        public string? ChatInValido { get; set; }
        public string? ChatOportunidad { get; set; }

    }
    public class ComboAtributoAlumnoDTO
    {
        public List<ComboDTO> ComboIndustria { get; set; }
        public List<ComboDTO> ComboAreaFormacion { get; set; }
        public List<ComboDTO> ComboAreaTrabajo { get; set; }
        public List<ComboDTO> ComboCargo { get; set; }
        public List<TamanioEmpresaComboDTO> ComboTamanioEmpresa { get; set; }
    }
    public class ObtenerAtributosAlumnoDTO
    {
        public int? Id { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? Celular { get; set; }
        public string? Celular2 { get; set; }
        public string? Telefono { get; set; }
        public string? Dni { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdTamanioEmpresaAgenda { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdCargo { get; set; }
        public bool? Desuscrito { get; set; }
        public bool? Archivado { get; set; }
        //public string? UsuarioModificacion { get; set; }
        //public DateTime? FechaModificacion { get; set; }



    }
    public class AtributosAlumnoDTO
    {
        public ObtenerAtributosAlumnoDTO ObtenerAtributosAlumno { get; set; }
        public List<HistorialAlumnoDTO> HistorialAlumno { get; set; }

    }
    public class ObtenerAtributosAlumnoOriginalDTO
    {
        public int? Id { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? Celular { get; set; }
        public string? Celular2 { get; set; }
        public string? Telefono { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdCargo { get; set; }
        public string? Dni { get; set; }
        public int? IdTamanioEmpresaAgenda { get; set; }
        //public string? UsuarioModificacion { get; set; }
        //public DateTime? FechaModificacion { get; set; }
    }

    public class WhatsAppMensajesDTO
    {
        public string Numero { get; set; }
        public string Mensaje { get; set; }
        public int? IdPersonal { get; set; }
        public int IdPais { get; set; }
        public int IdAlumno { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombreAlumno { get; set; }
    }
    public class WhatsAppHistorialMensajesDTO
    {
        public string Numero { get; set; }
        public int Tipo { get; set; }
        public int SubTipo { get; set; }
        public int IdPais { get; set; }
        public string Mensaje { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public int? Registro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombrePersonal { get; set; }
        public int? EstadoMensaje { get; set; }
    }
    public class WhatsAppHistorialMensajesOperacionesDTO
    {
        public string Numero { get; set; }
        public int Tipo { get; set; }
        public int IdPais { get; set; }
        public string Mensaje { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public int? Registro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombrePersonal { get; set; }
        public string? AreaPersonal { get; set; }
        public int? EstadoMensaje { get; set; }
    }
    public class PersonalNumeroMinimoChatDTO
    {
        public int IdPersonal { get; set; }
        public int NumeroChats { get; set; }
    }
    public class MensajeChatDTO
    {
        public int Id { get; set; }
        public DateTime FechaMensaje { get; set; }
    }
    public class DatosPlantillaWhatsAppDTO
    {
        public string codigo { get; set; }
        public string texto { get; set; }
    }
    public class ValidarOportunidadWhatsAppDTO
    {
        public int IdPersonal { get; set; }
        public int IdPgeneral { get; set; }
        public string FaseOportunidad { get; set; }
    }
    public class WhatsAppMensajeEnviadoRespuestaDTO
    {
        public string Mensaje { get; set; }
        public bool EstadoMensaje { get; set; }
    }
    public class WhatsAppEnviarMensajeDTO
    {
        public int Id { get; set; }
        public string WaTo { get; set; }
        public string? WaId { get; set; }
        public string WaType { get; set; }
        public int? WaTypeMensaje { get; set; }
        public string WaRecipientType { get; set; }
        public string? WaBody { get; set; }
        public string? WaFile { get; set; }
        public string? WaFileName { get; set; }
        public string? WaMimeType { get; set; }
        public string? WaSha256 { get; set; }
        public string? WaLink { get; set; }
        public string? WaCaption { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
        public int IdPersonal { get; set; }
        public int IdAlumno { get; set; }
        public string? usuario { get; set; }
        public List<DatosPlantillaWhatsAppDTO>? DatosPlantillaWhatsApp { get; set; }
    }
    public class WhatsAppEnviarAccesosAlumnoDTO
    {
        public int Id { get; set; }
        public string WaTo { get; set; }
        public string? WaId { get; set; }
        public string WaType { get; set; }
        public int? WaTypeMensaje { get; set; }
        public string WaRecipientType { get; set; }
        public string? WaBody { get; set; }
        public string? WaFile { get; set; }
        public string? WaFileName { get; set; }
        public string? WaMimeType { get; set; }
        public string? WaSha256 { get; set; }
        public string? WaLink { get; set; }
        public string? WaCaption { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
        public int IdPersonal { get; set; }
        public int IdAlumno { get; set; }
        public string? usuario { get; set; }
        public List<DatoPlantillaWhatsAppDTO>? DatosPlantillaWhatsApp { get; set; }
    }
    public class PlantillaWhatsAppEnvioAccesoDTO
    {
        public string Plantilla { get; set; } = "";
        public List<DatoPlantillaWhatsAppDTO> ListaEtiquetas { get; set; } = new List<DatoPlantillaWhatsAppDTO>();
        public DatosAlumnoOportunidadDTO DatoAlumno { get; set; } = new DatosAlumnoOportunidadDTO();
    }

    public class MktWhatsAppEnviarMensajeDTO
    {
        public int Id { get; set; }
        public string WaTo { get; set; }
        public string WaId { get; set; } = null;
        public string WaType { get; set; }
        public int WaTypeMensaje { get; set; }
        public string WaRecipientType { get; set; }
        public string WaBody { get; set; }
        public string WaFile { get; set; }
        public string WaFileName { get; set; }
        public string WaMimeType { get; set; } = null;
        public string WaSha256 { get; set; } = null;
        public string WaLink { get; set; } = null;
        public string WaCaption { get; set; } = null;
        public int IdPais { get; set; }
        public bool EsMigracion { get; set; }
        public int IdMigracion { get; set; }
        public int IdPersonal { get; set; }
        public int IdAlumno { get; set; }
        public string usuario { get; set; }
        public List<DatosPlantillaWhatsAppDTO> DatosPlantillaWhatsApp { get; set; } = null;
        public List<BotonDTO> botones { get; set; } = null;
        public string imagen { get; set; } = null;
    }


    public class WhatsAppEnviarMensajeMarketingDTO
    {
        public int Id { get; set; }
        public string WaTo { get; set; }
        public string? WaId { get; set; }
        public string WaType { get; set; }
        public int? WaTypeMensaje { get; set; }
        public string WaRecipientType { get; set; }
        public string? WaBody { get; set; }
        public string? WaFile { get; set; }
        public string? WaFileName { get; set; }
        public string? WaMimeType { get; set; }
        public string? WaSha256 { get; set; }
        public string? WaLink { get; set; }
        public string? WaCaption { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
        public int IdPersonal { get; set; }
        public int IdAlumno { get; set; }
        public string usuario { get; set; }
        public List<DatosPlantillaWhatsAppDTO>? DatosPlantillaWhatsApp { get; set; }
        public List<BotonDTO>? botones { get; set; } = null;
        public string? imagen { get; set; } = null;
    }

    public class BotonDTO
    {
        public string Nombre { get; set; }
    }
    public class FiltroChatWhatsappDTO
    {
        public int Tab { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class ChatWhatsAppMarketingDTO
    {
        public string? Celular { get; set; }
        public string? Mensaje { get; set; }
        public string? Alumno { get; set; }
        public int? IdAlumno { get; set; }
        public string? Asesor { get; set; }
        public int? IdPersonal { get; set; }
        public int? Tipo { get; set; }
        public int? Categoria { get; set; }
        public string? Proceso { get; set; }
        public int? Tab { get; set; }
        public string? Tiempo { get; set; }
        public string? Fecha { get; set; }
        public string? Pais { get; set; }
        public string? NumeroWhatsApp { get; set; }
        public string? NumeroIdentificador { get; set; }
        public int? IdPersonalVentanas { get; set; }
        public int? IdOportunidad { get; set; }
        public string? Rango { get; set; }
        public string? EstadoInteraccion { get; set; }
        public bool? RequiereDerivacion { get; set; }
        public string? MensajeParaAsesor { get; set; }
        public string? TipoMensajeDetectado { get; set; }
    }
    public class ObtenerChatWhatsAppMarketingPorCelularDTO
    {
        public int? IdAlumnoUM { get; set; }
        public string? CelularUM { get; set; }
        public string? CelularUMEncriptado { get; set; }
        public int? IdPaisEmpresa { get; set; }
        public string? EmailUM { get; set; }
        public string? EmailUMEncriptado { get; set; }
        public int? IdAlumno { get; set; }
        public string? Email { get; set; }
        public string? FechaCreacion { get; set; }
        public int? Estatus { get; set; }
        public int? Tipo { get; set; }
        public int? IdAlumnoCelular { get; set; }
        public string? Celular { get; set; }
        public string? Alumno { get; set; }
        public string? Mensaje { get; set; }
        public string? Personal { get; set; }
        public string? FechaMensaje { get; set; }

    }
    public class ChatWhatsAppMarketingPorCelularDTO
    {
        public int? IdAlumnoUM { get; set; }
        public string? CelularUM { get; set; }
        public string? CelularUMEncriptado { get; set; }
        public string? EmailUMEncriptado { get; set; }
        public string? EmailUM { get; set; }
        public int? IdPaisEmpresa { get; set; }
        public List<ObtenerChatWhatsAppMarketingAlumnoDTO>? ListaAlumnosPorCelular { get; set; }
        public List<ObtenerChatWhatsAppMarketingMensajeDTO>? MensajePorCelular { get; set; }
        public string? Rango { get; set; }

    }
    public class ObtenerChatWhatsAppMarketingAlumnoDTO
    {
        public int? IdAlumno { get; set; }
        public string? Email { get; set; }
        public string? FechaCreacion { get; set; }

    }
    public class ObtenerChatWhatsAppMarketingMensajeDTO
    {
        public int? Estatus { get; set; }
        public int? Tipo { get; set; }
        public int? IdAlumnoCelular { get; set; }
        public string? Celular { get; set; }
        public string? Alumno { get; set; }
        public string? Mensaje { get; set; }
        public string? Personal { get; set; }
        public string? FechaMensaje { get; set; }

    }


    public partial class T_WhatsAppMensajeRecibido
    {
        public int Id { get; set; }
        public string WaFrom { get; set; }
        public string WaId { get; set; }
        public string? WaTimeStamp { get; set; }
        public string? WaType { get; set; }
        public Nullable<int> WaTypeMensaje { get; set; }
        public string? WaIdTypeMensaje { get; set; }
        public string? WaBody { get; set; }
        public string? WaFile { get; set; }
        public string? WaFileName { get; set; }
        public string? WaMimeType { get; set; }
        public string? WaSha256 { get; set; }
        public string? WaCaption { get; set; }
        public int? IdPais { get; set; }
        public int? IdPersonal { get; set; }
        public Nullable<int> IdAlumno { get; set; }
        public Nullable<bool> EsMigracion { get; set; }


        public Nullable<bool> MensajeOfensivo { get; set; }
    }

    public partial class MarcadorAsesorDTO
    {
        public int IdPersonal { get; set; }
        public bool MarcadorActivo { get; set; }

    }
    public partial class WhatsappEnvioDTO
    {
        public string Asesor { get; set; }
        public T_WhatsAppMensajeRecibido obj { get; set; }

    }
        public class ReemplazoPlantillaDTO
        {
            public List<DatosPlantillaWhatsAppDTO> datos { get; set; }
            public string textoPlantilla { get; set; }
        }
        public class AsesorSignalDTO
        {
            public string Id { get; set; }
            public string Nombre { get; set; }
            public int IdIntegra { get; set; }
            public bool EnLinea { get; set; }
            public int IdProgramaGeneral { get; set; }
            public int CodigoPais { get; set; }
            public string Correo { get; set; }
        }
        public partial class InfoApiWhatsappDTO
        {
            public string Numero { get; set; }
            public string VName { get; set; }
            public int IdPais { get; set; }
            public string Bearer { get; set; }
            public string NumeroIndentificador { get; set; }
            public string VersionApi { get; set; }
            public DateTime FechaExpiracion { get; set; }
        }
        #region Mensaje de Texto
        // Mensaje de Texto
        public class MensajeTextoEnvio
        {
            public string to { get; set; }
            public string type { get; set; }
            public string recipient_type { get; set; }
            public text text { get; set; }
        }

    public partial class text
    {
        public string body { get; set; }
    }
    #endregion
    #region Mensaje con plantillas de WhatsApp
    // Mensaje con plantillas de WhatsApp
    public class MensajePlantillaWhatsAppEnvio
    {
        public string to { get; set; }
        public string type { get; set; }
        public hsm hsm { get; set; }
        public template template { get; set; }
    }

    public partial class hsm
    {
        public string @namespace { get; set; }
        public string element_name { get; set; }
        public language language { get; set; }
        public List<localizable_params> localizable_params { get; set; }
    }

    public partial class language
    {
        public string policy { get; set; }
        public string code { get; set; }
    }

    public partial class localizable_params
    {
        public string @default { get; set; }
    }

    public class MensajePlantillaWhatsAppEnvioTemplate
    {
        public string to { get; set; }
        public string type { get; set; }
        public template template { get; set; }
    }

    public partial class template
    {
        public string @namespace { get; set; }
        public string name { get; set; }
        public language language { get; set; }
        public List<components> components { get; set; }
    }

    public partial class components
    {
        public string type { get; set; }
        public List<parameters> parameters { get; set; }
    }
    public partial class parameters
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    #endregion
    #region Mensaje con Imagen
    //Mensaje con imagen
    public class MensajeImagenEnvio
    {
        public string to { get; set; }
        public string type { get; set; }
        public string recipient_type { get; set; }
        public image image { get; set; }
    }

    public partial class image
    {
        public string caption { get; set; }
        public string link { get; set; }
    }
    #endregion
    #region Mensaje con documento
    //Mensaje con documento
    public class MensajeDocumentoEnvio
    {
        public string to { get; set; }
        public string type { get; set; }
        public string recipient_type { get; set; }
        public document document { get; set; }
    }

    public partial class document
    {
        public string caption { get; set; }
        public string link { get; set; }
        public string filename { get; set; }
    }
    #endregion
    #region Respuesta de mensaje enviado
    // Respuesta de mensaje enviado
    public partial class respuestaMensaje
    {
        public messages[] messages { get; set; }
        public meta meta { get; set; }
    }

    public partial class messages
    {
        public string id { get; set; }
    }
    #endregion

    #region Mensaje con boton
    //Mensaje con documento
    public class MensajeBotonEnvio
    {
        public string to { get; set; }
        public string type { get; set; }
        public string recipient_type { get; set; }
        public Button button { get; set; }
    }

    public partial class Button
    {
        public string payload { get; set; }
        public string text { get; set; }
    }


    #endregion

    public class EnvioWhatsappAsignacionDTO
    {

        public int idOportunidad { get; set; }
        public int idPais { get; set; }
        public int idPersonal { get; set; }
        public int IdCategoriaOrigen { get; set; }
    }

    public class MensajeEnviadoErroneoWhatsAppDTO
    {
        public int Id { get; set; }
        public string CelularWhatsapp { get; set; }
        public int IdCampaniaGeneralDetalleResponsableWhatsapp { get; set; }
        public int IdPlantilla { get; set; }
        public string MensajePlantillaHtml { get; set; }
        public string ObjetoPlantilla { get; set; }
        public int idPais { get; set; }
        public string NumeroEnviado { get; set; }
        public string MensajeErroneo { get; set; }
        public string WaId { get; set; }
        public DateTime FechaCreacion { get; set; }


    }

    public class ProbabilidaWhatsAppDTO
    {
        public int Id { get; set; }
        public int IdProbabilidadRegistroPW { get; set; }
        public int IdProgramaGeneralPuntoCorte { get; set; }
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public int IdClasificacionPersona { get; set; }
        public int IdCentroCosto { get; set; }
        public string CentroCostoNombre { get; set; }
        public int IdEspecifico { get; set; }
        public string EspecificoNOmbre { get; set; }
        public int IdPGeneral { get; set; }
        public string ProgramaNombre { get; set; }
    }
    public class ProgramaPorOportunidadDTO
    {
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public int IdArea { get; set; }
        public int IdClasificacionPersona { get; set; }
        public int IdCentroCosto { get; set; }
        public string CentroCostoNombre { get; set; }
        public int IdEspecifico { get; set; }
        public string EspecificoNOmbre { get; set; }
        public int IdPGeneral { get; set; }
        public string ProgramaNombre { get; set; }
    }
    public class VentaCruzadaProbabilidadDTO
    {
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public int IdArea { get; set; }
        public int IdPGeneral { get; set; }


    }
    public class ProbabilidadResultadoDTO
    {
        public string Probabilidad { get; set; }
        public bool Apto { get; set; }
        public string Mensaje { get; set; }
        public string ProgramaNombre { get; set; }
        public List<OportunidadVentaCruzadaWhatsappDTO> ListaVentaCruzadaWhatsapp { get; set; }

    }
    public class OportunidadVentaCruzadaWhatsappDTO
    {
        public int IdOportunidad { get; set; }
        public int IdPGeneral { get; set; }
        public int IdCentroCosto { get; set; }
        public string? Programa { get; set; }
        public string? Probabilidad { get; set; }
        public string? ProbabilidadTexto { get; set; }
        public int? Tipo { get; set; }


    }
    public class OportunidadInformacionWhatsappDTO
    {
        public int IdAlumno { get; set; }
        public int IdArea { get; set; }
        public int IdPGeneral { get; set; }
        public List<OportunidadVentaCruzadaWhatsappDTO> ListaVentaCruzada { get; set; } = new List<OportunidadVentaCruzadaWhatsappDTO>();
        public List<OportunidadHistorialAgendaDTO> ListaHistorial { get; set; } = new List<OportunidadHistorialAgendaDTO>();
        public ProgramaGeneralPreBenCompuestoDTO ProgramaGeneralPreBen { get; set; } = new ProgramaGeneralPreBenCompuestoDTO();
        public List<ActividadOportunidadDTO> ActividadesOportunidad { get; set; } = new List<ActividadOportunidadDTO>();
        public List<OportunidadVentaCruzadaWhatsappDTO> ListaVentaCruzadaWhatsapp { get; set; }

    }
    public class AsesorActualDTO
    {
        public int IdOportunidad { get; set; }
        public int IdAsesor { get; set; }
    }
    public class ModeloPredictivoDTO
    {
        public int IdAlumno { get; set; }
        public int IdPGeneral { get; set; }
        public float Probabilidad { get; set; }
        public int Tipo { get; set; }
        public int IdModeloPredictivoTipo { get; set; }
    }


    public class DatoPredictivoDTO
    {
        public int IdAlumno { get; set; }
        public int IdPGeneral { get; set; }

    }


    //Modelos para CapturarRegistrosModeloIA
    public class DatosExtraccionRegistrosDTO
    {
        public int Rango { get; set; }
        public string CelularAlumno { get; set; }
    }

    public class DatosExtraccionRegistrosRequestDTO
    {
        public string Id_cliente { get; set; }
        public string Timestamp { get; set; }
        public List<MensajeExtraccionRegistroDTO> Mensajes { get; set; }
        public List<string> Campos { get; set; }
        public string Info_curso { get; set; }
    }
    public class MensajeExtraccionRegistroDTO
    {
        public string Id { get; set; }
        public string Contenido { get; set; }
        public string Remitente { get; set; }
        public string Timestamp { get; set; }
    }

    public class DatosExtraccionRegistrosResponseDTO
    {
        public string Id_Cliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DatoExtraccion Cargo { get; set; }
        public DatoExtraccion Area_De_Formacion { get; set; }
        public DatoExtraccion Area_De_Trabajo { get; set; }
        public DatoExtraccion Industria { get; set; }
        public string Timestamp { get; set; }
    }
    public class DatoExtraccion
    {
        public string Tipo { get; set; }
        public int Id { get; set; }
        public string Valor { get; set; }
    }

    public class DesactivarInteraccionResponseDTO
    {
        public string status { get; set; }
        public string? descripcion { get; set; }
        public string? advertencia { get; set; }
    }

    public class DatosInteraccionAutomaticaResponseDTO
    {
        public string Status { get; set; }
        public DatosExtraidosInteraccionAutomatica Datos_extraidos { get; set; }
    }
    public class DatosExtraidosInteraccionAutomatica
    {
        public string NumeroWhatsApp { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdAFormacion { get; set; }
        public int IdCargo { get; set; }
        public int IdATrabajo { get; set; }
        public int IdIndustria { get; set; }
    }


}



