namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CajaEgresoDTO
    {
        public int Id { get; set; }
        public int? IdCajaPorRendirCabecera { get; set; }
        public int IdCaja { get; set; }
        public int? IdComprobantePago { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string RucProveedor { get; set; }
        public int? IdSunatDocumento { get; set; }
        public string NombreSunatDocumento { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public int? IdFur { get; set; }
        public int? IdFurAnterior { get; set; }
        public string CodigoFur { get; set; }
        public string Descripcion { get; set; }
        public int IdMoneda { get; set; }
        public decimal TotalEfectivo { get; set; }
        public DateTime? FechaEmision { get; set; }
        public int? IdCajaEgresoAprobado { get; set; }
        public bool EsEnviado { get; set; }
        public int? IdPersonalResponsable { get; set; }
        public int? IdPersonalSolicitante { get; set; }
        public string PersonalSolicitante { get; set; }
        public decimal MontoFur { get; set; }
        public decimal MontoPendiente { get; set; }
        public bool EsCancelado { get; set; }
        public string UsuarioModificacion { get; set; }


    }

    public class FiltroCajaEgresoDTO
    {
        public int? idCaja { get; set; }
        public int idPersonalResponsable { get; set; }
        public int? idSolicitante { get; set; }
    }
    public class IdCajaEgresoCanceladoDTO
    {
        public int IdRec { get; set; }
        public bool FurEsCancelado { get; set; }
    }

    public class GenerarRegistroEgresoDTO
    {
        public CajaEgresoAprobadoDTO CajaRECAprobado { get; set; }
        public List<IdCajaEgresoCanceladoDTO> ListaEgresoCancelado { get; set; }
    }

    public class GenerarRegistroEgresoInmediatoDTO
    {
        public CajaEgresoAprobadoDTO CajaEgresoAprobado { get; set; }
        public List<CajaEgresoDTO> ListaRegistroEgreso { get; set; }
    }

    public class CajaEgresoActualizar
    {
        public int Id { get; set; }
        public int? IdFur { get; set; }
        public decimal TotalEfectivo { get; set; }
        public int? IdComprobantePago { get; set; }
        public string UsuarioModificacion { get; set; }
        public string Descripcion { get; set; }
        public int IdMoneda { get; set; }
        public int? IdFurAnterior { get; set; }
    }

    public class CajaEgresoGenerarPdfDTO
    {
        public int IdCajaEgresoAprobado { get; set; }
        public int IdCaja { get; set; }
        public string CodigoCaja { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Ruc { get; set; }
        public string Central { get; set; }
        public string CodigoEgresoCaja { get; set; }
        public string NombreProveedor { get; set; }
        public string RucProveedor { get; set; }
        public string CodigoFur { get; set; }
        public string Comprobantes { get; set; }
        public string FechaEmisionRecibo { get; set; }
        public string EntregadoA { get; set; }
        public string DniEntregadoA { get; set; }
        public string Responsable { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal MontoTotal { get; set; }
        public string Origen { get; set; }
        public string Moneda { get; set; }
        public int IdMoneda { get; set; }
        public string Detalle { get; set; }
        public string Observacion { get; set; }
        public string CodigosPorRendir { get; set; }
        public string TipoDocumentosSunat { get; set; }
        public string FechaGeneracionREC { get; set; }
    }

    public class InsertCajaEgresoDTO
    {
        public int IdCajaPorRendirCabecera { get; set; }
        public int IdCaja { get; set; }
        public int IdComprobantePago { get; set; }
        public int IdFur { get; set; }
        public string Descripcion { get; set; }
        public int IdMoneda { get; set; }
        public decimal TotalEfectivo { get; set; }
        public int? IdPersonalSolicitante { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}

