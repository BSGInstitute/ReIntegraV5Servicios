using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICicloRepository : IGenericRepository<TCiclo>
    {
        #region Metodos Base
        TCiclo Add(Ciclo entidad);
        TCiclo Update(Ciclo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCiclo> Add(IEnumerable<Ciclo> listadoEntidad);
        IEnumerable<TCiclo> Update(IEnumerable<Ciclo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
       // List<Ciclo> ObtenerPorIds(string ids);
 
    }
}

