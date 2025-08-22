using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaSendinblueEtiquetaPlantillaRepository : IGenericRepository<TPlantillaSendinblueEtiquetaPlantilla>
    {
        public TPlantillaSendinblueEtiquetaPlantilla Add(PlantillaSendinblueEtiquetaPlantilla entidad);
        public TPlantillaSendinblueEtiquetaPlantilla Update(PlantillaSendinblueEtiquetaPlantilla entidad);
        public bool Delete(int id, string usuario);
        public IEnumerable<TPlantillaSendinblueEtiquetaPlantilla> Add(IEnumerable<PlantillaSendinblueEtiquetaPlantilla> listadoEntidad);
        public IEnumerable<TPlantillaSendinblueEtiquetaPlantilla> Update(IEnumerable<PlantillaSendinblueEtiquetaPlantilla> listadoEntidad);
        public bool Delete(IEnumerable<int> listadoIds, string usuario);

        public List<EtiquetaPlantillaSendinblueDTO> ObtenerEtiquetasPorPlantilla(int id);




    }
}
