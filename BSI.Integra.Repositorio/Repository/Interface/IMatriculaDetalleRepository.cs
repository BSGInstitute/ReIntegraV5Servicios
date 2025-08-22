using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMatriculaDetalleRepository : IGenericRepository<TMatriculaDetalle>
    {
        #region Metodos Base
        TMatriculaDetalle Add(MatriculaDetalle entidad);
        TMatriculaDetalle Update(MatriculaDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMatriculaDetalle> Add(IEnumerable<MatriculaDetalle> listadoEntidad);
        IEnumerable<TMatriculaDetalle> Update(IEnumerable<MatriculaDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}
