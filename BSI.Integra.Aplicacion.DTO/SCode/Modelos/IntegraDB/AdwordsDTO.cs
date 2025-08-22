namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AdwordsDTO
    {
    }

    public class GoogleFormularioLeadgenDTO
    {
        public string? Lead { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }
        public string? Pais { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Cargo { get; set; }
        public string? AreaTrabajo { get; set; }
        public string? AreaFormacion { get; set; }
        public string? Industria { get; set; }
        public string? VersionApi { get; set; }
        public string? FormularioGoogle { get; set; }
        public string? CampaniaGoogle { get; set; }
        public string? KeyGoogle { get; set; }
        public bool? EsTest { get; set; }
        public string? Gcl { get; set; }
        public string? GrupoAds { get; set; }
        public string? CreativoAds { get; set; }
        public bool? Estado { get; set; }
        public string? Usuario { get; set; }
        public DateTime? Fecha { get; set; }
    }

    public class CampaniaAdwordsDTO
    {
        public string CampaniaGoogleId { get; set; }
        public string NombreCampania { get; set; }
        public string NombreFormulario { get; set; }
        public string ClaveFormulario { get; set; }
        public int IdCentroCosto { get; set; }
        public bool EsRemarketing { get; set; }
        public bool Estado { get; set; }
        public string Usuario { get; set; }
    }


    public class CampaniaAdwordsTodoDTO
    {
        public int Id { get; set; }
        public string CampaniaGoogleId { get; set; }
        public string NombreCampania { get; set; }
        public string NombreFormulario { get; set; }
        public string ClaveFormulario { get; set; }
        public int IdCentroCosto { get; set; }
        public bool EsRemarketing { get; set; }
    }

    public class ActualzarCampaniaAdwordsDTO
    {
        public int Id { get; set; }
        public string CampaniaGoogleId { get; set; }
        public string NombreCampania { get; set; }
        public string NombreFormulario { get; set; }
        public string ClaveFormulario { get; set; }
        public int IdCentroCosto { get; set; }
        public bool EsRemarketing { get; set; }
        public string Usuario { get; set; }
    }


}
