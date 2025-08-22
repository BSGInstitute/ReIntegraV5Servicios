
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CampaniaGeneralSmsDTO

    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public TimeSpan? HoraEnvio { get; set; }
        public DateTime? FechaInicioEnvioSms { get; set; }
    }
    public class ObtenerCampaniaGeneralDetalleSmsGrupoDTO
    {
        public int Id { get; set; }
        public string NombreCampaniaGeneralSms { get; set; }
        public string FechaInicioEnvioSms { get; set; }
        public string HoraEnvio { get; set; }
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public string NombreCampaniaOrigen { get; set; }
        public int Prioridad { get; set; }
        public string Nombre { get; set; }
        public bool ActivarMasivo { get; set; }
        public int Programados { get; set; }
        public int CantidadBase { get; set; }
        public int Enviados { get; set; }

    }
    public class ObtenerCampaniaGeneralDetalleSmsDTO
    {
        public int Id { get; set; }
        public string NombreCampaniaGeneralSms { get; set; }
        public string FechaInicioEnvioSms { get; set; }
        public string HoraEnvio { get; set; }
        public List<ObtenerCampaniaGeneralDetallePrioridadSmsDTO> ObtenerCampaniaGeneralDetallePrioridadSms { get; set; }
    }
    public class ObtenerCampaniaGeneralDetallePrioridadSmsDTO
    {
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public string NombreCampaniaOrigen { get; set; }
        public int Prioridad { get; set; }
        public string Nombre { get; set; }
        public bool ActivarMasivo { get; set; }
        public int Programados { get; set; }
        public int CantidadBase { get; set; }
        public int Enviados { get; set; }
    }
    public class ActualizarActivarMasivoPorCampaniaSmsDTO
    {
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public bool ActivarMasivo { get; set; }
        public string? Usuario { get; set; }
    }
    public class ActualizarCamposCampaniaGeneralDetalleSmsDTO
    {
        public string Nombre { get; set; }
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public int Prioridad { get; set; }
        public string Usuario { get; set; }
    }
    public class ProcesarDataPorPrioridadExcelAlumnoSmsDTO
    {
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public string ListaDeAlumnos { get; set; }
        public string Usuario { get; set; }
    }
    public class ProcesarDataPorPrioridadExcelSmsDTO
    {
        public string Nombre { get; set; }
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public int Prioridad { get; set; }
        public string Usuario { get; set; }
    }
    public class EliminarCampaniaGeneralDetalleSmsDTO
    {
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public string Usuario { get; set; }
    }
    public class EliminarCampaniaGeneralDetalleResponsableSmsDTO
    {
        public int IdCampaniaGeneralDetalleResponsableSms { get; set; }
        public string Usuario { get; set; }
    }
    public class ActualizarCampaniaGeneralSmsDTO
    {
        public string Nombre { get; set; }
        public string HoraEnvio { get; set; }
        public string FechaInicioEnvioSms { get; set; }
        public int? Id { get; set; }
        public string? Usuario { get; set; }
    }
    public class ErrorMasivosSms
    {
        public string error { get; set; }
        public string message { get; set; }
    }
    public class ObtenerCampaniaGeneralGrillaSmsDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string FechaInicioEnvioSms { get; set; }

        public string HoraEnvio { get; set; }
        public int Cantidad { get; set; }
    }
    public class EliminarCampaniaGeneralSmsDTO
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
    }
    public class InsertarCampaniaGeneralDetalleSmsDTO
    {
        public string Nombre { get; set; }
        public int IdCampaniaGeneralSms { get; set; }
        public int Prioridad { get; set; }
        public string Usuario { get; set; }
    }
    public class InsertarCampaniaGeneralDetalleResponsableAlumnoSmsDTO
    {
        public string Json { get; set; }
        public int IdCampaniaGeneralDetalleResponsableSms { get; set; }
        public string Usuario { get; set; }
    }
    public class ObtenerConfiguracionCampaniaGeneralDetalleSmsDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public int IdCampaniaGeneral { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
    }
    public class ObtenerCampaniaGeneralDetalleResponsablePorPrioridadSmsDTO
    {
        public int Id { get; set; }
        public int CantidadBase { get; set; }
        public int CantidadDisponible { get; set; }
        public int IdCampaniaGeneralDetalleResponsableSms { get; set; }
        public string Asesor { get; set; }
        public string Plantilla { get; set; }
        public string CentroCosto { get; set; }
        public int Cantidad { get; set; }
        public int Enviados { get; set; }
        public int AlumnoConfigurado { get; set; }
    }
    public class ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoSmsDTO
    {
        public int Id { get; set; }
        public int CantidadBase { get; set; }
        public int CantidadDisponible { get; set; }
        public List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadListaSmsDTO> ObtenerCampaniaGeneralDetalleResponsablePorPrioridadListaSms { get; set; }
    }
    public class ObtenerCampaniaGeneralDetalleResponsablePorPrioridadListaSmsDTO
    {
        public int IdCampaniaGeneralDetalleResponsableSms { get; set; }
        public string Asesor { get; set; }
        public string Plantilla { get; set; }
        public string CentroCosto { get; set; }
        public int Cantidad { get; set; }
        public int Enviados { get; set; }
        public int AlumnoConfigurado { get; set; }
    }
    public class InsertarCampaniaGeneralDetalleResponsableSmsDTO
    {
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public int IdPersonal { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdPlantilla { get; set; }
        public int IdCentroCosto { get; set; }
        public int Cantidad { get; set; }
        public string Usuario { get; set; }
    }
    public class ProcesarDataPorPrioridadSendinblueSmsDTO
    {
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public string Usuario { get; set; }
    }
    public class InsertarCampaniaGeneralDetalleResponsableInserSmsDTO
    {
        public int Id { get; set; }
        public int IdCampaniaGeneralDetalleResponsableSms { get; set; }
        public int IdAlumno { get; set; }
        public int IdPais { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public string CelularSms { get; set; }
        public string MensajePlantillaHtml { get; set; }
        public string ObjetoPlantilla { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get; set; }
        public byte[]? RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
    public class ComboCampaniaGeneralDetalleResponsableSmsDTO
    {
        public List<ComboGeneralDTO>? IdPersonal { get; set; }
        public List<ComboGeneralDTO>? IdPGeneral { get; set; }
        public List<ComboGeneralDTO>? IdPlantilla { get; set; }
        public List<ComboGeneralCentroCostoAreaCapacitacionDTO>? IdCentroCosto { get; set; }
    }
    public class ObtenerComboRespuestaSmsp
    {
        public List<ComboPaisDTO>? IdPaisDTO { get; set; }
        public List<ComboGeneralDTO>? IdPlantilla { get; set; }

        public List<ComboGeneralCentroCostoAreaCapacitacionDTO>? IdCentroCosto { get; set; }
    }
    public class ComboGeneralSmsDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class ComboPaisSmsDTO
    {
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
    }
    public class ComboGeneralCentroCostoAreaCapacitacionSmsDTO
    {
        public int IdCentroCosto { get; set; }
        public string Nombre { get; set; }
    }
    public class ObtenerComboCampaniasSendinBlueSmsDTO
    {
        public List<ComboCampaniaGeneralDTO>? IdCampaniaGeneral { get; set; }
        public List<IdCampaniaGeneralDetalleDTO>? IdCampaniaGeneralDetalle { get; set; }
    }
    public class ComboCampaniaGeneralSmsDTO
    {
        public int IdCampaniaGeneral { get; set; }
        public string Nombre { get; set; }
    }
    public class IdCampaniaGeneralDetalleSmsDTO
    {
        public int IdCampaniaGeneral { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public string Nombre { get; set; }
    }
    public class ObtenerComboCentroCostoCampaniasSendinBlueSmsDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class IdProgramaGeneralPlantillaSmsDTO
    {
        public int IdProgramaGeneral { get; set; }
    }
    public class ObtenerPlantillaSmsDTO
    {
        public string Descripcion { get; set; }
    }
    public class ObtenerDatosPorPrioridadAsignadaSmsDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdPlantilla { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdProgramaGeneral { get; set; }
    }
    public class ObtenerAlumnoConfiguradoPorPrioridadSmsDTO
    {
        public int IdAlumno { get; set; }
        public string CelularSms { get; set; }
        public int IdPais { get; set; }
    }

    public class PrioridadDatosSmsDTO
    {
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public int Cantidad { get; set; }

    }
    public class AlumnoSmsMasivo
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public string Plantilla { get; set; } = "";
        public int IdPais { get; set; }
        public string ObjetoPlantilla { get; set; }
        public List<DatoPlantillaSmsDTO> ListaObjetoPlantilla { get; set; }

    }
    public class DatoPlantillaSmsDTO
    {
        public string Codigo { get; set; } = "";
        public string Texto { get; set; } = "";
    }
    public class AlumnoSmsMasivoJSON
    {
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public string Plantilla { get; set; } = "";
        public int IdPais { get; set; }
        public string ObjetoPlantilla { get; set; }
    }
    public class AlumnoSmsMasivoBaseDTO
    {
        public int IdAlumno { get; set; }
        public string CelularSms { get; set; }
        public int IdPais { get; set; }
    }
    public class ReporteInteraccionCampaniaGeneralDetalleSmsDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public string NombreCampaniaOrigen { get; set; }
        public string NombreDetalle { get; set; }
        public string CentroCosto { get; set; }
        public string personal { get; set; }
        public string Plantilla { get; set; }
        public int Programados { get; set; }
        public int Enviados { get; set; }
        public int Entregados { get; set; }
        public int Leidos { get; set; }
        public int ChatsValidos { get; set; }
        public int ChatsInvalidos { get; set; }
        public int OportunidadesCreadas { get; set; }
    }
    public class SumaValidadorChatSmsDTO
    {
        public int IdAlumno { get; set; }
        public string CelularSms { get; set; }
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public string Usuario { get; set; }
    }
    public class SmsPlantillaDTO
    {
        public int IdPlantilla { get; set; }
        public int IdCentroCosto { get; set; }
        public string CelularSms { get; set; }
        public int IdPais { get; set; }
        public int IdAlumno { get; set; }
        public int IdPersonal { get; set; }
        public string usuario { get; set; }

    }
    public class SmsMensajeTextoDTO
    {
        public string CelularSms { get; set; }
        public string Mensaje { get; set; }
        public int IdPais { get; set; }
        public int IdAlumno { get; set; }
        public int IdPersonal { get; set; }
        public string usuario { get; set; }

    }
    public class respuestaMensajeSmsHook
    {
        public string messageId { get; set; }
        public string statusMessage { get; set; }
        public int statusCode { get; set; }
    }
    public class SumaOportunidadSmsAootDTO
    {
        public int IdAlumno { get; set; }
        public string CelularSms { get; set; }
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public int IdCentroCosto { get; set; }
        public string Usuario { get; set; }
    }
    public class PlantillaSmsDato
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class PlantillaDetalleSmsDato
    {
        public int IdPlantillaSms { get; set; }
        public string Text { get; set; }
        public bool CustomData { get; set; }
        public bool IsPremium { get; set; }
        public bool IsFlash { get; set; }
        public bool IsLongmessage { get; set; }
        public bool IsRandomRoute { get; set; }
        public bool ShortUrlConfig { get; set; }
        public string Url { get; set; }
        public string DomainShortUrl { get; set; }
    }
    public class ObtenerPrioridadesEnvioSmsDTO
    {
        public int IdCampaniaGeneralSms { get; set; }
        public string FechaInicioEnvioSms { get; set; }
        public string HoraEnvio { get; set; }
        public int IdCampaniaGeneralDetalleSms { get; set; }
        public int IdCampaniaGeneralDetalleResponsableSms { get; set; }
        public string Nombre { get; set; }
        public string NombreCampania { get; set; }
        public int Prioridad { get; set; }
        public string Personal { get; set; }
    }
    public class EmailAsesoresDto
    {
        public string Email { get; set; }
    }
    public class SmsConfiguracionLogEjecucionDTO
    {
        public int Id { get; set; }
        public bool Estado { get; set; }
    }
    public class CampaniaGeneralDetalleResponsableAlumnoLogSmsDTO
    {
        public int Id { get; set; }
        public int IdCampaniaGeneralDetalleResponsableSms { get; set; }
        public string FechaEnvio { get; set; }
        public string HoraEnvio { get; set; }
        public bool Estado { get; set; }
    }
    public class IdLogInsertadoDTO
    {
        public int Valor { get; set; }
    }
    public class PreCampaniaGeneralDetalleResponsableAlumnoSmsDTO
    {
        public int Id { get; set; }
        public string CelularSms { get; set; }
        public int IdAlumno { get; set; }
        public int IdPais { get; set; }
        public string MensajePlantillaHtml { get; set; }
        public string ObjetoPlantilla { get; set; }
        public int IdCampaniaGeneralDetalleResponsableSms { get; set; }
        public int IdPersonal { get; set; }
        public string Descripcion { get; set; }
        public string MessageId { get; set; } = null;
    }
    public class DetalleCampaniaDTO
    {
        public string Nombre { get; set; }
        public string Prioridad { get; set; }
        public string Asesor { get; set; }
    }
    public class IdLogDTO
    {
        public int IdLog { get; set; }
    }
    public class SmsEnviarMensajeDTO
    {
        public int IdCampaniaGeneralDetalleResponsableAlumnoSms { get; set; }
        public int idalumno { get; set; }
        public string CelularSms { get; set; }
        public string Nombre { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public string Customdata { get; set; }
        public bool IsPremium { get; set; }
        public bool IsFlash { get; set; }
        public bool isLongmessage { get; set; }
        public bool IsRandomRoute { get; set; }
        public bool ShortUrlConfig { get; set; }
        public string? Url { get; set; }
        public string DomainShorturl { get; set; }
    }
    public class SmsRequestModel
    {
        public string to { get; set; }
        public string text { get; set; }
        public string customdata { get; set; }
        public int isPremium { get; set; }
        public int isFlash { get; set; }
        public int isLongmessage { get; set; }
        public int isRandomRoute { get; set; }
        public int shortUrlConfig { get; set; }
        public int url { get; set; }
        public int domainShorturl { get; set; }
    }
    public class RespuestaSmsDTO
    {
        public bool Respuesta { get; set; }
    }

    public class PlantillaSmsDTO
    {
        public string Nombre { get; set; }
        public string? Usuario { get; set; }
    }
    public class DetallePlantillaSmsDTO
    {
        public int IdPlantillaSms { get; set; }
        public string Text { get; set; }
        public string? CustomData { get; set; }
        public int IsPremium { get; set; }
        public int IsFlash { get; set; }
        public int IsLongmessage { get; set; }
        public int IsRandomRoute { get; set; }
        public int ShortUrlConfig { get; set; }
        public string? Url { get; set; }
        public string DomainShortUrl { get; set; }
        public string? Usuario { get; set; }
    }
    public class ActualizarPlantillaSmsDTO
    {
        public int IdPlantillaSms { get; set; }
        public string Nombre { get; set; }
        public string Text { get; set; }
        public int IsPremium { get; set; }
        public int IsFlash { get; set; }
        public int IsLongmessage { get; set; }
        public int ShortUrlConfig { get; set; }
        public string Url { get; set; }
        public string Usuario { get; set; }
    }

    public class ObtenerDetallePlantillaSmsDTO
    {
        public int IdPlantillaSms { get; set; }
        public string Nombre { get; set; }
        public string Text { get; set; }
        public bool IsPremium { get; set; }
        public bool IsFlash { get; set; }
        public bool IsLongmessage { get; set; }
        public bool ShortUrlConfig { get; set; }
        public string Url { get; set; }
    }
    public class ObtenerPlantillaSmsGrillaDTO
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get; set; }
    }
    public class InsertarResponsableAlumnoEnviadoSms
    {
        public int IdCampaniaGeneralDetalleResponsableAlumnoSms { get; set; }
        public int IdAlumno { get; set; }
        public string CelularSms { get; set; }
        public string MessageId { get; set; }
        public string JsonEnvio { get; set; }

    }
    public class PruebaPlantillaSmsDTO
    {
        public string Celular { get; set; }
        public string Text { get; set; }
        public string? CustomData { get; set; }
        public bool IsPremium { get; set; }
        public bool IsFlash { get; set; }
        public bool IsLongmessage { get; set; }
        public bool IsRandomRoute { get; set; }
        public bool ShortUrlConfig { get; set; }
        public string? MessageId { get; set; }
        public string? Url { get; set; }
        public string? DomainShortUrl { get; set; }
        public string? Usuario { get; set; }

    }
    public class GrillaSms
    {
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public string Mensaje { get; set; }
        public string MensajePlantillaHtml { get; set; }
        public string FechaRecibido { get; set; }
        public int Tipo { get; set; }
        public string Correo { get; set; }
        public string NombreCompleto { get; set; }
        public string Asesor { get; set; }
        public string CentroCosto { get; set; }
        public int IdCampaniaGeneralDetalleResponsableAlumnoSms { get; set; }
    }
    public class tabGrillaSms
    {
        public int tab { get; set; }
        public int dia { get; set; }
    }
    public class ChatSms
    {
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public string Mensaje { get; set; }
        public string MensajePlantillaHtml { get; set; }
        public string FechaRecibido { get; set; }
        public int Tipo { get; set; }
        public string Correo { get; set; }
        public string NombreCompleto { get; set; }
    }
    public class DatosAlumno
    {
        public int IdAlumno { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int IdCargo { get; set; }
        public int IdATrabajo { get; set; }
        public int IdAFormacion { get; set; }
        public int IdIndustris { get; set; }
    }
    public class EstadoDTO
    {
        public bool Estado { get; set; }
    }
    public class EnviosDTO
    {
        public int? NumeroEnvios { get; set; }
    }
    public class CondifuracionEnvioSmsDTO
    {
        public string usuario { get; set; }
        public string pass { get; set; }
    }
}





