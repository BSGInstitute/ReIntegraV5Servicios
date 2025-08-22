namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CalidadProcesamientoAlternoDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int PerfilCamposLlenos { get; set; }
        public int PerfilCamposTotal { get; set; }
        public bool TieneDni { get; set; }
        public bool SentinelVerificado { get; set; }
        public int PgeneralMotivacionValidado { get; set; }
        public int PgeneralMotivacionTotal { get; set; }
        public int PublicoObjetivoValidado { get; set; }
        public int PublicoObjetivoTotal { get; set; }
        public int PrerequisitoProgramaValidado { get; set; }
        public int PrerequisitoProgramaTotal { get; set; }
        public int RequisitoCertificacionValidado { get; set; }
        public int RequisitoCertificacionTotal { get; set; }
        public int BeneficiosValidados { get; set; }
        public int BeneficiosTotales { get; set; }
        public bool InicioProgramaVerificado { get; set; }
        public bool CompetidoresVerificacion { get; set; }
        public int CantidadCompetidores { get; set; }
        public int ProblemaSeleccionados { get; set; }
        public int ProblemaSolucionados { get; set; }
    }
}
