using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionBeneficioProgramaGeneralDTO
    {
        public int? Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdBeneficio { get; set; }
        public int? Tipo { get; set; }
        public bool? Asosiar { get; set; }
        public int Entrega { get; set; }
        public int? IdMigracion { get; set; }
        public int? AvanceAcademico { get; set; }
        public bool? DeudaPendiente { get; set; }
        public int? OrdenBeneficio { get; set; }
        public bool? DatosAdicionales { get; set; }
        public string? Requisitos { get; set; }
        public string? ProcesoSolicitud { get; set; }
        public string? DetallesAdicionales { get; set; }
        public List<ConfiguracionBeneficioProgramaGeneralEstadoMatriculaDTO> ConfiguracionBeneficioProgramaGeneralEstadoMatriculas { get; set; }
        public List<ConfiguracionBeneficioProgramaGeneralSubEstadoDTO> ConfiguracionBeneficioProgramaGeneralSubEstados { get; set; }
    }
    public class ConfiguracionBeneficioProgramaGeneralAlternoDTO
    {
        public int IdPgeneral { get; set; }
        public int IdBeneficio { get; set; }
        public string? Requisitos { get; set; }
        public string? ProcesoSolicitud { get; set; }
        public string? DetallesAdicionales { get; set; }
    }
    public class ConfiguracionBeneficioProgramaGeneralComboDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdBeneficio { get; set; }
        public int Tipo { get; set; }
    }
    public class PgeneralConfiguracionBeneficioDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdBeneficio { get; set; }
        public int? TipoBeneficio { get; set; }
        public string Descripcion { get; set; }
        public List<int>? EstadosMatricula { get; set; }
        public List<int>? SubEstadosMatricula { get; set; }
        public List<int>? Paises { get; set; }
        public List<int>? Versiones { get; set; }
        public List<int>? DatosAdicional { get; set; }
        public int Entrega { get; set; }
        public bool Asosiar { get; set; }
        public bool? DeudaPendiente { get; set; }
        public int? AvanceAcademico { get; set; }
        public int? OrdenBeneficio { get; set; }
        public bool? DatosAdicionales { get; set; }
    }
    public class BeneficiosConfiguradosProgramaGeneralDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdBeneficio { get; set; }
        public int? TipoBeneficio { get; set; }
        public string Descripcion { get; set; }
        public int Entrega { get; set; }
        public bool Asosiar { get; set; }
        public bool? DeudaPendiente { get; set; }
        public int? AvanceAcademico { get; set; }
        public int? OrdenBeneficio { get; set; }
        public bool? DatosAdicionales { get; set; }
        public int? IdVersionPrograma { get; set; }
        public int? IdPais { get; set; }
    }

    public class BeneficioDTO
    {
        public int Paquete { get; set; } = 0;
        public string Titulo { get; set; } = "";
        public int OrdenBeneficio { get; set; } = 0;
    }

    public class BeneficioDTOjson
    {
        public int Paquete { get; set; } = 0;
        public string? Version { get; set; } = "";
        public string Titulo { get; set; } = "";
        public int OrdenBeneficio { get; set; } = 0;
    }
    public class BeneficioDetalleRequisitoDTO
    {
        public string? Requisitos { get; set; }
        public string? ProcesoSolicitud { get; set; }
        public string? DetallesAdicionales { get; set; }
    }
}
