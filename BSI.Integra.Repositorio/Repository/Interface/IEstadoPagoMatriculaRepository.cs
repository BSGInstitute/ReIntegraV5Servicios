using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEstadoPagoMatriculaRepository : IGenericRepository<TEstadoPagoMatricula>
    {
        #region Metodos Base
        TEstadoPagoMatricula Add(EstadoPagoMatricula entidad);
        TEstadoPagoMatricula Update(EstadoPagoMatricula entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEstadoPagoMatricula> Add(IEnumerable<EstadoPagoMatricula> listadoEntidad);
        IEnumerable<TEstadoPagoMatricula> Update(IEnumerable<EstadoPagoMatricula> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);

        #endregion


        public object ObtenerTodoEstadoMatricula();
        List<ComboDTO> ObtenerTodoFiltro();

        public List<ComboDTO> ObtenerEstadoPagoMatriculaDevoluciones();
    }
}
