using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PreguntaFrecuenteDTO
    {
        public int Id { get; set; }
        public int IdSeccionPreguntaFrecuente { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int? Tipo { get; set; }  
    }
    public class PreguntaFrecuenteComboModuloDTO
    {
        public List<ComboDTO> SeccionPreguntaFrecuente { get; set; }
        public List<PGeneralSubAreaFiltroDTO> PGeneralSubArea { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> SubAreaCapacitacion { get; set; }
        public List<AreaCapacitacionFiltroDTO> AreaCapacitacion { get; set; }
        public List<ComboDTO> ModalidadCurso { get; set; }
    }
    public class PreguntaFrecuentePreguntaRespuestaDTO
    {
        public string? Pregunta { get; set; }
        public string? Respuesta { get; set; }
    }

    public class PreguntaFrecuenteComboDTO
    {
        public int Id { get; set; }
        public int IdSeccion { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int Tipo { get; set; }
        public List<int> PreguntaFrecuenteAreas { get; set; }
        public List<int> PreguntaFrecuenteSubAreas { get; set; }
        public List<int?> PreguntaFrecuentePGenerals { get; set; }
        public List<int> PreguntaFrecuenteTipos { get; set; }
    }
    public class PreguntaFrecuentePgeneralDatosDTO
    {
        public int Id { get; set; }
        public int? IdPreguntaFrecuente { get; set; }
        public int? IdPGeneral { get; set; }
    }
    public class PreguntaFrecuenteParametrosDTO
    {
        public PreguntaFrecuenteDTO PreguntaFrecuente { get; set; }
        public List<int> PreguntaFrecuenteAreas { get; set; }
        public List<int> PreguntaFrecuenteSubAreas { get; set; }
        public List<int> PreguntaFrecuentePGenerals { get; set; }
        public List<int> PreguntaFrecuenteTipos { get; set; } 
    }
    public class FiltroPreguntaFrecuenteDTO
    {
        public List<int> Areas { get; set; }
        public List<int> Subareas { get; set; }
        public List<int> PGenerales { get; set; }
    }
    public class PreguntaFrecuenteFiltroResultadoDTO
    {
        public int Id { get; set; }
        public int? IdSeccion { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int? Tipo { get; set; } 
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdTipo { get; set; }
    }
    public class PreguntaFrecuenteFiltroResultadoAgrupadoDTO
    {
        public int Id { get; set; }
        public int? IdSeccion { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int? Tipo { get; set; }
        public List<int?> IdsAreas { get; set; }
        public List<int?> IdsSubareas { get; set; }
        public List<int?> IdsPgenerales { get; set; }
        public List<int?> IdsTipos { get; set; }
    }
}

