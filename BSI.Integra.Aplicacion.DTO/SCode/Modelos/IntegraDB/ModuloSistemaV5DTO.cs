namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ModuloSistemaV5DTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string IdModuloSistemaGrupo { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
    public class ModuloSistemaDTO
    {
        public int IdModulo { get; set; }
        public string NombreGrupo { get; set; }
        public string NombreModulo { get; set; }
        public string URL { get; set; }
    }
    public class AsignacionModuloDTO
    {
        public int[] IdModulos { get; set; }
        public int IdUsuario { get; set; }
    }
    public class ModuloUrlDTO
    {
        public string NombreModulo { get; set; }
        public string Url { get; set; }
    }
}
