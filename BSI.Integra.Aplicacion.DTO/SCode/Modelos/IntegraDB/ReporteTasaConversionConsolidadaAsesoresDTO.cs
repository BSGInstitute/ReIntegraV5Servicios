namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteTasaConversionConsolidadaAsesoresDTO
    {
        public List<TCRM_ConsolidadTCAsesores> Consolidado { get; set; }
        public List<TCRM_TasaConversionPorCategoriaDatoPaisDTO> Desagregado { get; set; }
    }
    public class ReporteTasaConversionConsolidadaAsesoresAlternoDTO
    {
        public List<TCRM_ConsolidadTCAsesores> Consolidado { get; set; }
        public List<TCRM_TasaConversionPorCategoriaDatoPaisAlternoDTO> Desagregado { get; set; }
    }
    public class TCRM_CambioDeFaseDTO
    {
        public int? IdAsesor { get; set; }
        public int? IdCodigoPais { get; set; }
        public decimal? IngresoReal { get; set; }
        public decimal? IngresoMes { get; set; }
        public decimal? PrecioSinDesc { get; set; }
        public decimal? PrecioListaFinal { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? DescuentoPromedio { get; set; }
        public decimal? PrecioPromedio { get; set; }
        public int? OportunidadesOCAnyIS { get; set; }
        public int? OportunidadesOCTotal { get; set; }
        public decimal? Meta { get; set; }
        public bool EstadoAsesor { get; set; }
        public string Nombres { get; set; }
        public decimal? TCReal { get; set; }
        public decimal? TCMeta { get; set; }
        public decimal? TCReal_TCMeta { get; set; }
        public decimal? PP_IM_USD { get; set; }
        public decimal? PP_OC_USD { get; set; }
        public decimal? PorcentajeIngresoMes { get; set; }
        public decimal? IngresoMeta { get; set; }
        public decimal? IR_IM { get; set; }
    }
    public class TCRM_CambioDeFasePredictivoDTO
    {
        public int? IdAsesor { get; set; }
        public int? IdCodigoPais { get; set; }
        public int? OportunidadesOCAnyIS { get; set; }
        public int? OportunidadesOCTotal { get; set; }
        public decimal? Meta { get; set; }
        public bool EstadoAsesor { get; set; }
        public string Nombres { get; set; }
        public decimal? TCReal { get; set; }
        public decimal? TCMeta { get; set; }
        public decimal? TCReal_TCMeta { get; set; }
    }
    public class TCRM_ConsolidadTCAsesores
    {
        public int orden { get; set; }
        public int idSub { get; set; }
        public string nombreSub { get; set; }
        public int idcategoriaOrigen { get; set; }
        public int idCoordinador { get; set; }
        public string nombreCoordinador { get; set; }
        public int idasesor { get; set; }
        public string ca_nombre { get; set; }
        public int pais { get; set; }
        public string nombrePais { get; set; }
        public string probabilidad { get; set; }
        public string nombre { get; set; }
        public int grupo { get; set; }
        public string nombreGrupo { get; set; }
        public int inscritosMatriculados { get; set; }
        public int oportunidadesCerradas { get; set; }
        public decimal tasaConversion { get; set; }
        public int ordenp { get; set; }
        public string probabilidadDesc { get; set; }
        public int tcMeta { get; set; }
        public int centroCosto { get; set; }
        public string nombreCentroCosto { get; set; }
    }
    public class TCRM_TasaConversionPorCategoriaDatoPaisDTO
    {
        public int Orden { get; set; }
        public string ProbabilidadDesc { get; set; }
        public string Grupo { get; set; }
        public string NombreGrupo { get; set; }
        public string Pais { get; set; }
        public string NombrePais { get; set; }
        public int TCMeta { get; set; }
        public List<TCRM_ConsolidadTCAsesores> ListaMuyAlta { get; set; }
    }
    public class TCRM_TasaConversionPorCategoriaDatoPaisAlternoDTO
    {
        public int orden { get; set; }
        public string probabilidadDesc { get; set; }
        public string grupo { get; set; }
        public string nombreGrupo { get; set; }
        public string pais { get; set; }
        public string nombrePais { get; set; }
        public int tcMeta { get; set; }
        public List<TCRM_ConsolidadTCAsesores> listaMuyAlta { get; set; }
    }
    public class TCRM_CentroCostoPorAsesorDTO
    {
        public int idasesor { get; set; }
        public double precioPromedio { get; set; }
        public double ingresoReal { get; set; }
        public double ingresoMes { get; set; }
        public double DescuentoPromedio { get; set; }
        public int oportunidadesOCAnyIS { get; set; }
        public int oportunidadesOCTotal { get; set; }
        public bool estadoAsesor { get; set; }
        public double precioListaFinal { get; set; }//nuevos valores solo para calcular valor
        public int idcodigopais { get; set; }
        public double Descuento { get; set; }//nuevos valores solo para calcular valor

    }
}
