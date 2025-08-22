namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ReporteProblemaAulaVirtuaCombolDTO
    {
        public List<MatriculaCabeceraComboDTO> MatriculasCabecera { get; set; }
        public List<PersonalComboDTO> Coordinadores { get; set; }
        public List<ComboDTO> CentroCostos { get; set; }
        public List<ComboDTO> TiposCategoriaError { get; set; }
    }
    public class ReporteProblemasAulaVirtualResultadoDTO
    {
        public int Id { get; set; }
        public string NombreAlumno { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreCentroCosto { get; set; }
        public string Coordinador { get; set; }
        public string Capitulo { get; set; }
        public string Sesion { get; set; }
        public int IdTipoCategoriaError { get; set; }
        public string NombreTipoCategoriaError { get; set; }
        public string Descripcion { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
    public class ReporteProblemasAulaVirtualFiltroDTO
    {
        public List<int>? IdsCentrosCosto { get; set; }
        public List<int>? IdsCoordinadores { get; set; }
        public List<int>? IdsMatriculasCabecera { get; set; }
        public List<int>? IdsTiposCategoriaError { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
