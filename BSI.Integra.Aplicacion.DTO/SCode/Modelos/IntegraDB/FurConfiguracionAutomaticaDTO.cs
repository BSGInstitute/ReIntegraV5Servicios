namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class FurConfiguracionAutomaticaVersionDetalleDTO
    {
        public int Id { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSede { get; set; }
        public int IdFurTipoPedido { get; set; }
        public int IdHistoricoProductoProveedor { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdMonedaPagoReal { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public int IdFrecuencia { get; set; }
        public int AjusteNumeroSemana { get; set; }
        public DateTime FechaInicioConfiguracion { get; set; }
        public DateTime FechaFinConfiguracion { get; set; }
        public string UsuarioSolicitud { get; set; }
        public string NombreSede { get; set; }
        public string NombreFurTipoPedido { get; set; }
        public string NombrePersonalAreaTrabajo { get; set; }
        public string NombreMonedaPagoReal { get; set; }
        public string RucProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string NombreProducto { get; set; }
        public string NombreFrecuencia { get; set; }
        public string NombreCentroCosto { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public int IdProductoPresentacion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal MontoProyectado { get; set; }
        public string NombreFurTipoSolicitud { get; set; }
        public string NombreEmpresa { get; set; }
        public string UsuarioCreacion { get; set; }
    }


    public class FurConfiguracionAutomaticaFrontDTO
    {
        public int Id { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSede { get; set; }
        public int IdFurTipoPedido { get; set; }
        public int IdHistoricoProductoProveedor { get; set; }
        public int IdCentroCosto { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public int IdFrecuencia { get; set; }
        public int AjusteNumeroSemana { get; set; }
        public DateTime FechaInicioConfiguracion { get; set; }
        public DateTime FechaFinConfiguracion { get; set; }
        public int IdMoneda { get; set; }//IdMonedaPagoReal
        public DateTime FechaSemilla { get; set; }//FechaGeneracionFur
        public string? Usuario { get; set; }
    }
}
