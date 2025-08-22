using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaSendinblueBaseRepository : IGenericRepository<TConjuntoListum>
    {
        public TConjuntoListum Add(PlantillaSendinblueBase entidad);
        public TConjuntoListum Update(PlantillaSendinblueBase entidad);
        public bool Delete(int id, string usuario);
        public IEnumerable<TConjuntoListum> Add(IEnumerable<PlantillaSendinblueBase> listadoEntidad);
        public IEnumerable<TConjuntoListum> Update(IEnumerable<PlantillaSendinblueBase> listadoEntidad);
        public bool Delete(IEnumerable<int> listadoIds, string usuario);

        public List<ComboDTO> ObtenerComboPlantillas();
        public List<PlantillaSendinblueBaseDTO> ObtenerComboPlantillasTodo();


    }
}
