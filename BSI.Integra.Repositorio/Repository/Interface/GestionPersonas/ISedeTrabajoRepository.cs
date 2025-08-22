using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ISedeTrabajoRepository : IGenericRepository<TSedeTrabajo>
    {
        #region Metodos Base
        TSedeTrabajo Add(SedeTrabajo entidad);
        TSedeTrabajo Update(SedeTrabajo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSedeTrabajo> Add(IEnumerable<SedeTrabajo> listadoEntidad);
        IEnumerable<TSedeTrabajo> Update(IEnumerable<SedeTrabajo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public IEnumerable<SedeTrabajoComboDTO> ObtenerSedeTrabajoCombo();
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
    }
}
