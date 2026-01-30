namespace BSI.Integra.Aplicacion.DTO
{
    public class ValorIntDTO
    {
        public int Id { get; set; }
        public int? Valor { get; set; }
    }
    public class StringDTO
    {
        public string Valor { get; set; }
    }
    public class DateTimeDTO
    {
        public DateTime? Valor { get; set; }
    }
    public class FloatDTO
    {
        public float? Valor { get; set; }
    }
    public class BoolDTO
    {
        public bool? Valor { get; set; }
    }
    public class ValorIdMatriculaDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public bool? Nuevo { get; set; }
    }
    public class ValorDTO
    {
        public int Id { get; set; }
        public int Valor { get; set; }
    }
    public class IntDTO
    {
        public int? Valor { get; set; }
    }
    public class IdDTO
    {
        public int Id { get; set; }
    }
    public class FechaInicioFinDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
    public class VersionMatriculaDTO
    {
        public int Id { get; set; }
        public int? IdVersion { get; set; }
        public string? Version { get; set; }
    }
    public class VersionMatriculaDisponibleDTO
    {
        public int? Id { get; set; }
        public int? IdVersion { get; set; }
        public string? Nombre { get; set; }
    }
    public class LongDTO
    {
        public long Valor { get; set; }
    }
    public class LongTotalDTO
    {
        public long Total { get; set; }
    }
}
