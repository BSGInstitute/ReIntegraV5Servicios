using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IExperienciaRepository : IGenericRepository<TExperiencium>
    {
        #region Metodos Base
        TExperiencium Add(Experiencia entidad);
        TExperiencium Update(Experiencia entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TExperiencium> Add(IEnumerable<Experiencia> listadoEntidad);
        IEnumerable<TExperiencium> Update(IEnumerable<Experiencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ExperienciaComboDTO> ObtenerCombo();
    }
}
