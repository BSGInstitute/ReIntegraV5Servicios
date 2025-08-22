namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.FacebookLeadsRecuperacionDatos
{
    public class FacebookLeadsRecuperacionDatosResponseDTO
    {
        public string LeadId { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public FacebookFormularioDTO Formulario { get; set; }
        public FacebookCampaniaDTO Campania { get; set; }
        public FacebookConjuntoAnuncioDTO ConjuntoAnuncio { get; set; }
        public FacebookAnuncioDTO Anuncio { get; set; }
    }

    public class FacebookFormularioDTO
    {
        public string Nombre { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Movil { get; set; } = "";
        public string Pais { get; set; } = "";
        public string Ciudad { get; set; } = "";
        public string AreaFormacion { get; set; } = "";
        public string AreaTrabajo { get; set; } = "";
        public string Cargo { get; set; } = "";
        public string Industria { get; set; } = "";
    }

    public class FacebookCampaniaDTO
    {
        public string Nombre { get; set; } = "";
        public string Estado { get; set; } = "";
        public string Objetivo { get; set; } = "";
    }

    public class FacebookConjuntoAnuncioDTO
    {
        public string Nombre { get; set; } = "";
        public string Estado { get; set; } = "";
    }

    public class FacebookAnuncioDTO
    {
        public string Nombre { get; set; } = "";
        public string Estado { get; set; } = "";
    }
}