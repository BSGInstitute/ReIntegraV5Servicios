namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SesionWebinarDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public string LinkWebinar { get; set; }
    }
    public class TrabajoCursoAlumnoDTO
    {
        public string DescripcionTrabajo { get; set; }
        public string NombreFormaEntrega { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
    public class MaterialDescargarDTO
    {
        public string UrlArchivo { get; set; }
        public string NombreArchivo { get; set; }
    }
    public class FiltroObtenerSesionesDTO
    {
        public List<int> ListaPEspecificos { get; set; }
        public int PEspecificoId { get; set; }
        public bool CursoIndividual { get; set; }
        public int? NroGrupo { get; set; }
    }
}
