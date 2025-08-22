using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IExcepcionFrecuenciaPwRepository : IGenericRepository<TExcepcionFrecuenciaPw>
    {
        #region Metodos Base
        TExcepcionFrecuenciaPw Add(ExcepcionFrecuenciaPw entidad);
        TExcepcionFrecuenciaPw Update(ExcepcionFrecuenciaPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TExcepcionFrecuenciaPw> Add(IEnumerable<ExcepcionFrecuenciaPw> listadoEntidad);
        IEnumerable<TExcepcionFrecuenciaPw> Update(IEnumerable<ExcepcionFrecuenciaPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ExcepcionFrecuenciaPwDTO> ObtenerExcepcionFrecuenciaPw();
        IEnumerable<ExcepcionFrecuenciaPGeneralDTO> ObtenerTodoProgramaGeneral();
    }
}