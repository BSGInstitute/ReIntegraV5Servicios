namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlantillaClaveValorDTO
    {
        public int Id { get; set; }
        public string Clave { get; set; } = null!;
        public string? Valor { get; set; }
        public string? Etiquetas { get; set; }
        public int IdPlantilla { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class PlantillaClaveValorComboDTO
    {
        public int Id { get; set; }
        public int IdPlantilla { get; set; }
        public string? Plantilla { get; set; }
        public string? Clave { get; set; }
        public string? Valor { get; set; }
    }
    public class PlantillaMailingAgendaDTO
    {
        public int IdPlantilla { get; set; }
        public string Nombre { get; set; } = null!;
        public string Valor { get; set; } = null!;
        public int? IdPlantillaClaveValor { get; set; }
    }
    public class PlantillaValorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Valor { get; set; } = null!;
    }
    public class PlantillaClaveValorAreaEtiquetaDTO
    {
        public int IdPlantilla { get; set; }
        public int IdPlantillaClaveValor { get; set; }
        public string Clave { get; set; } = null!;
        public string? Valor { get; set; }
        public int? IdAreaEtiqueta { get; set; }
    }
    public class PlantillaWhatsAppAgendaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int TipoPlantilla { get; set; }
        public string? Contenido { get; set; }
    }
    public class ProblemaCausaDTO
    {
        public int IdProblema { get; set; }
        public string NombreProblema { get; set; } = null!;
        public string? NombreCausa { get; set; }
    }
    public class ContenidoPlantillaDTO
    {
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public int Id { get; set; }
        public int? IdPlantillaClaveValor { get; set; }
        public int? IdAreaEtiqueta { get; set; }
    }

    public class PlantillaValorDetalleDTO
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int IdPlantilla { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string PlantillaBase { get; set; }
    }
}
