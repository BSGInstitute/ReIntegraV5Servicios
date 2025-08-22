using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPespecificoPadreFrecuenciaRepository : IGenericRepository<TPespecificoPadreFrecuencium>
    {
        #region Metodos Base
        TPespecificoPadreFrecuencium Add(PespecificoPadreFrecuencia entidad);
        TPespecificoPadreFrecuencium Update(PespecificoPadreFrecuencia entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPespecificoPadreFrecuencium> Add(IEnumerable<PespecificoPadreFrecuencia> listadoEntidad);
        IEnumerable<TPespecificoPadreFrecuencium> Update(IEnumerable<PespecificoPadreFrecuencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PespecificoPadreFrecuencia? ObtenerPorId(int id);
        PespecificoPadreFrecuencia? ObtenerPorIdPespecifico(int idPEspecifico);
    }
}
