namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CalidadProcesamientoDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int PerfilCamposLlenos { get; set; }
        public int PerfilCamposTotal { get; set; }
        public bool Dni { get; set; }
        public int PgeneralValidados { get; set; }
        public int PgeneralTotal { get; set; }
        public int PespecificoValidados { get; set; }
        public int PespecificoTotal { get; set; }
        public int BeneficiosValidados { get; set; }
        public int BeneficiosTotales { get; set; }
        public bool CompetidoresVerificacion { get; set; }
        public int ProblemaSeleccionados { get; set; }
        public int ProblemaSolucionados { get; set; }
    }
}
