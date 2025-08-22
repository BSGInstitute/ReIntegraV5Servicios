namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MatriculaCabeceraBeneficiosDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdSuscripcionProgramaGeneral { get; set; }
        public int? IdConfiguracionBeneficioProgramaGeneral { get; set; }
        public int? IdEstadoMatriculaCabeceraBeneficio { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public int? IdEstadoSolicitudBeneficio { get; set; }
        public int? Duracion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string? UsuarioAprobacion { get; set; }
        public string? UsuarioEntregoBeneficio { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class MatriculaCabeceraBeneficiosComboDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class CorrespondeBeneficiosDTO
    {
        public List<MatriculaCabeceraBeneficioDTO> beneficios { get; set; }
        public bool corresponde { get; set; }
    }
    public class MatriculaCabeceraBeneficioDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string EstadoMatriculaCabeceraBeneficio { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public string EstadoSolicitudBeneficio { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public int IdConfiguracionBeneficioProgramaGeneral { get; set; }
        public DateTime? FechaEntregaBeneficio { get; set; }
    }
    public class CodigoMatriculaIdStringDTO
    {
        public string Id { get; set; }
    }

    public class CodigoComboMatriculaIdStringDTO
    {
        public string Nombre { get; set; }


    }

    public class BeneficiosSolicitadosDTO
    {
        public string Beneficio { get; set; }
    }

}
