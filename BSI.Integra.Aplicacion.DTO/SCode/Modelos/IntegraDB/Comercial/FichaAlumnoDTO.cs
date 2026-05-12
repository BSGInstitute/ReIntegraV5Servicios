namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial
{
    // -------------------------------------------------------
    // DTOs: Modal Masivo Oportunidades WhatsApp
    // Autor: Miguel Valdivia
    // Fecha: 2026-04-24
    // -------------------------------------------------------

    /// <summary>Request para obtener el IdCentroCosto asignado a un alumno en campaña.</summary>
    public class CentroCostoPorAlumnoRequestDTO
    {
        public int IdAlumno { get; set; }
    }

    /// <summary>Response con el IdCentroCosto (puede ser null si no hay asignación).</summary>
    public class CentroCostoPorAlumnoResponseDTO
    {
        public int? IdCentroCosto { get; set; }
    }

    /// <summary>Request para pre-carga masiva: lista de IdsAlumno y rango de horas.
    /// IdsAlumno es el campo principal (la grilla ya lo tiene disponible).
    /// Celulares se mantiene como fallback para compatibilidad con clientes viejos.
    /// Ambas listas son opcionales — el service valida que al menos una tenga items.</summary>
    public class PreCargaMasivaRequestDTO
    {
        public List<int>? IdsAlumno { get; set; }
        public List<string>? Celulares { get; set; }
        public int HorasAtras { get; set; }
    }

    /// <summary>Un mensaje del historial de chat devuelto por mkt.SP_WhatsAppMensajeObtenerRecientesPorCelular.</summary>
    public class MensajeChatMasivoDTO
    {
        public int TipoMensaje { get; set; }      // 1 = recibido, 2 = enviado
        public string WaType { get; set; }         // text, button, hsm, template, image, voice, audio, video, etc. NULL = campaña masiva
        public string Mensaje { get; set; }
        public string Archivo { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime FechaMensaje { get; set; }
        public string PersonalFiltrado { get; set; }
        /// <summary>HTML listo para renderizar con [innerHTML] en el frontend. Lo construye el service a partir de WaType + Mensaje + Archivo.</summary>
        public string MensajeHtml { get; set; }
    }

    /// <summary>Response por alumno en la pre-carga masiva.</summary>
    public class PreCargaMasivaItemDTO
    {
        public string Celular { get; set; }
        public int IdAlumno { get; set; }
        public int? IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public object Alumno { get; set; }
        public List<MensajeChatMasivoDTO> Mensajes { get; set; }
        public List<object> HistorialOportunidades { get; set; }
        public DateTime? FechaUltimaCaptura { get; set; }
        public bool CargadoOk { get; set; }
        public string ErrorCarga { get; set; }
    }

    /// <summary>Request para ActualizarDatosAlumnoMasivo: lista de perfiles a actualizar.</summary>
    public class ActualizarAlumnoMasivoItemDTO
    {
        public int Id { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string? Nombre1 { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? Email2 { get; set; }
    }

    public class OportunidadWhatsappDTO
    {
        public int IdPersonalAsignado { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdAlumno { get; set; }
        public int? IdOrigen { get; set; }
        public bool? Activo { get; set; }
    }
    public class OportunidadFichaDTO
    {
        public int IdPersonalAsignado { get; set; }
        //public int IdCentroCosto { get; set; }
        public int IdPgeneral { get; set; }
        public int IdOportunidadRN2 { get; set; }
        //public int IdAlumno { get; set; }
        //public int IdClasificacionPersona { get; set; }
        public int IdFaseOportunidad { get; set; }
        public string Usuario { get; set; }
    }
}
