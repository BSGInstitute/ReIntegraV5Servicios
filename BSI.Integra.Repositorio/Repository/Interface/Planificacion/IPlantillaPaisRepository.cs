using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPlantillaPaisRepository : IGenericRepository<TPlantillaPai>
    {
        #region Metodos Base
        TPlantillaPai Add(PlantillaPais entidad);
        TPlantillaPai Update(PlantillaPais entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPlantillaPai> Add(IEnumerable<PlantillaPais> listadoEntidad);
        IEnumerable<TPlantillaPai> Update(IEnumerable<PlantillaPais> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlantillaPais> ObtenerPorIdPlantillaPw(int idPlantillaPw);
        PlantillaPais ObtenerPorIdPaisYIdPlantillaPw(int idPais, int idPlantillaPw);
    }
}
