using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProveedorRepository : IGenericRepository<TProveedor>
    {
        #region Metodos Base
        TProveedor Add(Proveedor entidad);
        TProveedor Update(Proveedor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProveedor> Add(IEnumerable<Proveedor> listadoEntidad);
        IEnumerable<TProveedor> Update(IEnumerable<Proveedor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Proveedor? ObtenerPorId(int id);
        IEnumerable<ProveedorComboDTO> ObtenerProveedorPorProducto(int idProducto);
        IEnumerable<ProveedorComboDTO> ObtenerNombreProveedorAutocomplete(string valor);
        IEnumerable<ProveedorRucRazonSocialDTO> ObtenerProveedorPorRuc(string valor);
        string ObtenerNombreProveedorPorId(int Id);
        IEnumerable<FiltroRucProveedorDTO> ObtenerProveedorRucAutocomplete();
        IEnumerable<ProveedorDTO> ObtenerTodoProveedorById(int? Id);
        IEnumerable<ProveedorComboDTO> ObtenerProveedorCombo(string Texto);
        IEnumerable<ProveedorComboDTO> ObtenerProveedoresConEstadoCuentaPagadoPendiente();
        int? ObtenerProveedorEliminadoEmailRepetido(string email);
        bool ActivarProveedor(int Id);
        IEnumerable<ComboDTO> ObtenerNombreProveedorParaHonorario();
        string ObtenerEmail(int id);
        IEnumerable<ComboDTO> ObtenerTodoCoordinadoresDocentes();
        ProveedorDTO ObtenerProveedorPorId(int idProveedor);
        IEnumerable<ProveedorProductoDTO> ObtenerInformacionProductoProveedor();
        Task<IEnumerable<ProveedorProductoDTO>> ObtenerInformacionProductoProveedorAsync();
        IEnumerable<ComboDTO> ObtenerProveedorFiltro();
        Task<IEnumerable<ComboDTO>> ObtenerProveedorFiltroAsync();
        List<FiltroConvocatoriaPersonalDTO> ObtenerProveedoresConvocatoriaPersonal();
        IEnumerable<ComboDTO> ObtenerListaDocentes();
        public int? ObtenerProveedorEmailRepetido(string email);
        List<RespuestaReporteRevisionDocenteDTO> GenerarReporteRevisionForo(string condicion);
        List<RespuestaReporteRevisionDocenteDTO> GenerarReporteProyecto(string condicion);
        public PersonalAsignadoDocenteDTO PersonalAsignadoDocente(int Id);

        IEnumerable<ProveedorComboDTO> ObtenerProveedoresPaginasReclutadoras();
        ProveedorComboDTO ObtenerProveedoresPaginasReclutadorasPorId(int id);
    }
}
