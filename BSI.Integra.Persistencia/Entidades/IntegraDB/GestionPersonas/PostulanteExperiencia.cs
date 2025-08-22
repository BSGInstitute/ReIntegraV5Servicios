using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PostulanteExperiencia : BaseIntegraEntity
    {
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
        public string TipoActualizacion { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
