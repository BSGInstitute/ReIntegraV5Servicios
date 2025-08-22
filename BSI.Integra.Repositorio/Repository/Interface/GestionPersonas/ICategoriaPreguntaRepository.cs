using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ICategoriaPreguntaRepository : IGenericRepository<TPreguntaCategorium>
    {
        #region Metodos Base
        TPreguntaCategorium Add(CategoriaPregunta entidad);
        TPreguntaCategorium Update(CategoriaPregunta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPreguntaCategorium> Add(IEnumerable<CategoriaPregunta> listadoEntidad);
        IEnumerable<TPreguntaCategorium> Update(IEnumerable<CategoriaPregunta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CategoriaPreguntaDTO> Obtener();
        CategoriaPregunta? ObtenerPorId(int id);
    }
}
