using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEmbudoNivelRepository : IGenericRepository<TEmbudoNivel>
    {
        #region Metodos Base
        TEmbudoNivel Add(EmbudoNivel entidad);
        TEmbudoNivel Update(EmbudoNivel entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEmbudoNivel> Add(IEnumerable<EmbudoNivel> listadoEntidad);
        IEnumerable<TEmbudoNivel> Update(IEnumerable<EmbudoNivel> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<ComboDTO> ObtenerEmbudoNivel();
    }
}
