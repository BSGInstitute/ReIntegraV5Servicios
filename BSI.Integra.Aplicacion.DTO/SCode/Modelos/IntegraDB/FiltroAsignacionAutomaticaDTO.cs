namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FiltroAsignacionAutomaticaDTO
    {
        public string IdCentroCosto { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string IdIndustria { get; set; }
        public string IdCategoriaDato { get; set; }
        public string IdCargo { get; set; }
        public string IdPais { get; set; }
        public string IdAreaTrabajo { get; set; }
        public string IdProbabilidad { get; set; }
        public string IdAreaFormacion { get; set; }
    }


    public class AsignacionAutomaticaRegistroImportadoDTO
    {
        public int TotalRegistros { get; set; }
        public string Alumno { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string CentroCosto { get; set; }
        public string TipoDato { get; set; }
        public string Origen { get; set; }
        public string CodigoFase { get; set; }
        public string AreaFormacion { get; set; }
        public string AreaTrabajo { get; set; }
        public string Industria { get; set; }
        public string Cargo { get; set; }
        public string NombrePais { get; set; }
        public string NombreCiudad { get; set; }
        public string origenCampania { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public decimal? ProbabilidadActual { get; set; }
        public string NombreProbabilidadActual { get; set; }
    }


    public class OportunidadesAsesorAsignacionAutomaticaDTO
    {
        public int Id { get; set; }
        public Guid? IdMigracion { get; set; }
    }
    public class OportunidadWhatsappEnvioDTO
    {
        public int IdOportunidad { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public int IdPersonal { get; set; }
        public bool AplicaEnvioWhatsapp { get; set; }
    }

    public class AsignacionAutomaticaFiltroDTO
    {
        public IEnumerable<ComboDTO> listaCentroCosto { get; set; }
        public IEnumerable<ComboDTO> listaCategoriaDato { get; set; }
        public IEnumerable<PaisComboDTO> listaPais { get; set; }
        public IEnumerable<CiudadComboDTO> listaCiudad { get; set; }
        public IEnumerable<ComboDTO> listaProbabilidad { get; set; }
        public IEnumerable<ComboDTO> listaIndustria { get; set; }
        public IEnumerable<ComboDTO> listaCargo { get; set; }
        public IEnumerable<ComboDTO> listaAreaTrabajo { get; set; }
        public IEnumerable<ComboDTO> listaAreaFormacion { get; set; }
    }
    public class FiltroBusquedaAsignacionAutomaticaCompuestoDTO
    {
        public PaginadorDTO paginador { get; set; }
        public FiltroAsignacionAutomaticaDTO? filtroRegistros { get; set; }
    }
    public class ResultadoAsignacionDTO
    {
        public int IdOportunidad { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
