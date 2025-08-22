
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PostulanteExperienciaLogDTO
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public int IdPostulanteExperiencia { get; set; }
        public int? IdEmpresa { get; set; }
        public string? OtraEmpresa { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? NombreJefe { get; set; }
        public string? NumeroJefe { get; set; }
        public bool? AlaActualidad { get; set; }
        public bool? EsUltimoEmpleo { get; set; }
        public decimal? Salario { get; set; }
        public string? Funcion { get; set; }
        public decimal? SalarioComision { get; set; }
        public int? IdMoneda { get; set; }
        public string? TipoActualizacion { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }

    public class PostulanteExperienciaLogV2DTO
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public string Empresa { get; set; }
        public string OtraEmpresa { get; set; }
        public string Cargo { get; set; }
        public string AreaTrabajo { get; set; }
        public string Industria { get; set; }
        public string EsUltimoEmpleo { get; set; }
        public string Salario { get; set; }
        public string SalarioComision { get; set; }
        public string Moneda { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string AlaActualidad { get; set; }
        public string NombreJefe { get; set; }
        public string NumeroJefe { get; set; }
        public string Funcion { get; set; }
        public string TipoActualizacion { get; set; }
        public string FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }


}
