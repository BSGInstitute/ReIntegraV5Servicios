using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IConfiguracionAsignacionCoordinadorOportunidadOperacionService
    {
        #region Metodos Base
        ConfiguracionAsignacionCoordinadorOportunidadOperacion Add(ConfiguracionAsignacionCoordinadorOportunidadOperacion entidad);
        ConfiguracionAsignacionCoordinadorOportunidadOperacion Update(ConfiguracionAsignacionCoordinadorOportunidadOperacion entidad);
        bool Delete(int id, string usuario);

        List<ConfiguracionAsignacionCoordinadorOportunidadOperacion> Add(List<ConfiguracionAsignacionCoordinadorOportunidadOperacion> listadoEntidad);
        List<ConfiguracionAsignacionCoordinadorOportunidadOperacion> Update(List<ConfiguracionAsignacionCoordinadorOportunidadOperacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        ConfiguracionCoordinadoraCentroCostoCantidadDTO ObtenerCoordinadorAsignacion(int idPEspecifico, int? idEstadoMatricula, int? idSubEstadoMatricula, int idMatriculaCabecera);
        List<ConfiguracionCentroCostoCoordinadorDTO> ObtenerConfiguracionCoordinadores();
        List<ConfiguracionCentroCostoCoordinadorDTO> ObtenerCentroCostoSigAsignacion();
        ConfiguracionAsignacionCoordinadorOportunidadOperacion MapeoEntidadDesdeDTO(ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO dto);
        IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO> ObtenerPorIdPersonal(int idPersonal);
    }
}
