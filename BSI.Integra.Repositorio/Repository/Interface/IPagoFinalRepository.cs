using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPagoFinalRepository : IGenericRepository<TPagoFinal>
    {
        #region Metodos Base
        TPagoFinal Add(PagoFinal entidad);
        TPagoFinal Update(PagoFinal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPagoFinal> Add(IEnumerable<PagoFinal> listadoEntidad);
        IEnumerable<TPagoFinal> Update(IEnumerable<PagoFinal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
     
    }
}
