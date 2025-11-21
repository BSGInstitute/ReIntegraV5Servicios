namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AsignarAsesorManualParametroDTO
    {
        public AsignarAsesorManualDTO AsignarAsesor { get; set; }
        // public OportunidadLog? OportunidadLogNueva { get; set; }
        public string Usuario { get; set; }
    }

    public class CerrarOportunidadODDTO
    {
        public List<int> IdOportunidades { get; set; }


        public string Usuario { get; set; }
    }


    public class AsignarAsesorManualParametroWhtasapDTO
    {
        public AsignarAsesorManuaWhatsapplDTO AsignarAsesor { get; set; }
        public string Usuario { get; set; }
    }
    public class AsignarAsesorManuaWhatsapplDTO
    {
        public int?[] IdOportunidades { get; set; }
        public int? IdAsesor { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public int? IdCentroCosto { get; set; }
        public string? Comentario { get; set; }
        public bool? SegunMejorPro { get; set; }
        public bool? envioWhats { get; set; }
    }
}


