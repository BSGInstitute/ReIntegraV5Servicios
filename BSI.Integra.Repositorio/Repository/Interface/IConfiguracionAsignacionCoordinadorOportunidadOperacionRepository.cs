using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionAsignacionCoordinadorOportunidadOperacionRepository
    {
        #region Metodos Base
        TConfiguracionAsignacionCoordinadorOportunidadOperacione Add(ConfiguracionAsignacionCoordinadorOportunidadOperacion entidad);
        TConfiguracionAsignacionCoordinadorOportunidadOperacione Update(ConfiguracionAsignacionCoordinadorOportunidadOperacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionAsignacionCoordinadorOportunidadOperacione> Add(IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacion> listadoEntidad);
        IEnumerable<TConfiguracionAsignacionCoordinadorOportunidadOperacione> Update(IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ConfiguracionCoordinadorCentroCostoDTO> ObtenerPorIdPEspecifico(int idPespecifico);
        List<ConfiguracionCentroCostoCoordinadorDTO> ObtenerConfiguracionCoordinadores();
        List<ConfiguracionCentroCostoCoordinadorDTO> ObtenerCentroCostoSigAsignacion();
        List<CentroCostoHijoDTO> ObtenerCentroCostoHijos(int idCentroCosto);
        IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO> ObtenerPorIdPersonal(int idPersonal);
    }
}
