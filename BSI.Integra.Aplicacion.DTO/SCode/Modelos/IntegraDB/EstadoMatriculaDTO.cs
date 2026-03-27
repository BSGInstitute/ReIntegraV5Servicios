namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class EstadoMatriculaDTO
    {
        public int Id { get; set; }
        public string EstadoMatricula { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }

    }
    public class EstadoMatriculaListDTO
    {
        public List<TCRM_EstadoMatriculaInsertarDTO> Estado { get; set; }

    }
    public class TCRM_EstadoMatriculaInsertarDTO
    {
        public int Id { get; set; }
        public string EstadoMatricula { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; } = null!;
        public List<int> IdSubEstados { get; set; }
    }
    public class EstadoMatriculaComboDTO
    {
        public int Id { get; set; }
        public string EstadoMatricula { get; set; } = null!;
    }
    public class EstadoMatriculadoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public string EstadoCertificacion { get; set; }
        public string EstadoEvaluacion { get; set; }
        public string EstadoFinanciero { get; set; }
        public string TipoCuota { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int IdCentroCosto { get; set; }
        public int Version { get; set; }
        public int? VersionPrograma { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Documentos { get; set; }
    }

    public class MatriculaAlumnoDTO
    {
        public string EstadoFinanciero { get; set; }
        public string EstadoEvaluacion { get; set; }
        public string EstadoCertificacion { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int Version { get; set; }
        public string TipoCuota { get; set; }
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int? VersionPrograma { get; set; }
        public string? NombrePersonal { get; set; }
        public string? NombreVersionPrograma { get; set; }
    }
    public class MatriculaDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public int? VersionPrograma { get; set; }
        public int? IdCentroCosto { get; set; }
        public string Documentos { get; set; }
    }
    public class CRUDEstadoMatriculaDTO
    {
        public int Id { get; set; }
        public string EstadoMatricula { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public string IdSubEstados { get; set; } = null!;
        public bool Activo { get; set; }
    }
    public class ObtenerEstadoMatriculaDTO
    {
        public int Id { get; set; }
        public string EstadoMatricula { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; } = null!;
    }
    public class EstadosMatriculaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class ConfirmarReclamoDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
    }
    public class ProgramaGeneralMatriculaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

}
