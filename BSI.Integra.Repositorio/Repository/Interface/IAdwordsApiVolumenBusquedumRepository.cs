using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAdwordsApiVolumenBusquedumRepository : IGenericRepository<TAdwordsApiVolumenBusquedum>
    {
        #region Metodos Base
        TAdwordsApiVolumenBusquedum Add(AdwordsApiVolumenBusquedum entidad);
        TAdwordsApiVolumenBusquedum Update(AdwordsApiVolumenBusquedum entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAdwordsApiVolumenBusquedum> Add(IEnumerable<AdwordsApiVolumenBusquedum> listadoEntidad);
        IEnumerable<TAdwordsApiVolumenBusquedum> Update(IEnumerable<AdwordsApiVolumenBusquedum> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<AdwordsApiVolumenBusquedaHistoricoDTO> ObtenerHistorico(DateTime fechaInicio, DateTime fechaFin, string palabras, int idPais);
        void eliminarhistorico(DateTime fechaInicio, DateTime fechaFin, string palabras, int idPais);


    }
}
