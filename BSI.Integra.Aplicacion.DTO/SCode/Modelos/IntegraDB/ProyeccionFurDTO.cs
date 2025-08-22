using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProyeccionFurDTO
    {
        public string IdAreas { get; set; }
        public int IdConfiguracionProyeccion { get; set; }
        //public List<CabeceraSolicitudDTO> CabeceraSolicitud { get; set; }
    }

    public class CabeceraSolicitudDTO
    {
        public int IdCabeceraSolicitud { get; set; }
        public int IdArea { get; set; }
    }

    public class CongelamientoProyeccionFurDTO
    {
        public int IdArea { get; set; }
        public int IdCabeceraSolicitud { get; set; }
        public ConfiguracionProyeccionFur ConfiguracionProyeccionFur { get; set; }
        public List<FurConfiguracionAutomaticaVersionDetalleDTO> DetalleCabeceraProyeccionFur { get; set; }
    }

    public class LogProyectadosDTO
    {
        public string  TipoProyectado { get; set; }
        public int TotalProcesado { get; set; }
        public int TotalProyectados { get; set; }
        public int TotalErrores { get; set; }
        public DateTime FechaSemilla { get; set; }
        public DateTime FechaInicioProyeccion { get; set; }
        public DateTime FechaFinProyeccion { get; set; }
        public string ConfiguracionesConError { get; set; }
    }

    public class ErrorFurDTO
    {
        public FurConfiguracionAutomaticaVersionDetalleDTO Configuracion { get; set; }
        public string MensajeError { get; set; }
    }


    public class FurParaProyeccionDTO
    {
        public string? Codigo { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int IdCiudad { get; set; }
        public string? NumeroFur { get; set; }
        public int? NumeroSemana { get; set; }
        public string? UsuarioSolicitud { get; set; }
        public string? UsuarioAutoriza { get; set; }
        public string? Observaciones { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal? Monto { get; set; }
        public int? IdProductoPresentacion { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdMonedaProveedor { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string? NumeroRecibo { get; set; }
        public decimal? PagoMonedaOrigen { get; set; }
        public decimal? PagoDolares { get; set; }
        public DateTime? FechaCobroBanco { get; set; }
        public string? ResponsableCobro { get; set; }
        public DateTime? FechaPago { get; set; }
        public string Cuenta { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal PrecioUnitarioMonedaOrigen { get; set; }
        public decimal PrecioUnitarioDolares { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalDolares { get; set; }
        public int IdFurFaseAprobacion1 { get; set; }
        public int? AprobadoFase2 { get; set; }
        public string FechaLimite { get; set; }
        public int IdFurTipoPedido { get; set; }
        public bool Cancelado { get; set; }
        public int? Antiguo { get; set; }
        public int? IdMonedaPagoReal { get; set; }
        public bool? OcupadoSolicitud { get; set; }
        public bool? OcupadoRendicion { get; set; }
        public bool EstadoAprobadoObservado { get; set; }
        public int? IdMonedaPagoRealizado { get; set; }
        public DateTime? FechaAprobacionProcesoCulminado { get; set; }
        public decimal? MontoProyectado { get; set; }
        public bool? EsDiferido { get; set; }
        public int? IdFurSubFaseAprobacion { get; set; }
        public DateTime? FechaLimiteReprogramacion { get; set; }
        public int? IdEmpresa { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    public class ParametrosEnvioDTO
    {
        public string IdAreas { get; set; }
        public string IdSeleccion { get; set; }
    }
    public class CambioDeEstadoDTO
    {
        public int IdCabecera { get; set; }
        public string idSeleccion { get; set; }
    }

    public class EliminarFurProyectadoDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string IdsProducto { get; set; }
        public string IdsProveedor { get; set; }
        public string? Usuario { get; set; }

    }

}
