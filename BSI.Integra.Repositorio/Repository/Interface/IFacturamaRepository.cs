using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFacturamaRepository
    {

        public FacturamaCredencialesDTO ObtenerCredencialesActivas();
        public List<RegimenFiscalDTO> ObtenerListaRegimenFiscal();
        public List<UsoCfdiDTO> ObtenerListaUsoCfdi();
        public List<FormapagoFacturamaDTO> ObtenerFormapagoFacturama();
        public bool ActualizaEnviadoFacturama(int id, String UsuarioModificacion);
        public string InsertarRegimenFiscal(string clave, string descripcion, string usuario);
        public bool ActualizarRegimenFiscal(int id,string clave, string descripcion, string usuario);
        public bool EliminarRegimenFiscal(int id, string usuario);
        public string InsertarUsoComprobante(string clave, string descripcion, string usuario);
        public bool ActualizarUsoComprobante(int id, string clave, string descripcion, string usuario);
        public bool EliminarUsoComprobante(int id, string usuario);
        public List<ResumenMatriculaDTO> ObtenerResumenMatriculas(FiltroFechaDTO filtro);
        public int InsertarCliente(FacturamaClienteDTO cliente, string usuario);
        public void InsertarDireccionCliente(FacturamaAddressDTO direccion, int idCliente, string usuario);
        public int InsertarFactura(FacturamaFacturaDTO factura, int idCliente, string codigoMatricula,int idCronogramaPagoDetalleFinal, string usuario);
        public void InsertarImpuestoItem(FacturamaTaxDTO tax, int idItem, string usuario);
        public int InsertarItemFactura(FacturamaItemDTO item, int idFactura, string usuario);

        public FacturamaFacturaClienteDTO ObtenerDatosFacturaClientePorId(int idFactura);
        public void ActualizarFacturaComoEnviada(int idFactura, string uuid, string cfdiId, DateTime? fechaEnvio, string usuario);
        public FacturamaFacturaClienteCronogrmaDTO ObtenerFacturaClientePorCodigoMatricula(string codigoMatricula);
        public int ObtenerIdFacturaPorCodigoMatricula(string codigoMatricula);
        public List<FacturamaFacturaMasivoDTO> ObtenerFacturasPendientesEnvio();
        public int ObtenerIdCronogramaPorIdFactura(int idFactura);
        public FacturamaFacturaCronogramaDetalleDTO ObtenerDetalleFacturaFacturamaCronograma(int IdFacturamaFactura);
        public int ObtenerIdAlumnoPorIdCronograma(int idCronograma);
        public void InsertarActualizarFacturamaAlumno(int idAlumno, FacturamaClienteDTO cliente, FacturamaFacturaDTO factura, string usuario);
        public bool ExisteFacturaConfigurada(int idCronogramaPagoDetalleFinal);
        public bool EliminarFacturasPendientesFacturama(List<int> idsFacturas, string usuario);

    }
}
