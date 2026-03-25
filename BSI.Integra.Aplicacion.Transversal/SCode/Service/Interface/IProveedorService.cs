using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IProveedorService
    {
        #region Metodos Base
        Proveedor Add(Proveedor entidad);
        Proveedor Update(Proveedor entidad);
        bool Delete(int id, string usuario);

        List<Proveedor> Add(List<Proveedor> listadoEntidad);
        List<Proveedor> Update(List<Proveedor> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProveedorComboDTO> ObtenerProveedorPorProducto(int idProducto);
        IEnumerable<ProveedorRucRazonSocialDTO> ObtenerProveedorPorRuc(string valor);
        string ObtenerNombreProveedorPorId(int Id);
        IEnumerable<FiltroRucProveedorDTO> ObtenerProveedorRucAutocomplete();
        IEnumerable<ProveedorDTO> ObtenerTodoProveedorById(int? Id);
        IEnumerable<ProveedorComboDTO> ObtenerProveedorCombo(string Texto);
        IEnumerable<ProveedorComboDTO> ObtenerProveedoresConEstadoCuentaPagadoPendiente();
        int? ObtenerProveedorEliminadoEmailRepetido(string email);
        bool ActivarProveedor(int Id);
        IEnumerable<ComboDTO> ObtenerNombreProveedorParaHonorario();
        int InsertarProveedorCuentaBanco(ProveedorWEnvioDTO proveedor, List<ProveedorCuentaBancoEnvioDTO> listaCuentaBanco);
        bool ActualizarProveedorCuentaBanco(ProveedorWEnvioDTO proveedor, List<ProveedorCuentaBancoEnvioDTO> listaCuentaBanco);
        object ObtenerNombreProveedor();
        IEnumerable<ProveedorComboDTO> ObtenerNombreProveedorAutocomplete();
        bool EliminarProveedor(int Id, string Usuario);
        bool ValidarExistenciaProveedor(CadenaStringDTO DocidentidadEmail);
        object ObtenerImpuestosProveedor();
        IEnumerable<ProveedorDTO> ObtenerProveedorGrilla(int? IdProveedor);
        IEnumerable<DTO.ComboDTO> ObtenerTodoCoordinadoresDocentes();
        ProveedorDTO ObtenerProveedorPorId(int idProveedor);
        List<FiltroConvocatoriaPersonalDTO> ObtenerProveedoresConvocatoriaPersonal();
        IEnumerable<ComboDTO> ObtenerListaDocentes();
        List<RespuestaReporteRevisionDocenteDTO> GenerarReporte(ReporteRevisionDocenteDTO Filtro);

        IEnumerable<ProveedorComboDTO> ObtenerProveedoresPaginasReclutadoras();
        IEnumerable<ProveedorDocenteDTO> ObtenerDocentesActivos();
    }
}
