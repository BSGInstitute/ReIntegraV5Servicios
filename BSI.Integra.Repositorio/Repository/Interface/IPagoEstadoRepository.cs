using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using EntidadPagoEstado = BSI.Integra.Persistencia.Entidades.IntegraDB.PagoEstado;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPagoEstadoRepository : IGenericRepository<TPagoEstado>
    {
        #region Metodos Base
        TPagoEstado Add(EntidadPagoEstado entidad);
        TPagoEstado Update(EntidadPagoEstado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPagoEstado> Add(IEnumerable<EntidadPagoEstado> listadoEntidad);
        IEnumerable<TPagoEstado> Update(IEnumerable<EntidadPagoEstado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PagoEstadoDTO> ObtenerPagoEstados();
    }
}
