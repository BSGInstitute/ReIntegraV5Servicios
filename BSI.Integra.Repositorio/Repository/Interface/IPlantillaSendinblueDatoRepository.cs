using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaSendinblueDatoRepository : IGenericRepository<TPlantillaSendinblueDato>
    {
        public TPlantillaSendinblueDato Add(PlantillaSendinblueDato entidad);
        public TPlantillaSendinblueDato Update(PlantillaSendinblueDato entidad);
        public bool Delete(int id, string usuario);
        public IEnumerable<TPlantillaSendinblueDato> Add(IEnumerable<PlantillaSendinblueDato> listadoEntidad);
        public IEnumerable<TPlantillaSendinblueDato> Update(IEnumerable<PlantillaSendinblueDato> listadoEntidad);
        public bool Delete(IEnumerable<int> listadoIds, string usuario);

        public List<PlantillaSendinblueDatoDTO> ObtenerDatosPlantilllaPorId(int IdPlantillaSendinblue);
    }
}
