using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPlantillaMaestroPwRepository : IGenericRepository<TPlantillaMaestroPw>
    {
        #region Metodos Base
        TPlantillaMaestroPw Add(PlantillaMaestroPw entidad);
        TPlantillaMaestroPw Update(PlantillaMaestroPw entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPlantillaMaestroPw> Add(IEnumerable<PlantillaMaestroPw> listadoEntidad);
        IEnumerable<TPlantillaMaestroPw> Update(IEnumerable<PlantillaMaestroPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlantillaMaestroPwDTO> ObtenerCombo();
    }
}
