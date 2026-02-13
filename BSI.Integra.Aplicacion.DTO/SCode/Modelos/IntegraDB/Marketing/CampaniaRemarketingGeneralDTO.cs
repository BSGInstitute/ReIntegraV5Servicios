using System.Text.Json.Serialization;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing
{
    public class CampaniaRemarketingGeneralDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaEnvioProgramada { get; set; }
        public string EnvioConfigurado { get; set; }
        public string MedioEnvio { get; set; }
        public string EstadoEnvio { get; set; }
        public string IdentificadorLlamadaIA { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class CombosConfiguracionCampaniaDTO
    {
        public List<ElementoConfiguracionCampania> MedioEnvio { get; set; }
        public List<ElementoConfiguracionCampania> TipoMensaje { get; set; }
        public List<ElementoConfiguracionCampania> LogicaEnvio { get; set; }
        public List<ElementoConfiguracionCampania> Argumento { get; set; }
        public List<ElementoConfiguracionCampania> CategoriaArgumento { get; set; }
        public List<int> PrioridadesUnicas { get; set; }
    }
    public class ElementoConfiguracionCampania
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class SegmentoCreadoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool FiltroEjecutado { get; set; }
    }

    public class ResultadoTextoGeneradoDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string Pais { get; set; }
        public string ContenidoGenerado { get; set; }
    }

    public class EnvioCampaniaRemarketingDTO
    {
        public int? Id { get; set; }
        public SegmentoDTO Segmento { get; set; }
        public List<int> MediosEnvio { get; set; }
        public int TipoMensaje { get; set; }
        public int LogicaEnvio { get; set; }
        public List<int> Argumentos { get; set; }
        public string? RemitenteCorreo { get; set; }
        public string? RemitenteNombre { get; set; }
        public string? Asunto { get; set; }
        public string EnvioSeleccionado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? IdentificadorLlamadaIA { get; set; }
    }

    public class ConfiguracionCampaniaRemarketingDTO
    {
        public int? Id { get; set; }
        public SegmentoDTO Segmento { get; set; }
        public List<ElementoConfiguracionCampaniaDTO> MediosEnvio { get; set; }
        public ElementoConfiguracionCampaniaDTO TipoMensaje { get; set; }
        public ElementoConfiguracionCampaniaDTO LogicaEnvio { get; set; }
        public ElementoConfiguracionCampaniaDTO CategoriaArgumento { get; set; }
        public List<int> Prioridades { get; set; }
        public string? RemitenteCorreo { get; set; }
        public string? RemitenteNombre { get; set; }
        public string? Asunto { get; set; }
        public string EnvioSeleccionado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? IdentificadorLlamadaIA { get; set; }
        public bool? FlagEditar { get; set; }
    }
    public class ElementoConfiguracionCampaniaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }



    public class SegmentoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class DetallesCampaniaDTO
    {
        public int TotalMensajes { get; set; }
        public int Enviados { get; set; }
        public int Abiertos { get; set; }
        public int Rebotados { get; set; }
        public List<EstadoAlumnosDTO> EstadoAlumnos { get; set; }
    }

    public class EstadoAlumnosDTO
    {
        public int IdAlumno { get; set; }
        public string EstadoEnvio { get; set; }
        public bool Abierto { get; set; }
        public bool Rebotado { get; set; }
        public string? RazonRechazo { get; set; }
        public DateTime FechaEnvio { get; set; }
    }

    public class MensajeGeneradoDTO
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
    }

    public class CampaniaRemarketingIndividualDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdFiltroSegmento { get; set; }
        public int TipoMensaje { get; set; }
        public int LogicaEnvio { get; set; }
        public string RemitenteCorreo { get; set; }
        public string RemitenteNombre { get; set; }
        public string Asunto { get; set; }
        public string EnvioConfigurado { get; set; }
        public DateTime FechaEnvioProgramada { get; set; }
        public List<int>? MediosEnvio { get; set; } = new();
        public string IdentificadorLlamadaIA { get; set; }
        public int CategoriaArgumento { get; set; }
        public List<int>? Prioridades { get; set; } = new();
    }

    public class IntValueDTO
    {
        public int Value { get; set; }
    }

    //Respuesta de mensaje generado por IA
    public class MensajeGeneradoIA
    {
        public string identificador_llamada { get; set; }
        public DateTime fecha_generacion { get; set; }
        public int id_alumno { get; set; }
        public string canal { get; set; }
        public string contenido { get; set; }
        public List<RespuestaArgumentoIA>? argumentos { get; set; }
    }

    public class RespuestaArgumentoIA
    {
        public int numero_argumento { get; set; }
        public string nombre_argumento { get; set; }
        public double score_argumento { get; set; }
    }

    public class EstadoEjecucionLlamadaIA
    {
        public string id_llamada { get; set; }
        public int total { get; set; }
        public int pendientes { get; set; }
        public int finalizados { get; set; }
        public int en_proceso { get; set; }
        public List<string> error { get; set; }
        public List<MensajeGeneradoIA>? mensajesGenerados { get; set; }
    }

    public class RespuestaIdentificadorLlamadaIA
    {
        public string id_llamada { get; set; }
    }

    public class AlumnoCorreoDTO
    {
        public int IdAlumno { get; set; }
        public string Correo { get; set; }
    }

    /// DTO para almacenar el estado de envío de cada correo en una campaña
    public class RemarketingEstadoCampaniaDTO
    {
        public int? Id { get; set; }
        public int IdCampaniaRemarketing { get; set; }
        public int IdAlumno { get; set; }
        public string IdentificadorMensaje { get; set; }
        public bool Enviado { get; set; }
        public bool Entregado { get; set; }
        public bool Abierto { get; set; }
        public bool Rebotado { get; set; }
        public string RazonRechazo { get; set; }
        public string EstadoMandrill { get; set; }
        public string UsuarioCreacion { get; set; }
    }

    /// DTO para el resultado del envío masivo de correos
    public class ResultadoEnvioMasivoDTO
    {
        public int TotalProcesados { get; set; }
        public int TotalEnviados { get; set; }
        public int TotalRechazados { get; set; }
        public int TotalInvalidos { get; set; }
        public List<RemarketingEstadoCampaniaDTO> Detalle { get; set; }
    }

    /// DTO para el mensaje a enviar (combina alumno con su mensaje generado)
    public class MensajeParaEnvioDTO
    {
        public int IdAlumno { get; set; }
        public string Email { get; set; }
        public string Contenido { get; set; }
        public string Asunto { get; set; }
    }

    public class ElementoEstadoEnvio
    {
        public int IdEstadoEnvio { get; set; }
    }

    public class ReenviarMensajeRequest
    {
        public int IdAlumno { get; set; }
        public string IdentificadorLlamadaIA { get; set; }
    }

}