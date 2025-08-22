using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPeriodoService
    {
        #region Metodos Base
        Periodo Add(PeriodoDTO entidad, string Usuario);
        Periodo Update(PeriodoDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<Periodo> Add(List<Periodo> listadoEntidad);
        List<Periodo> Update(List<Periodo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PeriodoDTO> ObtenerPeriodo();
        IEnumerable<PeriodoComboDTO> ObtenerCombo();
        List<FiltroDTO> ObtenerPeriodosPendiente();
        StringDTO ObtenerFechaInicial(int idPeriodo);
        StringDTO ObtenerFechaFinal(int idPeriodo);
    }
}
