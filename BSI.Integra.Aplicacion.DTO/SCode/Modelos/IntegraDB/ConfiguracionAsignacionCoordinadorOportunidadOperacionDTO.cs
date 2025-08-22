namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdCentroCosto { get; set; }
        public int? IdCentroCostoHijo { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public int? IdMigracion { get; set; }
        public bool? Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class ConfiguracionCoordinadorCentroCostoDTO
    {
        public int IdPersonal { get; set; }
        public string UsuarioPersonal { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
    }
    public class ConfiguracionCoordinadoraCentroCostoCantidadDTO
    {
        public int IdPersonal { get; set; }
        public string UsuarioPersonal { get; set; }
        public int IdPespecifico { get; set; }
        public int Cantidad { get; set; }
    }
}
