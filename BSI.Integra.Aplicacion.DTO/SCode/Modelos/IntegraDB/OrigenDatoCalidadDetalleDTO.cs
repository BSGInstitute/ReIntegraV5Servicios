namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OrigenDatoCalidadDetalleDTO
    {
        public int IdOrigenDatoCalidad { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public string Nombre { get; set; }
        public bool? DatosCalidad { get; set; }
        public bool? DatoCalidadWhatsapp { get; set; }
        public bool? DatoCalidadMailing { get; set; }
        public bool MuyAltaAr { get; set; }
        public bool MuyAltaAd { get; set; }
        public bool AltaAd { get; set; }
        public bool AltaAr { get; set; }
        public bool MediaAd { get; set; }
        public bool MediaAr { get; set; }

    }
    public class IdOrigenDatoCalidadDetalleDTO
    {
        public int? Id { get; set; }


    }
    public class OrigenDatoCalidadDetalleConfiguracionDTO
    {

        public List<OrigenDatoCalidadDetalleDTO> origenDatoCalidadDetalleIndividual { get; set; }

        public OrigenDatoCalidadDetalleAgrupadoConfigurado? origenDatoCalidadDetalleAgrupado { get; set; } = new OrigenDatoCalidadDetalleAgrupadoConfigurado();


    }
    public class OrigenDatoCalidadDetalleAgrupadoConfigurado
    {

        public NombreCantidadAgrupadoDTO NombreCantidadAgrupadoVarDTO { get; set; } = new NombreCantidadAgrupadoDTO();

        public origenDatoCalidadDetalleConfiguracionAgrupadoDTO listaOrigenesAgrupado { get; set; } = new origenDatoCalidadDetalleConfiguracionAgrupadoDTO();
    }
    public class NombreCantidadAgrupadoDTO
    {
        public string Nombre { get; set; }
        public int CantidadAgrupados { get; set; }

    }
    public class origensectorDTO
    {
        public int IdOrigenSector { get; set; }
    }


    public class origenDatoCalidadDetalleConfiguracionAgrupadoDTO
    {
        public bool? DatosCalidad { get; set; }
        public bool? DatoCalidadWhatsapp {  get; set; }  
        public bool? DatoCalidadMailing {  get ; set; }  
        public bool MuyAltaAd { get; set; }
        public bool MuyAltaAr { get; set; }
        public bool AltaAd { get; set; }
        public bool AltaAr { get; set; }
        public bool MediaAd { get; set; }
        public bool MediaAr { get; set; }
        public bool AgruparCategoriaOrigen { get; set; }

    }

}
