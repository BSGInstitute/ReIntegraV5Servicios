using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAsignacionOportunidadService
    {
        #region Metodos Base
        AsignacionOportunidad Add(AsignacionOportunidad entidad);
        AsignacionOportunidad Update(AsignacionOportunidad entidad);
        bool Delete(int id, string usuario);
        List<AsignacionOportunidad> Add(List<AsignacionOportunidad> listadoEntidad);
        List<AsignacionOportunidad> Update(List<AsignacionOportunidad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        AsignacionOportunidad AsignacionPorIdOportunidad(int idOportunidad);
        ValorIntDTO ObtenerCantidadOportunidadesAsesor(int idAsesor, DateTime fechaAsignacion);
        ValorIntDTO ObtenerMaximaAsignacionAsesor(int idAsesor);
        AsignacionOportunidad ObtenerPorIdOportunidad(int idOportunidad);
    }
}
