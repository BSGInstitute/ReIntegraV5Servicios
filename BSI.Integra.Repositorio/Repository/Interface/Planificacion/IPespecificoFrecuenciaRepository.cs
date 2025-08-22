using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPespecificoFrecuenciaRepository : IGenericRepository<TPespecificoFrecuencium>
    {
        #region Metodos Base
        TPespecificoFrecuencium Add(PespecificoFrecuencia entidad);
        TPespecificoFrecuencium Update(PespecificoFrecuencia entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPespecificoFrecuencium> Add(IEnumerable<PespecificoFrecuencia> listadoEntidad);
        IEnumerable<TPespecificoFrecuencium> Update(IEnumerable<PespecificoFrecuencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PespecificoFrecuencia? ObtenerPorId(int id);
        IEnumerable<PespecificoFrecuencia>? ObtenerPorIds(List<int> id);
        PespecificoFrecuencia? ObtenerPorIdPespecifico(int idPespecifico);
        IEnumerable<PespecificoFrecuenciaDTO> ObtenerPespecificoFrecuenciaPorIdPespecifico(int idPespecifico);
    }
}
