namespace BSI.Integra.Aplicacion.DTO.Modelos.Wolkbox
{
    public class WolkboxTokenDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string Token { get; set; }
        public string AgentId { get; set; }
        public string WolkvoxServer { get; set; }
        public bool Activo { get; set; }
        public int Limite { get; set; }
        public int ContadorDia { get; set; }
    }
}
