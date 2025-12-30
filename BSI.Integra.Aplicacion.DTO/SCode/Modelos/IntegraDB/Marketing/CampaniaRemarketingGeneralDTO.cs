namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing
{
    public class CampaniaRemarketingGeneralDTO
    {
        public int Id { get; set; }
        public string NombreCampania { get; set; }
        public string tipoCampania { get; set; }
        public string usuarioCreacion { get; set; }
        public DateTime fechaEnvio { get; set; }
        public int cantidad { get; set; }
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
}