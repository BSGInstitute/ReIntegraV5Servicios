namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class TipoDocumentoAlumnoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public string NombrePlantillaFrontal { get; set; }
        public int IdPlantillaPosterior { get; set; }
        public string NombrePlantillaPosterior { get; set; }
        public int IdOperadorComparacion { get; set; }
        public bool TieneDeuda { get; set; }
    }
    public class PlantilaCertificadoConstanciaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPlantillaBase { get; set; }
    }
    public class TipoDocumentoAlumnoComboPGDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class TipoDocumentoAlumnoDetalleDTO
    {
        public IEnumerable<int> IdsEstadoMatricula { get; set; }
        public IEnumerable<int> IdsModalidad { get; set; }
        public IEnumerable<int> IdsSubEstadoMatricula { get; set; }
        public IEnumerable<int> IdsPGeneral { get; set; }
    }
    public class TipoDocumentoAlumnoCombosDTO
    {
        public IEnumerable<ComboDTO> filtroModalidadCurso { get; set; }
        public IEnumerable<ComboDTO> filtroEstadoMatricula { get; set; }
        public IEnumerable<ComboDTO> filtroOperadorComparacion { get; set; }
        public IEnumerable<SubEstadoMatriculaFiltroDTO> filtroSubEstadoMatricula { get; set; }
    }
    public class TipoDocumentoAlumnoDetalleConfiguracionDTO
    {
        public int Id { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdEstadoMatricula { get; set; }
        public int IdSubEstadoMatricula { get; set; }
        public int IdOperadorComparacion { get; set; }
        public bool TieneDeuda { get; set; }
    }
    public class TipoDocumentoAlumnoListaDetalleConfiguracionDTO
    {
        public int Id { get; set; }
        public List<int> IdsModalidad { get; set; }
        public List<int> IdsEstadoMatricula { get; set; }
        public List<int> IdsSubEstadoMatricula { get; set; }
        public List<int> IdsProgramaGeneral { get; set; }
        public int IdOperadorComparacion { get; set; }
        public bool TieneDeuda { get; set; }
    }
    public class TipoDocumentoAlumnoEntidadDTO
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int IdPlantillaPosterior { get; set; }
        public int IdCriterioNota { get; set; }
        public bool TieneDeuda { get; set; }
        public List<int> IdsPGenerales { get; set; }
        public List<int> IdsModalidad { get; set; }
        public List<int> IdsEstadoMatricula { get; set; }
        public List<int> IdsSubEstadoMatricula { get; set; }
    }
}
