namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RegistroTokenDTO
    {
        public bool TokenValida { get; set; }
        public string? DescripcionGeneral { get; set; }
        public RegistroClaimTokenDTO RegistroClaimToken { get; set; }
    }

    public class RegistroClaimTokenDTO
    {
        public int IdPersonal { get; set; }
        public int IdRol { get; set; }
        public string AreaTrabajo { get; set; }
        public string UserName { get; set; }
        public string UserAsp { get; set; }
        public string Expira { get; set; }
        public string TipoPersonal { get; set; }
    }
}
