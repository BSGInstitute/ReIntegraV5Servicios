using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ITipoPagoCategoriaRepository : IGenericRepository<TTipoPagoCategorium>
    {
        #region Metodos Base
        TTipoPagoCategorium Add(TipoPagoCategoria entidad);
        TTipoPagoCategorium Update(TipoPagoCategoria entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoPagoCategorium> Add(IEnumerable<TipoPagoCategoria> listadoEntidad);
        IEnumerable<TTipoPagoCategorium> Update(IEnumerable<TipoPagoCategoria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerComboPorIdCategoriaPrograma(int idCategoriaPrograma);
    }
}
