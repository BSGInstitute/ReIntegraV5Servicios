using BSI.Integra.Aplicacion.DTO.SCode;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AsignacionManualOportunidadFiltroDTO
    {
        public string CentrosCosto { get; set; }
        public string Asesores { get; set; }
        public string TiposDato { get; set; }
        public string? Origenes { get; set; }
        public string Categorias { get; set; }
        public string FasesOportunidad { get; set; }
        public string Programa { get; set; }
        public string Area { get; set; }
        public string subArea { get; set; }
        public string Pais { get; set; }
        public string TipoCategoriaOrigen { get; set; }
        public string contacto { get; set; }
        public string email { get; set; }
        public Nullable<DateTime> FechaInicio { get; set; }
        public Nullable<DateTime> FechaFin { get; set; }
        public string Probabilidad { get; set; }
        public string ventaCruzada { get; set; }
        public string UsuarioModificacion { get; set; }
        public Nullable<DateTime> FechaProgramacionInicio { get; set; }
        public Nullable<DateTime> FechaProgramacionFin { get; set; }
        public int? NroOportunidades { get; set; }
        public int? IdOperadorComparacionNroOportunidades { get; set; }
        public int? NroSolicitud { get; set; }
        public int? IdOperadorComparacionNroSolicitud { get; set; }
        public int? NroSolicitudPorArea { get; set; }
        public int? IdOperadorComparacionNroSolicitudPorArea { get; set; }
        public int? NroSolicitudPorSubArea { get; set; }
        public int? IdOperadorComparacionNroSolicitudPorSubArea { get; set; }
        public int? NroSolicitudPorProgramaGeneral { get; set; }
        public int? IdOperadorComparacionNroSolicitudPorProgramaGeneral { get; set; }
        public int? NroSolicitudPorProgramaEspecifico { get; set; }
        public int? IdOperadorComparacionNroSolicitudPorProgramaEspecifico { get; set; }
    }
    public class AsignacionManualOportunidadOperacionesFiltroGrillaDTO
    {
        public PaginadorDTO Paginador { get; set; }
        public GridFiltersDTO? Filter { get; set; }
        public AsignacionManualOportunidadOperacionesFiltroDTO Filtro { get; set; }
    }
    public class AsignacionManualOportunidadOperacionesFiltroDTO
    {
        public List<int> ListaPersonal { get; set; }
        public List<int> ListaCentroCosto { get; set; }
        public List<int> ListaEstados { get; set; }
        public List<int> ListaSubestados { get; set; }
        public string Email { get; set; }
        public string CodigoMatricula { get; set; }
        public List<string> ListaCodigoMatricula { get; set; }
        public List<string> ListaModalidad { get; set; }
        public int Personal { get; set; }
    }
    public class ResultadoFiltroAsignacionOportunidadDTO
    {
        public List<AsignacionManualOportunidadOperacionesDTO> Lista { get; set; }
        public int Total { get; set; }
    }
    public class AsignacionManualOportunidadOperacionesDTO
    {
        public int Id { get; set; }
        public string CentroCosto { get; set; }
        public string Alumno { get; set; }
        public string Email { get; set; }
        public string Personal { get; set; }
        public string CodigoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class TotalOportunidadAsignacionManualOperacionesDTO
    {
        public int Cantidad { get; set; }
    }
    public class AsignarOportunidadOperacionesFiltroDTO
    {
        public int IdPersonal { get; set; }
        public int IdTab { get; set; }
        public List<int> ListaOportunidades { get; set; }
        public string Usuario { get; set; }
    }
}
