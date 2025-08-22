namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AspNetUserAutenticateDTO
    {
        public string Id { get; set; } = null!;
        public int IdPersonal { get; set; }
        public int IdRol { get; set; }
        public string? AreaTrabajo { get; set; }
        public string UserName { get; set; } = null!;
        public string Token { get; set; } = null!;
        public ExcepcionRegistroDTO Excepcion { get; set; } = null!;
        public string TipoPersonal { get; set; } = null!;
    }

    public class AspNetUserTokenDTO
    {
        public string Token { get; set; } = null!;
        public ExcepcionRegistroDTO Excepcion { get; set; } = null!;
    }
}
