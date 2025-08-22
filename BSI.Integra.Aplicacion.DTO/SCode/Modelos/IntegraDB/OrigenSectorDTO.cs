namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OrigenSectorDTO
    {
        public int IdProveedorCampaniaIntegra { get; set; }
        public string? Nombre { get; set; }

    }

    public class OrigenSectorContadoDTO
    {
        public int ContadorConfigurado { get; set; }
        public int ContadorNoConfigurado { get; set; }
        public List<OrigenSectorDTO> ListaOrigenSectorConfigurado { get; set; }
        public List<OrigenSectorDTO> ListaOrigenSectorNoConfigurado { get; set; }

    }
    public class OrigenSectorConfiguradoDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? Orden { get; set; }
        public int? CantidadOportunidad { get; set; }
        public int? EsAgrupado { get; set; }
    }

    public class ActualizarDatosDeConfiguracionDTO
    {

        public string? UsuarioModificacion { get; set; }
        public int? idorigendatoCalidad { get; set; }
        public bool? DatosCalidad { get; set; }
        public bool? DatoCalidadWhatsapp { get; set; }
        public bool? DatoCalidadMailing { get; set; }
        public bool? MuyAltaAr { get; set; }
        public bool? MuyAltaAd { get; set; }
        public bool? AltaAd { get; set; }
        public bool? AltaAr { get; set; }
        public bool? MediaAd { get; set; }
        public bool? MediaAr { get; set; }



    }
    public class ActualizarDatosDeConfiguracionAgrupadoDTO
    {

        public int? idOrigenSector { get; set; }
        public string? UsuarioModificacion { get; set; }
        public bool? DatosCalidad { get; set; }
        public bool? MuyAltaAr { get; set; }
        public bool? MuyAltaAd { get; set; }
        public bool? AltaAd { get; set; }
        public bool? AltaAr { get; set; }
        public bool? MediaAd { get; set; }
        public bool? MediaAr { get; set; }



    }
    public class ListaIdCategoriaOrigenDTO
    {

        public int? Id { get; set; }




    }

}