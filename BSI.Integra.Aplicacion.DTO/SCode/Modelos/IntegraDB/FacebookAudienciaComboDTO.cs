namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FacebookAudienciaComboDTO
    {
        public int Id { get; set; }
        public string FacebookIdAudiencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class FiltroSegmentoRemarketingCombosDTO
    {
        public List<FacebookAudienciaComboDTO> ListaFacebookAudiencia { get; set; }
        public List<FacebookCuentaPublicitariaDTO> ListaFacebookCuentaPublicitaria { get; set; }
    }
}
