namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing
{
    public class CampaniaRemarketingGeneralDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaEnvioProgramada { get; set; }
        public string EnvioConfigurado { get; set; }
        public string MedioEnvio { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class CombosConfiguracionCampaniaDTO
    {
        public List<ElementoConfiguracionCampania> MedioEnvio { get; set; }
        public List<ElementoConfiguracionCampania> TipoMensaje { get; set; }
        public List<ElementoConfiguracionCampania> LogicaEnvio { get; set; }
        public List<ElementoConfiguracionCampania> Argumento { get; set; }
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
        public SegmentoDTO Segmento { get; set; }
        public List<int> MediosEnvio { get; set; }
        public int TipoMensaje { get; set; }
        public int LogicaEnvio { get; set; }
        public List<int> Argumentos { get; set; }
        public string RemitenteCorreo { get; set; }
        public string RemitenteNombre { get; set; }
        public string Asunto { get; set; }
        public string EnvioSeleccionado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class SegmentoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class DetallesCampaniaDTO
    {
        public int Programados { get; set; }
        public int Aperturas { get; set; }
        public int Clicks { get; set; }
        public int Rebotados { get; set; }
        public List<AlumnoContactadoDTO> AlumnosContactados { get; set; }
    }

    public class AlumnoContactadoDTO
    {
        public int IdAlumno { get; set; }
        public string EstadoEnvio { get; set; }
        public string NombreAlumno { get; set; }
        public bool Apertura { get; set; }
        public bool Click { get; set; }
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
        public List<int>? Argumentos { get; set; } = new();
    }

    public class IntValueDTO
    {
        public int Value { get; set; }
    }

}