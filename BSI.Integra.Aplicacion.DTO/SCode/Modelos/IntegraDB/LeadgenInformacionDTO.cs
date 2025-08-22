using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{


    public class LeadgenInformacionDTO
    {
        public string created_time { get; set; }
        public string Id { get; set; }
        public string AdsetId { get; set; }
        public string AdId { get; set; }
        public string AdName { get; set; }
        public string AdsetName { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string? AreaFormacion { get; set; }
        public string? Cargo { get; set; }
        public string? AreaTrabajo { get; set; }
        public string? Industria { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public string InicioCapacitacion { get; set; }
        public string CondicionalPregunta1 { get; set; }
        public string CondicionalPregunta2 { get; set; }
        public bool FormularioMultiple { get; set; }
        public bool FormularioRemarketing { get; set; }
        public string CampaignId { get; set; }
        public string NombreCampania { get; set; }
        public string Name { get; set; }
        public string OptimizationGoal { get; set; }
        public string DailyBudget { get; set; }
        public string BudgetRemaining { get; set; }
        public string EffectiveStatus { get; set; }
        public string? StartTime { get; set; }
        public string? AdsetCreatedTime { get; set; }
        public string? UpdatedTime { get; set; }
        public string AdsetStatus { get; set; }
        public int Account { get; set; }
    }


   
    public class FacebookFormularioLeadgenDTO
    {
        public int Id { get; set; }
        public string IdLeadgenFacebook { get; set; }
        public DateTime FechaCreacionFacebook { get; set; }
        public string IdCampanhaFacebook { get; set; }
        public string NombreCampaniaFacebook { get; set; }
        public string FacebookAnuncioId { get; set; }
        public string FacebookAnuncioNombre { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string AreaFormacion { get; set; }
        public string Cargo { get; set; }
        public string AreaTrabajo { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public bool EsProcesado { get; set; }
        public string Industria { get; set; }
        public string InicioCapacitacion { get; set; }
        public string Excepcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
       
    }


}
