
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PespecificoParticipacionExpositorDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int? Orden { get; set; }
        public int? Grupo { get; set; }
        public int? IdExpositorCurso { get; set; }
        public string? ExpositorCurso { get; set; }
        public int? IdExpositorGrupo { get; set; }
        public string? ExpositorGrupo { get; set; }
        public int? IdExpositorV3 { get; set; }
        public string? ExpositorV3 { get; set; }
        public int? IdExpositorGrupoConfirmado { get; set; }
        public int? IdProveedorFurHonorario { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdProveedorPlanificacionGrupo { get; set; }
        public int? IdProveedorOperacionesGrupoConfirmado { get; set; }
        public bool? EsSilaboAprobado { get; set; }
    }
    public class PEE_ProveedorOperacionesGrupoConfirmadoDTO
    {
        public int Id { get; set; }
        public int? IdProveedorOperacionesGrupoConfirmado { get; set; }
    }
    public class ParticipacionExpositorDTO
    {
        public int Id { get; set; }
        public int? IdExpositorConfirmado { get; set; }
        public int? IdProveedorFur { get; set; }
        public int? IdProveedorOperacionesGrupoConfirmado { get; set; }
    }
    public class CombosPEspecificoExpositorDTO
    {
        public IEnumerable<ComboDTO>? Estados { get; set; }
        public IEnumerable<ComboDTO>? CiudadesBs { get; set; }
        public IEnumerable<ComboDTO>? Modalidades { get; set; }
        public IEnumerable<ComboDTO>? ProovedorHonorarios { get; set; }
        public IEnumerable<ComboDTO>? Expositores { get; set; }
        public IEnumerable<ComboDTO>? Areas { get; set; }
        public IEnumerable<SubAreaCapacitacionFiltroDTO>? Subareas { get; set; }
        public IEnumerable<ProgramaGeneralSubAreaFiltroDTO>? PGenerales { get; set; }
        public IEnumerable<PEspecificoProgramaGeneralFiltroDTO>? PEspecificos { get; set; }
        public IEnumerable<CentroCostoProgramaEspecificoFiltroDTO>? CentroCostos { get; set; }
    }
    public class PEspecificoHistorialParticipacionDocenteDTO
    {
        public int Id { get; set; }
        public int Anho { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public string PEspecificoPadre { get; set; }
        public string EstadoPrograma { get; set; }
        public int IdPEspecifico { get; set; }
        public string PEspecifico { get; set; }
        public string EstadoCurso { get; set; }
        public string Modalidad { get; set; }
        public string ModalidadPrograma { get; set; }
        public int IdCentroCostoPrograma { get; set; }
        public string CentroCostoPrograma { get; set; }
        public string Ciudad { get; set; }
        public int? Orden { get; set; }
        public int? Grupo { get; set; }
        public string EstadoParticipacion { get; set; }

        public string ExpositorPlanificacion { get; set; }
        public string ExpositorV3 { get; set; }
        public string ExpositorConfirmado { get; set; }
        public int? IdExpositorConfirmado { get; set; }
        public int? IdProveedorPlanificacionGrupo { get; set; }
        public int? IdProveedorOperacionesGrupoConfirmado { get; set; }

        public int? IdProveedorFur { get; set; }

        public string ProveedorFur { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }

        public bool? EsNotaAprobada { get; set; }
        public bool? EsAsistenciaAprobada { get; set; }
        public bool AplicaCierreAsistencia { get; set; }
    }

    public class ParticipacionExpositorFiltroDTO
    {
        public int IdExpositor { get; set; }
        public string? IdArea { get; set; }
        public string? IdSubArea { get; set; }
        public string? IdPGeneral { get; set; }
        public string? IdProgramaEspecifico { get; set; }
        public string? IdCentroCosto { get; set; }
        public string? IdEstadoPEspecifico { get; set; }
        public string? IdCodigoBSCiudad { get; set; }
        public string? IdModalidadCurso { get; set; }
        public int? IdCentroCostoD { get; set; }
        public string? IdProveedorPlanificacion { get; set; }
        public string? IdProveedorOperaciones { get; set; }
        public string? IdProveedorFur { get; set; }
        public bool? SinNotaAprobada { get; set; }
        public bool? SinAsistenciaAprobada { get; set; }
    }
}
