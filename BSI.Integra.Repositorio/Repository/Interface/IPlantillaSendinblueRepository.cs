using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaSendinblueRepository : IGenericRepository<TPlantillaSendinblue>
    {
        public TPlantillaSendinblue Add(PlantillaSendinblue entidad);
        public TPlantillaSendinblue Update(PlantillaSendinblue entidad);
        public bool Delete(int id, string usuario);
        public IEnumerable<TPlantillaSendinblue> Add(IEnumerable<PlantillaSendinblue> listadoEntidad);
        public IEnumerable<TPlantillaSendinblue> Update(IEnumerable<PlantillaSendinblue> listadoEntidad);
        public bool Delete(IEnumerable<int> listadoIds, string usuario);

        public PlantillaSendinblueDTO ObtenerPlantilllaPorId(int id);
        public List<PlantillaSendinblueDTO> ObtenerTodo();



    }
}
