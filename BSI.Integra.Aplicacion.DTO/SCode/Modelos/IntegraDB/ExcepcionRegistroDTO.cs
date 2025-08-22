namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ExcepcionRegistroDTO
    {
        public bool ExcepcionGenerada { get; set; }
        public string? DescripcionGeneral { get; set; }
        public ExcepcionErrorDTO? Error { get; set; }
    }

    public class ExcepcionErrorDTO
    {
        public string? InnerException { get; set; }
        public string? Message { get; set; }
        public string? Source { get; set; }
        public string? Descripcion { get; set; }
    }
}
