using BSI.Integra.Aplicacion.DTO.SCode;
using Google.Api.Ads.AdWords.v201809;
using System.Security.Policy;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CampaniaGeneralWhatsAppDTO

    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public TimeSpan? HoraEnvio { get; set; }
        public DateTime? FechaInicioEnvioWhatsapp { get; set; }
    }
    public class ObtenerCampaniaGeneralDetalleWhatsAppGrupoDTO
    {
        public int Id { get; set; }
        public string NombreCampaniaGeneralWhatsApp { get; set; }
        public string FechaInicioEnvioWhatsapp { get; set; }
        public string HoraEnvio { get; set; }
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public string NombreCampaniaOrigen { get; set; }
        public int Prioridad { get; set; }
        public string Nombre { get; set; }
        public bool ActivarMasivo { get; set; }
        public int Programados { get; set; }
        public int CantidadBase { get; set; }
        public int Enviados { get; set; }

    }

    public class ObtenerCampaniaGeneralDetalleWhatsAppDTO
    {
        public int Id { get; set; }
        public string NombreCampaniaGeneralWhatsApp { get; set; }
        public string FechaInicioEnvioWhatsapp { get; set; }
        public string HoraEnvio { get; set; }
        public List<ObtenerCampaniaGeneralDetallePrioridadWhatsAppDTO> ObtenerCampaniaGeneralDetallePrioridadWhatsApp { get; set; }


    }
    public class ObtenerCampaniaGeneralDetallePrioridadWhatsAppDTO
    {
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public string NombreCampaniaOrigen { get; set; }
        public int Prioridad { get; set; }
        public string Nombre { get; set; }
        public bool ActivarMasivo { get; set; }
        public int Programados { get; set; }
        public int CantidadBase { get; set; }
        public int Enviados { get; set; }

    }

    public class ActualizarActivarMasivoPorCampaniaDTO
    {
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public bool ActivarMasivo { get; set; }
        public string? Usuario { get; set; }
    }

    public class ActualizarCamposCampaniaGeneralDetalleWhatsAppDTO
    {
        public string Nombre { get; set; }
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public int Prioridad { get; set; }
        public string Usuario { get; set; }
    }
    public class ProcesarDataPorPrioridadExcelAlumnoDTO
    {
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public string ListaDeAlumnos { get; set; }
        public string Usuario { get; set; }
        public int Dias { get; set; }

    }
    public class ProcesarDataPorPrioridadExcelDTO
    {
        public string Nombre { get; set; }
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public int Prioridad { get; set; }
        public string Usuario { get; set; }
    }

    public class EliminarCampaniaGeneralDetalleWhatsAppDTO
    {
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public string Usuario { get; set; }
    }
    public class EliminarCampaniaGeneralDetalleResponsableWhatsAppDTO
    {
        public int IdCampaniaGeneralDetalleResponsableWhatsApp { get; set; }
        public string Usuario { get; set; }
    }
    public class ActualizarCampaniaGeneralWhatsAppDTO
    {
        public string Nombre { get; set; }
        public string HoraEnvio { get; set; }
        public string FechaInicioEnvioWhatsapp { get; set; }
        public int? Id { get; set; }
        public string? Usuario { get; set; }
    }
    public class ErrorMasivos
    {
        public string error { get; set; }
        public string message { get; set; }
    }
    public class ObtenerCampaniaGeneralGrillaWhatsAppDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string FechaInicioEnvioWhatsapp { get; set; }

        public string HoraEnvio { get; set; }
        public int Cantidad { get; set; }
    }
    public class EliminarCampaniaGeneralWhatsAppDTO
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }


    }
    public class InsertarCampaniaGeneralDetalleWhatsAppDTO
    {
        public string Nombre { get; set; }
        public int IdCampaniaGeneralWhatsApp { get; set; }
        public int Prioridad { get; set; }
        public string Usuario { get; set; }

    }
    public class InsertarCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO
    {
        public string Json { get; set; }
        public int IdCampaniaGeneralDetalleResponsableWhatsApp { get; set; }
        public string Usuario { get; set; }

    }
    public class ObtenerConfiguracionCampaniaGeneralDetalleWhatsAppDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public int IdCampaniaGeneral { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }

    }

    public class ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO
    {
        public int Id { get; set; }
        public int CantidadBase { get; set; }
        public int CantidadDisponible { get; set; }
        public int IdCampaniaGeneralDetalleResponsableWhatsApp { get; set; }
        public string Asesor { get; set; }
        public string Plantilla { get; set; }
        public string CentroCosto { get; set; }
        public int Cantidad { get; set; }
        public int Enviados { get; set; }
        public int AlumnoConfigurado { get; set; }

    }
    public class ValorDevueltoDTO
    {
        public int Valor { get; set; }


    }
    public class ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoDTO
    {
        public int Id { get; set; }
        public int CantidadBase { get; set; }
        public int CantidadDisponible { get; set; }
        public List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadListaDTO> ObtenerCampaniaGeneralDetalleResponsablePorPrioridadLista { get; set; }
    }
    public class ObtenerCampaniaGeneralDetalleResponsablePorPrioridadListaDTO
    {
        public int IdCampaniaGeneralDetalleResponsableWhatsApp { get; set; }
        public string Asesor { get; set; }
        public string Plantilla { get; set; }
        public string CentroCosto { get; set; }
        public int Cantidad { get; set; }
        public int Enviados { get; set; }
        public int AlumnoConfigurado { get; set; }
    }
    public class InsertarCampaniaGeneralDetalleResponsableWhatsAppDTO
    {
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public int IdPersonal { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdPlantilla { get; set; }
        public int IdCentroCosto { get; set; }
        public int Cantidad { get; set; }
        public int Dias { get; set; }
        public string Usuario { get; set; }
    }
    public class ProcesarDataPorPrioridadSendinblueDTO
    {
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public int Dias { get; set; }
        public string Usuario { get; set; }

    }
    public class InsertarCampaniaGeneralDetalleResponsableInserWhatsAppDTO
    {
        public int Id { get; set; }
        public int IdCampaniaGeneralDetalleResponsableWhatsApp { get; set; }
        public int IdAlumno { get; set; }
        public int WhatsAppEmpresaIdPais { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public string CelularWhatsApp { get; set; }
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
    public class ComboCampaniaGeneralDetalleResponsableWhatsAppDTO
    {
        public List<ComboGeneralDTO>? IdPersonal { get; set; }
        public List<ComboGeneralDTO>? IdPGeneral { get; set; }
        public List<ComboGeneralDTO>? IdPlantilla { get; set; }
        public List<ComboGeneralCentroCostoAreaCapacitacionDTO>? IdCentroCosto { get; set; }

    }
    public class ObtenerComboRespuestaWhatsAppp
    {
        public List<ComboPaisDTO>? IdPaisDTO { get; set; }
        public List<ComboGeneralDTO>? IdPlantilla { get; set; }

        public List<ComboGeneralCentroCostoAreaCapacitacionDTO>? IdCentroCosto { get; set; }

    }
    public class ComboGeneralDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class ComboPaisDTO
    {
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
    }
    public class ComboGeneralCentroCostoAreaCapacitacionDTO
    {
        public int IdCentroCosto { get; set; }
        public string Nombre { get; set; }
    }
    public class ObtenerComboCampaniasSendinBlueDTO
    {
        public List<ComboCampaniaGeneralDTO>? IdCampaniaGeneral { get; set; }
        public List<IdCampaniaGeneralDetalleDTO>? IdCampaniaGeneralDetalle { get; set; }


    }
    public class ComboCampaniaGeneralDTO
    {
        public int IdCampaniaGeneral { get; set; }
        public string Nombre { get; set; }
    }
    public class IdCampaniaGeneralDetalleDTO
    {
        public int IdCampaniaGeneral { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public string Nombre { get; set; }
    }
    public class ObtenerComboCentroCostoCampaniasSendinBlueDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class IdProgramaGeneralPlantillaDTO
    {
        public int IdProgramaGeneral { get; set; }
    }
    public class ObtenerPlantillaWhatsAppDTO
    {
        public string Descripcion { get; set; }
    }
    public class ObtenerDatosPorPrioridadAsignadaDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdPlantilla { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdProgramaGeneral { get; set; }
    }
    public class ObtenerAlumnoConfiguradoPorPrioridadDTO
    {
        public int IdAlumno { get; set; }
        public string CelularWhatsApp { get; set; }
        public int WhatsAppEmpresaIdPais { get; set; }
    }

    public class PrioridadDatosDTO
    {
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public int Cantidad { get; set; }
        public int Dias { get; set; }

    }
    public class AlumnoWhatsAppMasivo
    {
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public string Plantilla { get; set; } = "";
        public int WhatsAppEmpresaIdPais { get; set; }
        public string ObjetoPlantilla { get; set; }
        public List<DatoPlantillaWhatsAppDTO> ListaObjetoPlantilla { get; set; }

    }
    public class AlumnoWhatsAppMasivoJSON
    {
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public string Plantilla { get; set; } = "";
        public int WhatsAppEmpresaIdPais { get; set; }
        public string ObjetoPlantilla { get; set; }
        public int Dias { get; set; }
    }
    public class AlumnoWhatsAppMasivoBaseDTO
    {
        public int IdAlumno { get; set; }
        public string CelularWhatsApp { get; set; }
        public int WhatsAppEmpresaIdPais { get; set; }

    }
    public class ReporteInteraccionCampaniaGeneralDetalleDTO
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
    public class SumaValidadorChatDTO
    {
        public int IdAlumno { get; set; }
        public string CelularWhatsApp { get; set; }
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public string Usuario { get; set; }
    }


    public class WhatsAppPlantillaDTO
    {
        public int IdPlantilla { get; set; }
        public int IdCentroCosto { get; set; }
        public string CelularWhatsApp { get; set; }
        public int IdPais { get; set; }
        public int IdAlumno { get; set; }
        public int IdPersonal { get; set; }
        public string usuario { get; set; }

    }
    public class WhatsAppPlantillaListaDTO
    {
        public int IdPlantilla { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdPersonal { get; set; }
        public string usuario { get; set; }
        public List<AlumnoData> Alumnos { get; set; } = new List<AlumnoData>();
        public class AlumnoData
        {
            public string CelularWhatsApp { get; set; }
            public int IdPais { get; set; }
            public int IdAlumno { get; set; }
        }

    }

    public class WhatsAppMensajeTextoDTO
    {
        public string CelularWhatsApp { get; set; }
        public string Mensaje { get; set; }
        public int IdPais { get; set; }
        public int IdAlumno { get; set; }
        public int IdPersonal { get; set; }
        public string usuario { get; set; }

    }

    public class WhatsAppMensajeTextoFacebookDTO
    {
        public string CelularWhatsApp { get; set; }
        public string Mensaje { get; set; }
        public int IdPais { get; set; }
        public int IdAlumno { get; set; }
        public int IdPersonal { get; set; }
        public string usuario { get; set; }

    }
    public class WhatsAppMensajeArchivoFacebookDTO
    {
        public string WaTo { get; set; }
        public string WaType { get; set; }
        public string WaLink { get; set; }
        public string WaFileName { get; set; }
        public int IdPais { get; set; }
        public int IdAlumno { get; set; }
        public int? IdPersonal { get; set; }

    }
    public class RespuestaMensajeHookDTO
    {
        public string Mensaje { get; set; }
        public string WaId { get; set; }
        public bool EstadoMensaje { get; set; }
        public string? NumeroEnvio { get; set; }

    }

    public class RespuestaMensajeWhatsappDTO
    {
        public string Mensaje { get; set; }
        public bool Estado { get; set; }

    }

    public class TareasRespuestaMensajeHookDTO
    {
        public PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO Item { get; set; }
        public Task<RespuestaMensajeHookDTO>? Tarea  { get; set; }

    }

    public class SumaOportunidadWhatsAootDTO
    {
        public int IdAlumno { get; set; }
        public string CelularWhatsApp { get; set; }
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public int IdCentroCosto { get; set; }
        public string Usuario { get; set; }
    }

    public class ProgramaprobabilidadDTO
    {
        public string Nombre { get; set; }
        public float Probabilidad { get; set; }
        public int IdAlumno { get; set; }
    }

    public class NombreDTO
    {
        public string Nombre { get; set; }
    }

    public class ObtenerPrioridadesEnvioWhatsAppDTO
    {
        public int IdCampaniaGeneralWhatsApp { get; set; }
        public string FechaInicioEnvioWhatsapp { get; set; }
        public string HoraEnvio { get; set; }
        public int IdCampaniaGeneralDetalleWhatsApp { get; set; }
        public int IdCampaniaGeneralDetalleResponsableWhatsApp { get; set; }
        public string NombreCampania { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public string Personal { get; set; }


    }
    public class AsesoresMktDTO
    {
        public string Email { get; set; }

    }
    public class CampaniaGeneralDetalleResponsableAlumnoLogWhatsAppDTO
    {
        public int Id { get; set; }
        public int IdCampaniaGeneralDetalleResponsableWhatsApp { get; set; }
        public string FechaEnvio { get; set; }
        public string HoraEnvio { get; set; }
        public bool Estado { get; set; }

    }
    public class ResultadoEjecucionCampaniaDTO
    {
        public List<ObtenerPrioridadesEnvioWhatsAppDTO> ListaPrioridades { get; set; }
        public string HoraServidor { get; set; }
        public string FechaServidor { get; set; }
        public string HoraProgramada { get; set; }
        public string FechaProgramada { get; set; }
    }

    public class DiasWhatsappDTO
    {
        public int Dias { get; set; }

    }

    public class IdDiasWhatsappDTO
    {
        public int Id { get; set; }
        public int Dias { get; set; }


    }
    public class UltimoMensajeDTO
    {
        public string UltimoMensaje { get; set; }
    }

    public class NumeroIdentificadorDTO
    {
        public string NumeroIdentificador { get; set; }
    }
}




