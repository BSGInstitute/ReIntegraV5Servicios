using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAsignacionOportunidadRepository : IGenericRepository<TAsignacionOportunidad>
    {
        #region Metodos Base
        TAsignacionOportunidad Add(AsignacionOportunidad entidad);
        TAsignacionOportunidad Update(AsignacionOportunidad entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TAsignacionOportunidad> Add(IEnumerable<AsignacionOportunidad> listadoEntidad);
        IEnumerable<TAsignacionOportunidad> Update(IEnumerable<AsignacionOportunidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        AsignacionOportunidad ObtenerPorIdOportunidad(int idOportunidad);
        ValorIntDTO ObtenerCantidadOportunidadesAsesor(int idAsesor, DateTime fechaAsignacion);
        ValorIntDTO ObtenerMaximaAsignacionAsesor(int idAsesor);
    }
}
