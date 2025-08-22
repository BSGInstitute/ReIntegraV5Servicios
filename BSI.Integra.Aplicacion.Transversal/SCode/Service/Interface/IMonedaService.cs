using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IMonedaService
    {
        #region Metodos Base
        Moneda Add(Moneda entidad);
        Moneda Update(Moneda entidad);
        bool Delete(int id, string usuario);

        List<Moneda> Add(List<Moneda> listadoEntidad);
        List<Moneda> Update(List<Moneda> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<MonedaDTO> ObtenerMoneda();
        IEnumerable<MonedaComboDTO> ObtenerCombo();
        StringDTO ObtenerCodigoMonedaPorIdAlumno(int idAlumno);
        IEnumerable<MonedaNombrePluralSimboloDTO> ObtenerMonedaNombrePluralSimbolo();
        IEnumerable<MonedaCodigoCambioDTO> ObtenerMonedaCodigoCambio();
        MonedaNombrePluralSimboloDTO ObtenerMonedaParaDocumento(int idMoneda);
        List<MonedaNombrePluralSimboloDTO> ObtenerMonedaTodo();
        MonedaNombrePluralSimboloDTO ObtenerMonedaPorId(int id);
        public List<FiltroGenericoDTO> ObtenerFiltroMoneda();
    }
}
