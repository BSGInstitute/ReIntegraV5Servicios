using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISolicitudCategoriaRepository : IGenericRepository<TSolicitudCategorium>
    {
        #region Metodos Base
        TSolicitudCategorium Add(SolicitudCategoria entidad);
        TSolicitudCategorium Update(SolicitudCategoria entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSolicitudCategorium> Add(IEnumerable<SolicitudCategoria> listadoEntidad);
        IEnumerable<TSolicitudCategorium> Update(IEnumerable<SolicitudCategoria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        SolicitudCategoria ObtenerPorId(int id);
        IEnumerable<ComboSolicitudDTO> ObtenerCombo();
        IEnumerable<ComboSolicitudDTO> ObtenerComboPorTipoReporte(int idTipoReporte);
        IEnumerable<TipoReporteCategoriaDTO> ObtenerTipoReporteCategoria();
        IEnumerable<TipoReporteSubCategoriaDTO> ObtenerTipoReporteSubCategoria();
    }
}
