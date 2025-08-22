namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TipoComprobanteDTO
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        public string Nombre { get; set; } = null!;
        public string Usuario { get; set; }
    }
}

