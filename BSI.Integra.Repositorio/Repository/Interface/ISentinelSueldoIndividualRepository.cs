using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelSueldoIndividualRepository : IGenericRepository<TSentinelSueldoIndividual>
    {
        #region Metodos Base
        TSentinelSueldoIndividual Add(SentinelSueldoIndividual entidad);
        TSentinelSueldoIndividual Update(SentinelSueldoIndividual entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinelSueldoIndividual> Add(IEnumerable<SentinelSueldoIndividual> listadoEntidad);
        IEnumerable<TSentinelSueldoIndividual> Update(IEnumerable<SentinelSueldoIndividual> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSueldoIndividualDTO> ObtenerSentinelSueldoIndividual();
        IEnumerable<SentinelSueldoIndividualComboDTO> ObtenerCombo();
        FloatDTO ObtenerSueldoPromedioPorDni(string dni);
    }
}