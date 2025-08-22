namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FacebookFormularioWebhookLeadgenDTO
    {
        public string FacebookIdLeadgen { get; set; }
        public string FacebookIdCampania { get; set; }
        public string FacebookIdFormulario { get; set; }
        public string FacebookFechaUnix { get; set; }
        public string FacebookIdPagina { get; set; }
        public string FacebookIdGrupo { get; set; }
        public bool EsProcesado { get; set; }
        public DateTime FacebookFechaLead { get; set; }
    }

    public class FacebookFormularioWebHookLeadgenDTO
    {
        public int IdFacebookFormularioWebHookLeadgen { get; set; }
    }

    public class FacebookRangoFechaDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
