using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelSueldoIndividualService
    {
        #region Metodos Base
        SentinelSueldoIndividual Add(SentinelSueldoIndividual entidad);
        SentinelSueldoIndividual Update(SentinelSueldoIndividual entidad);
        bool Delete(int id, string usuario);

        List<SentinelSueldoIndividual> Add(List<SentinelSueldoIndividual> listadoEntidad);
        List<SentinelSueldoIndividual> Update(List<SentinelSueldoIndividual> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSueldoIndividualDTO> ObtenerSentinelSueldoIndividual();
        IEnumerable<SentinelSueldoIndividualComboDTO> ObtenerCombo();
        FloatDTO ObtenerSueldoPromedioPorDni(string dni);
    }
}
