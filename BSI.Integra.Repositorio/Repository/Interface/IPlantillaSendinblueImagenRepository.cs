using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaSendinblueImagenRepository : IGenericRepository<TPlantillaSendinblueImagen>
    {
        public TPlantillaSendinblueImagen Add(PlantillaSendinblueImagen entidad);
        public TPlantillaSendinblueImagen Update(PlantillaSendinblueImagen entidad);
        public bool Delete(int id, string usuario);
        public IEnumerable<TPlantillaSendinblueImagen> Add(IEnumerable<PlantillaSendinblueImagen> listadoEntidad);
        public IEnumerable<TPlantillaSendinblueImagen> Update(IEnumerable<PlantillaSendinblueImagen> listadoEntidad);
        public bool Delete(IEnumerable<int> listadoIds, string usuario);

        public List<PlantillaSendinblueImagenDTO> ObtenerImagenesPlantilla();
        public string guardarArchivos(byte[] archivo, string tipo, string nombreArchivo);

    }
}
