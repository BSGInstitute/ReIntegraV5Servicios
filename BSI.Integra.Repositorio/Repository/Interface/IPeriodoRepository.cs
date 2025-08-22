using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPeriodoRepository : IGenericRepository<TPeriodo>
    {
        #region Metodos Base
        TPeriodo Add(Periodo entidad);
        TPeriodo Update(Periodo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPeriodo> Add(IEnumerable<Periodo> listadoEntidad);
        IEnumerable<TPeriodo> Update(IEnumerable<Periodo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PeriodoDTO> ObtenerPeriodo();
        IEnumerable<PeriodoComboDTO> ObtenerCombo();
        List<PeriodoFiltroDTO> ObtenerCombo2();
        DateTime ObtenerFechaInicialNulo(int? IdPeriodo);
        DateTime ObtenerFechaFinalNulo(int? IdPeriodo);
        List<FiltroDTO> ObtenerIdPeriodoFechaActual();
        List<FiltroCombosDTO> ObtenerPersonalMarketingFiltro();
        List<FiltroDTO> ObtenerPeriodosPendiente();
        StringDTO ObtenerFechaInicial(int idPeriodo);
        StringDTO ObtenerFechaFinal(int idPeriodo);
        public List<PeriodoFiltroDTO> ObtenerPeriodos();
        public List<FiltroIdNombreDTO> ObtenerUltimoPeriodo();
    }
}
