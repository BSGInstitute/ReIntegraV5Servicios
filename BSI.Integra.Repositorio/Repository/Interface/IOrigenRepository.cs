using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOrigenRepository : IGenericRepository<TOrigen>
    {
        #region Metodos Base
        TOrigen Add(Origen entidad);
        TOrigen Update(Origen entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOrigen> Add(IEnumerable<Origen> listadoEntidad);
        IEnumerable<TOrigen> Update(IEnumerable<Origen> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<OrigenDTO> ObtenerOrigen();
        List<TarifarioDetalleAgendaDTO> ObtenerTarifariosDetallesAgenda(int idMatriculaCabecera);
        List<VersionprogramaDTO> obtenerversionAlumno(int idMatriculaCabecera);
        OrigenIdCategoriaOrigenDTO ObtenerIdCategoriaOrigenPorOrigen(int idOrigen);
        List<ComboFiltroDTO> ObtenerOrigeneParaRegistrarOportunidad(string Area);
        List<ComboFiltroDTO> ObtenerOrigenPorCategoriaOrigen(int idCategoriaOrigenInbox, int idCategoriaOrigenCorreo, int idCategoriaOrigenComentarios);
        List<ComboFiltroDTO> ObtenerOrigenChat(string nombre);
        public IEnumerable<ComboDTO> ObtenerTodoFiltro();

        List<TarifarioDTO> ObtenerTarifarios();
        List<TarifarioDetalleConfiguracionDTO> ObtenerTarifariosDetalles(int idTarifario);
        List<TarifarioDTO> InsertarTarifario(TarifarioNuevoDTO objeto);
        List<TarifarioDTO> ActualizarTarifario(TarifarioNuevoDTO objeto);
        List<TarifarioDTO> EliminarTarifario(int idTarifario, string usuario);
        public List<OrigenesCategoriaOrigenDTO> ObtenerOrigenesCategoriasOrigen();
        List<TarifarioDetalleDTO> EliminarTarifarioDetallePais(int id, string usuario);
        List<TarifarioDetalleDTO> EliminarTarifarioDetalle(string concepto, string usuario);
        List<ComboFiltroDTO> ObtenerCombosOrigen();
        public List<TasasAcademicasDetalleDTO> AgregarTasasAcademicasProcedimiento(string CodigoMatricula, int IdConcepto, float Monto, string Moneda, string Usuario, DateTime Fecha);
        public List<TarifarioDetalleMontoDTO> ObtenerTarifariosDetallesMonto(string nombre);

        int ObtenerIdPorNombre(string nombre);
    }
}
