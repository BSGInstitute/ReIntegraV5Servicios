namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CajaPorRendirDTO
    {
        public int Id { get; set; }
        public int? IdCaja { get; set; }
        public string CodigoCaja { get; set; }
        public int? IdFur { get; set; }
        public string CodigoFur { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public string NombrePersonalSolicitante { get; set; }
        public int IdPersonalResponsable { get; set; }
        public string NombrePersonalResponsable { get; set; }
        public string Descripcion { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public decimal TotalEfectivo { get; set; }
        public DateTime FechaEntregaEfectivo { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    public class CajaPorRendirFiltroDTO
    {
        public int idPersonalResponsable { get; set; }
        public int? idMonedaCaja { get; set; }
        public int? idPersonalSolicitante { get; set; }
    }
    public class CajaPorRendirDevolerDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = null!;
    }

    public class CajaPorRendirCombosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class MontoCajaDTO
    {
        public decimal NotaIngresoCaja { get; set; }
        public decimal ReciboEgresoCaja { get; set; }
        public decimal PorRendir { get; set; }
        public decimal SaldoCaja { get; set; }
    }
    public class GenerarPorRendirDTO
    {
        public CajaPorRendirCabeceraDTO CajaPRCabecera { get; set; }
        public List<int> ListaIdPorRendir { get; set; }
    }
    public class GenerarPorRendirInmediatoDTO
    {
        public CajaPorRendirCabeceraDTO CajaPRCabecera { get; set; }
        public List<CajaPorRendirDTO> ListaPorRendir { get; set; }
    }


    public class CajaPorRendirGenerarPdfDTO
    {
        public int IdPorRendirCabecera { get; set; }
        public int IdCaja { get; set; }
        public string CodigoCaja { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Ruc { get; set; }
        public string Central { get; set; }
        public string PersonalResponsable { get; set; }
        public string CuentaCaja { get; set; }
        public string Moneda { get; set; }
        public int IdMoneda { get; set; }
        public string CodigoPorRendir { get; set; }
        public string CodigoFur { get; set; }
        public string FechaAprobacion { get; set; }
        public string EntregadoA { get; set; }
        public decimal MontoTotal { get; set; }
        public string Detalle { get; set; }
        public string Observacion { get; set; }
        public int TotalReciboEgreso { get; set; }
        public decimal MontoDevolucion { get; set; }
        public decimal MontoPendienteRendicion { get; set; }
        public string FechasRendicion { get; set; }
        public string DniSolicitante { get; set; }
        public string CodigoCajaEgreso { get; set; }
    }

    public class CajaPorRendirCabeceraRendicionDTO
    {
        public int IdCajaPorRendirCabecera { get; set; }
        public decimal MontoSolicitado { get; set; }
        public decimal MontoPendiente { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public int NumeroSolicitudes { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CodigoPR { get; set; }
        public int IdCaja { get; set; }
        public string CodigoCaja { get; set; }
    }

    public class DatosSolicitudDTO
    {
        public string Descripcion { get; set; }
        public DateTime FechaEntregaEfectivo { get; set; }
        public int Id { get; set; }
        public int IdFur { get; set; }
        public int IdMoneda { get; set; }
        public int IdPersonalResponsable { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public decimal TotalEfectivo { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    public class EsEnviadoSolicitudDTO
    {
        public List<int> listaIds { get; set; }
        public string Usuario { get; set; }
    }
}
