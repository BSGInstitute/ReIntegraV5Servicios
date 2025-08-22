using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMonedaRepository : IGenericRepository<TMonedum>
    {
        #region Metodos Base
        TMonedum Add(Moneda entidad);
        TMonedum Update(Moneda entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMonedum> Add(IEnumerable<Moneda> listadoEntidad);
        IEnumerable<TMonedum> Update(IEnumerable<Moneda> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MonedaDTO> ObtenerMoneda();
        IEnumerable<MonedaComboDTO> ObtenerCombo();
        Task<IEnumerable<MonedaComboDTO>> ObtenerComboAsync();
        StringDTO ObtenerCodigoMonedaPorIdAlumno(int idAlumno);
        IEnumerable<MonedaNombrePluralSimboloDTO> ObtenerMonedaNombrePluralSimbolo();
        IEnumerable<MonedaCodigoCambioDTO> ObtenerMonedaCodigoCambio();
        MonedaNombrePluralSimboloDTO ObtenerMonedaParaDocumento(int idMoneda);
        Task<MonedaNombrePluralSimboloDTO> ObtenerMonedaParaDocumentoAsync(int idMoneda);
        List<MonedaNombrePluralSimboloDTO> ObtenerMonedaTodo();
        MonedaNombrePluralSimboloDTO ObtenerMonedaPorId(int id);
        public List<FiltroGenericoDTO> ObtenerFiltroMoneda();
    }
}