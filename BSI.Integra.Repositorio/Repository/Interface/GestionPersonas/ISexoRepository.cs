using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ISexoRepository : IGenericRepository<TSexo>
    {
        #region Metodos Base
        TSexo Add(Sexo entidad);
        TSexo Update(Sexo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSexo> Add(IEnumerable<Sexo> listadoEntidad);
        IEnumerable<TSexo> Update(IEnumerable<Sexo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
    }
}
