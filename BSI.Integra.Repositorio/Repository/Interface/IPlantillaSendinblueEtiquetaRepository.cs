using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaSendinblueEtiquetaRepository : IGenericRepository<TConjuntoListum>
    {
        public TConjuntoListum Add(PlantillaSendinblueEtiqueta entidad);
        public TConjuntoListum Update(PlantillaSendinblueEtiqueta entidad);
        public bool Delete(int id, string usuario);
        public IEnumerable<TConjuntoListum> Add(IEnumerable<PlantillaSendinblueEtiqueta> listadoEntidad);
        public IEnumerable<TConjuntoListum> Update(IEnumerable<PlantillaSendinblueEtiqueta> listadoEntidad);
        public bool Delete(IEnumerable<int> listadoIds, string usuario);

        public List<EtiquetaPlantillaSendinblueDTO> ObtenerEtiquetasPlantilla();

    }
}
