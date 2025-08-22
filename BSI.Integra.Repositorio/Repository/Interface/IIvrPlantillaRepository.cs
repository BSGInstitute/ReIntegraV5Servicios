using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IIvrPlantillaRepository : IGenericRepository<TIvrPlantilla>
    {
        #region Metodos Base
        TIvrPlantilla Add(IvrPlantilla entidad);
        TIvrPlantilla Update(IvrPlantilla entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TIvrPlantilla> Add(IEnumerable<IvrPlantilla> listadoEntidad);
        IEnumerable<TIvrPlantilla> Update(IEnumerable<IvrPlantilla> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<IvrPlantillaDTO> ObtenerIvrPlantilla();
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
