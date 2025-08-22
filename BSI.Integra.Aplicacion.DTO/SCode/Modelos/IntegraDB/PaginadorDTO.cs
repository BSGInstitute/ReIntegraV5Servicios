namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public partial class PaginadorDTO
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        //Adicional
        public string? identificador { get; set; }
    }
}
