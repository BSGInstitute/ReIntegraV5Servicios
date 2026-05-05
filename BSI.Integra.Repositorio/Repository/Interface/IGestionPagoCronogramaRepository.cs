using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGestionPagoCronogramaRepository : IGenericRepository<TGestionPagoCronograma>
    {
        #region Metodos Base
        TGestionPagoCronograma Add(GestionPagoCronograma entidad);
        TGestionPagoCronograma Update(GestionPagoCronograma entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TGestionPagoCronograma> Add(IEnumerable<GestionPagoCronograma> listadoEntidad);
        IEnumerable<TGestionPagoCronograma> Update(IEnumerable<GestionPagoCronograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<GestionPagoCronogramaDTO> ObtenerCronogramaPorGestionPago(int idGestionPago);
        bool ActualizarFechaPago(int idCronograma, DateTime fechaRealPago, string usuario);
    }
}
