namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TipoContratoComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Idpais { get; set; }
    }

    public class TipoContratoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Comentario { get; set; }
        public int IdPais { get; set; }
    }
}
