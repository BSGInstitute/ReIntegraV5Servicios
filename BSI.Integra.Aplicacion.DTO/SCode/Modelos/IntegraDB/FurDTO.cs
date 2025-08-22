using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FurDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public string Programa { get; set; }
        public int IdCiudad { get; set; }
        public int IdFurTipoPedido { get; set; }
        public int? NumeroSemana { get; set; }
        public int? IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public int? IdProducto { get; set; }
        public string Producto { get; set; }
        public int? IdProductoPresentacion { get; set; }
        public string ProductoPresentacion { get; set; }
        public int? IdMoneda_Proveedor { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Descripcion { get; set; }
        public string NumeroCuenta { get; set; }
        public string Cuenta { get; set; }
        public decimal Cantidad { get; set; }
        public int IdFaseAprobacion1 { get; set; }
        public string FaseAprobacion1 { get; set; }
        public decimal PrecioUnitarioMonedaOrigen { get; set; }
        public decimal PrecioUnitarioDolares { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalDolares { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Observaciones { get; set; }
        public int? IdMonedaPagoReal { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdCondicionTipoPago { get; set; }
        public string MonedaPagoReal { get; set; }
        public int? IdEmpresa { get; set; }
    }

    public class ParametrosFurDTO
    {
        public int IdArea { get; set; }
        public string Codigo { get; set; }
        public int IdRol { get; set; }
        public int IdPersonal { get; set; }
        public int IdEstadoFaseAprobacion1 { get; set; }
        public string Usuario { get; set; }
        public int? IdCiudad { get; set; }
        public int? Anio { get; set; }
        public int? Semana { get; set; }
    }


    public class ParametrosFurGrillaDTO
    {
        public int IdArea { get; set; }
        public string? Codigo { get; set; }
        public int IdRol { get; set; }
        public int IdEstadoFaseAprobacion1 { get; set; }
        public string UserName { get; set; }
        public ModoPersonalFurDTO modo { get; set; }
        public int? anio { get; set; }
        public int? semana { get; set; }
        public int? idCiudad { get; set; }
    }

    public class ProductoFurDTO
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string CuentaContable { get; set; }
        public string Cuenta { get; set; }
        public int IdCantidad { get; set; }
        public string Cantidad { get; set; }
        public int IdMoneda { get; set; }
        public decimal CostoOriginal { get; set; }
        public decimal CostoDolares { get; set; }
        public decimal PrecioProducto { get; set; }
        public int IdCondicionTipoPago { get; set; }
    }

    public class FurPorAprobarDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string CentroCosto { get; set; }
        public string Programa { get; set; }
        public string RazonSocial { get; set; }
        public string Producto { get; set; }
        public int? IdMoneda_Proveedor { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitarioMonedaOrigen { get; set; }
        public decimal PrecioUnitarioDolares { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalDolares { get; set; }
        public string Observaciones { get; set; }
        public int? IdMonedaPagoReal { get; set; }
        public string MonedaPagoReal { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string NombreArea { get; set; }
        public DateTime FechaLimite { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string FurTipoPedido { get; set; }
        public string UsuarioSolicitud { get; set; }
    }

    public class FiltroFurPorAprobar
    {
        public int IdArea { get; set; }
        public string Codigo { get; set; } = null!;
        public int IdRol { get; set; }
        public int tipo { get; set; }
    }

    public class AprobarObservaFurDTO
    {
        public List<int> Ids { get; set; }
        public string? Usuario { get; set; }
        public int IdRol { get; set; }
        public int CheckedIsFurGeneral { get; set; }
        public bool IsAprobar { get; set; }
        public string? Observacion { get; set; }
    }

    public class FurAprobadoNoEjecutadoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string CentroCosto { get; set; }
        public string Programa { get; set; }
        public string Ciudad { get; set; }
        public string TipoPedido { get; set; }
        public string RazonSocial { get; set; }
        public string Producto { get; set; }
        public string ProductoPresentacion { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Descripcion { get; set; }
        public string NumeroCuenta { get; set; }
        public string Cuenta { get; set; }
        public decimal Cantidad { get; set; }
        public string FaseAprobacion1 { get; set; }
        public decimal PrecioUnitarioMonedaOrigen { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public string UsuarioSolicitud { get; set; }
        public string MonedaPagoReal { get; set; }
        public DateTime FechaAprobacionJefeFinanzas { get; set; }
    }

    public class FurCajaPRDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class FurAprobarPoryectadosDTO
    {
        public List<int> ListaIdFur { get; set; }
        public string? Usuario { get; set; }
    }
    public class ProgramaEspecificoFURDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Proveedor { get; set; }
        public string Producto { get; set; }
        public string CentroCosto { get; set; }
        public string Unidades { get; set; }
        public string Descripcion { get; set; }
        public string Ciudad { get; set; }
        public int IdProveedor { get; set; }
        public int IdProducto { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdCiudad { get; set; }
        public int? IdEmpresa { get; set; }
    }
    public class FurProgramaDTO
    {
        public int IdPespecifico { get; set; }
        public int IdHistoricoProductoProveedor { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public decimal Cantidad { get; set; }
        public string Factor { get; set; }
        public int AreaTrabajo { get; set; }
        public int Ciudad { get; set; }
        public int IdEmpresa { get; set; }
    }
    public class FurSesionFiltroDTO
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public string? Factor { get; set; }
        public decimal Cantidad { get; set; }
        public int AreaTrabajo { get; set; }
        public int Semana { get; set; }
        public int Ciudad { get; set; }
        public int IdEmpresa { get; set; }
    }

    public class FurNivelAccesoDTO
    {
        public int IdPersonal { get; set; }
        public int Nivel { get; set; }
    }
    public class AsociarActualizarFurMaterialVersionDTO
    {
        public int IdMaterialPEspecificoDetalle { get; set; }
        public int? IdFur { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdProducto { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Monto { get; set; }
        public string DireccionEntrega { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class FurRegistroMaterialDTO
    {
        public int Id { get; set; }
        public int IdMaterialPEspecificoDetalle { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public decimal Cantidad { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; }
    }

}
